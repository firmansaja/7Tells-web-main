using ActionServices.Surat;
using DataTables.Mvc;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace iTells.Areas.User.Controllers.Surat
{
    public class SuratSecController : Controller
    {
        ISuratSecService service { get; set; }
        GeneralHelper dHelper = new GeneralHelper();
        public SuratSecController() : this(new SuratSecService()) { }

        public SuratSecController(ISuratSecService service)
        {
            this.service = service;
        }

        // GET: User/SuratSec
        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            ViewData["ListSuratSecurity"] = service.Get();


            return View();
        }

        [HttpPost]
        //public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{

        //    var query = service.Get().AsQueryable();

        //    var totalCount = query.Count();

        //    #region Filtering
        //    //Apply filters for searching
        //    if (requestModel.Search.Value != string.Empty)
        //    {
        //        var value = requestModel.Search.Value.Trim();
        //        query = query.Where(p => p.Kategori.ToLower().ToString().Contains(value.ToString().ToLower())
        //                            );
        //    }

        //    var filteredCount = query.Count();

        //    #endregion Filtering

        //    #region Sorting
        //    // Sorting
        //    var sortedColumns = requestModel.Columns.GetSortedColumns();
        //    var orderByString = String.Empty;

        //    foreach (var column in sortedColumns)
        //    {
        //        orderByString += orderByString != String.Empty ? "," : "";
        //        orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
        //    }

        //    query = query.OrderBy(orderByString == string.Empty ? "id asc" : orderByString);

        //    #endregion Sorting

        //    // Paging
        //    query = query.Skip(requestModel.Start).Take(requestModel.Length);

        //    var data = query.Select(res => new
        //    {
        //        res.Id,
        //        res.Dept,
        //        res.NoSurat,
        //        res.Tanggal,
        //        res.Kategori,
        //        res.Pengirim,
        //        res.Perihal
        //    }).ToList();

        //    return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {

            var query = service.Get().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Perihal.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "Id desc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(res => new
            {
                res.Id,
                res.NoUrut,
               // res.NoSurat,
                res.Tanggal,
                res.Dept,
                //res.Kategori,
               //res.Tujuan,
                res.Pengirim,
                res.Penerima,
                res.Perihal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
    }
}
