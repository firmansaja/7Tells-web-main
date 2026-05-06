using ActionServices;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using BusinessObjects.PPIC;

namespace iTells.Areas.User.Controllers
{
    public class PPICController : Controller
    {
        IBuktiMuatService service { get; set; }
        IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public PPICController() : this(new BuktiMuatService(), new ComboListService()) { }
        public PPICController(IBuktiMuatService service, IComboListService comboListService)
        {
            this.service = service;
        }

        // GET: User/PPIC
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScanPartOut()
        {
            //DataTable dt = new DataTable();
            //dt = service.GetJadwalLine(dHelper.CurrentLoginUserName());

            //if (dt.Rows.Count > 0)
            //{
            //    ViewBag.Tanggal = dt.Rows[0]["Tanggal"];
            //    ViewBag.Shift = dt.Rows[0]["Shift"];
            //    ViewBag.Line = dt.Rows[0]["Line"];
            //}

            return View();
        }

        public ActionResult CheckBuktiMuat(string noSeq)
        {
            try
            {
                List<BuktiMuatModel> listMuat = new List<BuktiMuatModel>();
                listMuat = service.CheckBuktiMuat(noSeq);
                if (listMuat.Count > 0)
                {
                    return Json(new { success = true, message = "No. Seq ditemukan", dataMuat = listMuat }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No. Seq " + noSeq + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SimpanBuktiMuatOut(List<BuktiMuatModel> DataPart)
        {
            try
            {
                bool isSave = false;

                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                string Username = dHelper.CurrentLoginUserName();

                int hasil = service.SimpanBuktiMuatOut(DataPart, Username);

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

        public ActionResult ViewReportBuktiMuatOut(String NoSeq)
        {
            this.HttpContext.Session["NoSeq"] = NoSeq;

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

    }
}