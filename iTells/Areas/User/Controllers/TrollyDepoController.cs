using ActionServices;
using BusinessObjects;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using DataTables.Mvc;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using DataObjects.Helpers;
using System.Web.Security;

namespace iTells.Areas.User.Controllers
{
    public class TrollyDepoController : Controller
    {
        ITrollyDepoService service { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public TrollyDepoController() : this(new TrollyDepoService()) { }

        public TrollyDepoController(ITrollyDepoService service)
        {
            this.service = service;
        }

        // GET: User/TrollyDepo
        public ActionResult Index(FilterTrollyOutDepoModel Filter)
        {
            GetDataTrollyInOut(Filter);
            return View();
        }

        public ActionResult TrollyInDepo()
        {
            if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult TrollyOutDepo()
        {
            if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult TrollyOutDepoToADM()
        {
            if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            DataTable dt = new DataTable();
            dt = service.GetJadwalLine(dHelper.CurrentLoginUserName());

            if (dt.Rows.Count > 0)
            {
                ViewBag.Tanggal = dt.Rows[0]["Tanggal"];
                ViewBag.Shift = dt.Rows[0]["Shift"];
                ViewBag.Line = dt.Rows[0]["Line"];
            }

            return View();
        }

        public ActionResult LapTrollyOutDepo()
        {
            if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetLapTrollyOutDepo([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, FilterTrollyOutDepoModel Filter)
        {
            if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            var query = new TrollyDepoModel[] { }.AsQueryable();
            query = service.GetLapTrollyOutDepo(Filter).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.NoTrans.ToLower().ToString().Contains(value.ToString().ToLower())
                                    );
            }

            var filteredCount = query.Count();

            #endregion Filtering

            #region Sorting
            // Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "NoTrans asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_Out => new
            {
                _Out.NoTrans,
                _Out.TglTrans,
                _Out.NoMobil,
                _Out.NamaSupir
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckNoMobil(string noMobil)
        {
            try
            {
                List<TrollyDepoAllModel> listTrolly = new List<TrollyDepoAllModel>();
                listTrolly = service.CheckNoMobil(noMobil);
                if (listTrolly.Count > 0)
                {    
                    return Json(new { success = true, message = "No mobil ditemukan", dataTrolly = listTrolly }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No mobil " + noMobil + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CheckNoTransTSC(string noTrans)
        {
            try
            {
                if ( string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
                {
                    return Json(new { success = false, errorCode = 401, redirectUrl = "/account/logout" }, JsonRequestBehavior.AllowGet);
                }

                List<TrollyDepoAllModel> listTrolly = new List<TrollyDepoAllModel>();
                listTrolly = service.CheckNoTransTSC(noTrans);
                if (listTrolly.Count > 0)
                {
                    return Json(new { success = true, message = "No surat jalan ditemukan", dataTrolly = listTrolly }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No surat jalan " + noTrans + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CheckKodeQR(string kodeQR)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = service.CheckKodeQR(kodeQR);
                if (dt.Rows.Count > 0)
                {
                    var dataTrolley = new
                    {
                        KodeQR = dt.Rows[0]["KodeQR"],
                        TypeTrolly = dt.Rows[0]["TypeTrolly"],
                        Project = dt.Rows[0]["Project"]
                    };

                    return Json(new { success = true, message = "Kode QR ditemukan", dataTrolly = dataTrolley }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Kode QR " + kodeQR + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SimpanTrollyInOutDepo(TrollyDepoModel model)
        {
            try
            {
                bool isSave = false;
                var DeptID = "";

                //if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
                //{
                //    return Redirect("/account/login");
                //}

                //if (dHelper.CurrentLoginDeptID() != null)
                //{
                //    DeptID = dHelper.CurrentLoginDeptID().ToString();
                //}

                model.Username = dHelper.CurrentLoginUserName();

                int hasil = service.SimpanTrollyInOutDepo(model, DeptID);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Data Berhasil Disimpan" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Data Gagal Disimpan" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewReportTrolleyOut(String NoTrans)
        {
            this.HttpContext.Session["NoTrans"] = NoTrans;

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataTrollyInOut(FilterTrollyOutDepoModel Filter)
        {
            try
            {
                if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
                {
                    return Redirect("/account/login");
                }

                List<TrollyInOutDepoModel> ListTrollyInOut = new List<TrollyInOutDepoModel>();
                ListTrollyInOut = service.GetTrollyInOutDepo(Filter);
                ViewData["DataTrollyInOut"] = ListTrollyInOut;
                return PartialView("_ListTrollyInOutDepo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult CheckPart(string invtId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = service.CheckPart(invtId);
                if (dt.Rows.Count > 0)
                {
                    var dataPart = new
                    {
                        PartNo = dt.Rows[0]["PartNo"],
                        PartName = dt.Rows[0]["PartName"],
                        Type = dt.Rows[0]["Type"]
                    };

                    return Json(new { success = true, message = "Passcard ditemukan", part = dataPart }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, message = "Passcard " + invtId + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SimpanPartDepoToADM(List<TrollyInOutDepoToADMModel> Data)
        {
            try
            {
                bool isSave = false;
                
                if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
                {
                    //return Redirect("/account/login");
                    return Json(new { errorCode = 401 , redirectUrl = "/account/logout" });
                }

                int hasil = service.SimpanPartDepoToADM(Data, dHelper.CurrentLoginUserName());

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Data Berhasil Disimpan" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Data Gagal Disimpan" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult TrollyStokDepo(FilterTrollyOutDepoModel Filter)
        {
            GetDataTrollyStokDepo(Filter);
            return View();
        }

        public ActionResult GetDataTrollyStokDepo(FilterTrollyOutDepoModel Filter)
        {
            try
            {
                if (string.IsNullOrEmpty( dHelper.CurrentLoginUserName()))
                {
                    return Redirect("/account/login");
                }

                List<TrollyStokDepoModel> ListTrollyStok = new List<TrollyStokDepoModel>();
                ListTrollyStok = service.GetTrollyStokDepo(Filter);
                ViewData["DataTrollyStok"] = ListTrollyStok;
                return PartialView("_ListTrollyStokDepo");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}