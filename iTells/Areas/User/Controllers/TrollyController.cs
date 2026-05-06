using ActionServices;
using BusinessObjects;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace iTells.Areas.User.Controllers
{
    public class TrollyController : Controller
    {
        ITrollyService service { get; set; }
        IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public TrollyController() : this(new TrollyService(), new ComboListService()) { }
        public TrollyController(ITrollyService service, IComboListService comboListService)
        {
            this.service = service;
        }

        // GET: User/Trolly
        public ActionResult Index(FilterTrollyOutDepoModel Filter)
        {
            GetDataTrollyInOut(Filter);
            return View();
        }

        public ActionResult TrollyInTSC()
        {
            if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult TrollyOutTSC()
        {
            if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult CheckTrolly(string kodeQR)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = service.CheckTrolly(kodeQR);
                if (dt.Rows.Count > 0)
                {
                    var dataTrolley = new {
                        KodeQR = dt.Rows[0]["KodeQR"] ,
                        TypeTrolly = dt.Rows[0]["TypeTrolly"],
                        Project = dt.Rows[0]["Project"] ,
                        Qty = dt.Rows[0]["Qty"]
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

        public ActionResult CheckPart(string invtId, string project, string typeTrolly)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = service.CheckPart(invtId, project, typeTrolly);
                if (dt.Rows.Count > 0)
                {
                    var dataPart = new
                    {
                        PartNo = dt.Rows[0]["PartNo"],
                        PartName = dt.Rows[0]["PartName"],
                        Type = dt.Rows[0]["Type"],
                        QtyPassCard = dt.Rows[0]["QtyPassCard"]
                    };

                    return Json(new { success = true, message = "Kode QR ditemukan", part = dataPart }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, message = "Invt Id " + invtId + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CheckNoMobil(string noMobil)
        {
            try
            {
                List<TrollyAllModel> listTrolly = new List<TrollyAllModel>();
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

        public ActionResult CheckSuratJalan(string noTransDepo)
        {
            try
            {
                List<TrollyAllModel> listTrolly = new List<TrollyAllModel>();
                listTrolly = service.CheckNoSuratJalan(noTransDepo);
                if (listTrolly.Count > 0)
                {
                    return Json(new { success = true, message = "No surat jalan ditemukan", dataTrolly = listTrolly }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No surat jalan " + noTransDepo + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SimpanTrollyInOutTSC(TrollyModel model)
        {
            try
            {
                bool isSave = false;
                var DeptID = "";

                if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
                {
                    return Redirect("/account/login");
                }

                //if (dHelper.CurrentLoginDeptID() != null)
                //{
                //    DeptID = dHelper.CurrentLoginDeptID().ToString();
                //}

                model.Username = dHelper.CurrentLoginUserName();

                int hasil = service.SimpanTrollyInOutTSC(model, DeptID);

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

        public ActionResult GetDataTrollyInOut(FilterTrollyOutDepoModel Filter)
        {
            try
            {
                if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
                {
                    return Redirect("/account/login");
                }
               
                List<TrollyInOutModel> ListTrollyInOut = new List<TrollyInOutModel>();
                ListTrollyInOut = service.GetTrollyInOut(Filter);
                ViewData["DataTrollyInOut"] = ListTrollyInOut;
                return PartialView("_ListTrollyInOut");
            }
            catch (Exception)
            {

                throw;
            }

        }

        //public ActionResult ViewTrollyDetail(String noTrans, String kodeQR)
        //{
        //    List<TrollyInOutDetailModel> ListTrollyDetail = new List<TrollyInOutDetailModel>();
        //    ListTrollyDetail = service.GetTrollyInOutDetail(noTrans, kodeQR);
        //    ViewData["ListTrollyDetail"] = ListTrollyDetail;
        //    return PartialView("_ListTrollyInOutDetail");
        //}

        [HttpGet]
        public ActionResult GetTrollyInOutDetail(String noTrans, String kodeQR)
        {

            List<TrollyInOutDetailModel> ListTrollyDetail = new List<TrollyInOutDetailModel>();
            ListTrollyDetail = service.GetTrollyInOutDetail(noTrans, kodeQR);
            ViewData["ListTrollyDetail"] = ListTrollyDetail;

            return View();
        }
    }
}