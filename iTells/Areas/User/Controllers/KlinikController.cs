using ActionServices;
using DataObjects.Helpers;
using DataTables.Mvc;
using iTells.Helper;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace iTells.Areas.User.Controllers
{
    public class KlinikController : Controller
    {
        GeneralHelper dHelper = new GeneralHelper();

        IKlinikService service { get; set; }
        IComboListService comboListService { get; set; }

        public KlinikController() : this(new KlinikService(), new ComboListService()) { }

        public KlinikController(IKlinikService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }

        public ActionResult Index()
        {
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission(Url);
            if (control.IsLogin == false)
                return Redirect("/account/login");
            if (control.IsView == false)
                return Redirect("/admin/notaccess");

            ViewBag.IsAdd = control.IsAdd;

            return View();
        }

        [HttpPost]
        public ActionResult GetTrxPeriksa([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string TglMulai, string TglSelesai)
        {
            var query = service.GetKlinikPeriksa(Convert.ToDateTime(TglMulai), Convert.ToDateTime(TglSelesai)).AsQueryable();

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

            var data = query.Select(_Trx => new
            {
                _Trx.NoTrans,
                _Trx.Tanggal,
                _Trx.NIK,
                _Trx.Nama,
                _Trx.Dept,
                _Trx.NamaDokter,
                _Trx.Diagnosa,
                _Trx.TotalObat,
                _Trx.TotalHargaObat
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
    }
}