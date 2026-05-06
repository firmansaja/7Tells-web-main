using iTells.Helper;
using iTells.Models;
using System;
using System.Web.Mvc;

namespace iTells.Areas.Admin.Controllers
{
    public class ActivityLogController : Controller
    {
        private readonly iTellsEntities db = new iTellsEntities();
        private GeneralHelper dHelper = new GeneralHelper();

        // GET: Admin/ActivityLog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetActivityLog()
        {
            var control = dHelper.GetUserPermission("");
            if (control.IsLogin == false)
            {
                return Redirect("/account/login");
            }

            var activity = dHelper.GetActivityLog(Guid.Empty);
            if (control.IsAdmin == false)
            {
                activity = dHelper.GetActivityLog(new Guid(dHelper.CurrentLoginUser()));
            }

            return PartialView("_ActivityLog", activity);
        }
    }
}