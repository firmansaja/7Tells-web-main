using ActionServices;
using iTells.Helper;
using System.Web.Mvc;

namespace iTells.Areas.User.Controllers
{
    public class Module1Controller : Controller
    {
        private IModuleService service { get; set; }
        private IComboListService comboListService { get; set; }

        private readonly GeneralHelper dHelper = new GeneralHelper();

        public Module1Controller() : this(new ModuleService(), new ComboListService())
        {
        }

        public Module1Controller(IModuleService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }

        // GET: User/Module1
        public ActionResult Index()
        {
            return View();
        }
    }
}