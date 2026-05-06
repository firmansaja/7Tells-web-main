using iTells.Helper;
using System.Web.Mvc;

namespace iTells.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private GeneralHelper dHelper = new GeneralHelper();

        // GET: Admin/Category
        public ActionResult Index()
        {
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission(Url);
            if (control.IsLogin == false)
            {
                return Redirect("/account/login");
            }

            if (control.IsView == false)
            {
                return Redirect("/admin/notaccess");
            }

            ViewBag.IsAdd = control.IsAdd;
            return View();
        }

        public ActionResult Create()
        {
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission(Url);
            if (control.IsLogin == false)
            {
                return Redirect("/account/login");
            }

            if (control.IsView == false)
            {
                return Redirect("/admin/notaccess");
            }

            return View();
        }
    }
}