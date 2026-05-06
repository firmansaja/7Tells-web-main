using ActionServices;
using BusinessObjects;
using DataTables.Mvc;
using iTells.Helper;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace iTells.Areas.User.Controllers
{
    public class ModuleController : Controller
    {
        private IModuleService service { get; set; }
        private IComboListService comboListService { get; set; }
        private GeneralHelper dHelper = new GeneralHelper();

        public ModuleController() : this(new ModuleService(), new ComboListService())
        {
        }

        public ModuleController(IModuleService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }

        // GET: User/Module
        public ActionResult Index()
        {
            ViewData["Module"] = comboListService.GetModuleParent();
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetModules([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = service.GetModules().AsQueryable();

            var totalCount = query.Count();

            #region Filtering

            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.FullName.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "FullName asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(Module => new
            {
                Module.FullName,
                Module.ID
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleById(int id)
        {
            var result = service.GetModuleById(id);

            if (result != null)
            {
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }

        public ActionResult Create(Module model)
        {
            bool isSave = false;
            if (model == null)
            {
                return Json(new { success = isSave, message = "Model kosong " }, JsonRequestBehavior.AllowGet);
            }
            string PageSlug = dHelper.GetModuleSlug(model.ModuleName, model.ParentModuleID); ;
            model.PageSlug = PageSlug;
            int hasil = service.Insert(model);
            if (hasil != -1)
            {
                isSave = true;
                return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = isSave, message = "Simpan Data Gagal" }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult Edit(Module model)
        //{
        //    bool isSave = false;
        //    model.updated_by = User.Identity.Name;
        //    int hasil = service.Update(model);
        //    if (hasil != -1)
        //    {
        //        isSave = true;
        //        return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        return Json(new { success = isSave, message = "Update Data Gagal" }, JsonRequestBehavior.AllowGet);

        //    }
        //}
        //public ActionResult Delete(int Id)
        //{
        //    bool isSave = false;
        //    int hasil = service.Delete(Id);
        //    if (hasil != -1)
        //    {
        //        isSave = true;
        //        return Json(new { success = isSave, message = "Hapus Data Berhasil" }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        return Json(new { success = isSave, message = "Hapus Data Gagal" }, JsonRequestBehavior.AllowGet);

        //    }
        //}
    }
}