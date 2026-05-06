using ActionServices;
using BusinessObjects;
using iTells.Helper;
using System;
using System.Data;
using System.Web.Mvc;

namespace iTells.Areas.User.Controllers
{
    public class TrolleyController : Controller
    {
        ITrolleyService service { get; set; }
        IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public TrolleyController() : this(new TrolleyService(), new ComboListService()) { }
        public TrolleyController(ITrolleyService service, IComboListService comboListService)
        {
            this.service = service;
        }
        // GET: User/Trolley
        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult TrolleyIn()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult TrolleyOut()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            return View();
        }

        public ActionResult CheckTrolley(string KodeTrolley)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = service.CheckTrolley(KodeTrolley);
                if (dt.Rows.Count > 0 )
                {
                    return Json(new { success = true, message = "Kode trolley ditemukan", jenisTrolley = dt.Rows[0]["jenis_trolley"] }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, message = "Kode trolley " + KodeTrolley + " tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SimpanTrolleyInOut(Trolley model)
        {
            try
            {
                bool isSave = false;
                var DeptID = "";

                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                if (dHelper.CurrentLoginDeptID() != null)
                {
                    DeptID = dHelper.CurrentLoginDeptID().ToString();
                }

                model.Username = dHelper.CurrentLoginUserName();

                int hasil = service.SimpanTrolleyInOut(model, DeptID);

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