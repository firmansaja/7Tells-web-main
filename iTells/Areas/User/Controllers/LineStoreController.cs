using ActionServices;
using BusinessObjects;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System.Globalization;

namespace iTells.Areas.User.Controllers
{
    public class LineStoreController : Controller
    {
        ILineStoreService service { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public LineStoreController() : this(new LineStoreService()) { }
        public LineStoreController(ILineStoreService service)
        {
            this.service = service;
        }

        // GET: User/LineStore
        public ActionResult Index()
        {
            GetDataLineStore();

            return View();
        }

        public ActionResult GetDataLineStore()
        {
            try
            {
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                List<LineStorePartModel> ListLineStore = new List<LineStorePartModel>();
                ListLineStore = service.GetLineStore();
                int Empty = 0;
                int Critical = 0;
                int Normal = 0;
                int Over = 0;
                foreach (var item in ListLineStore)
                {
                    if (item.Status == "Empty")
                    {
                        Empty += 1;
                    } else if (item.Status == "Critical")
                    {
                        Critical += 1;
                    }
                    else if (item.Status == "Over")
                    {
                        Over += 1;
                    } else
                    {
                        Normal += 1;
                    }

                }
                ViewData["DataLineStore"] = ListLineStore;
                ViewBag.Empty = Empty;
                ViewBag.Critical = Critical;
                ViewBag.Over = Over;
                ViewBag.Normal = Normal;
                return PartialView("_ListLineStore");
            }
            catch (Exception)
            {

                throw;
            }

        }
        
        public ActionResult ScanPart()
        {
            if (dHelper.CurrentLoginUserName() == null)
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

        public ActionResult ScanPartProd()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login"); 
            }

            return View();
        }

        public ActionResult ScanPartPunch()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            DataTable dt = new DataTable();
            dt = service.GetJadwalLine(dHelper.CurrentLoginUserName());

            if (dt.Rows.Count > 0)
            {
                //string date = "28/12/2019";
                string[] validformats = new[] { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
                                        "MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff" };

                CultureInfo provider = new CultureInfo("en-US");

                ViewBag.Tanggal = dt.Rows[0]["Tanggal"];
                ViewBag.Shift = dt.Rows[0]["Shift"];
                ViewBag.Line = dt.Rows[0]["Line"];
            }

            return View();
        }

        public ActionResult CheckPart(string invtId)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

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
                        JobNo = dt.Rows[0]["JobNo"],
                        QtyPassCard = dt.Rows[0]["QtyPassCard"]
                    };

                    return Json(new { success = true, message = "Pass card ditemukan", part = dataPart }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, message = "Pass card " + invtId + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SimpanScanPart(List<LineStoreScanPartModel> Data)
        {
            try
            {
                bool isSave = false;

                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }                

                int hasil = service.SimpanScanPart(Data, dHelper.CurrentLoginUserName());

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

        [HttpPost]
        public ActionResult SimpanScanPartPunch(List<LineStorePunchModel> Data)
        {
            try
            {
                bool isSave = false;

                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                int hasil = service.SimpanScanPartPunch(Data, dHelper.CurrentLoginUserName());

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
    }
}