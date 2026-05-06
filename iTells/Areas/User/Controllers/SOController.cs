using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects;
using ActionServices;
using DataTables.Mvc;
using System.Linq.Dynamic;
using iTells.Helper;
using DataObjects.Helpers;
using System.Data;
using ClosedXML.Excel;
using System.IO;
namespace iTells.Areas.User.Controllers
{
    public class SOController : Controller
    {
        GeneralHelper dHelper = new GeneralHelper();
        ISoService service { get; set; }
        public SOController() : this(new SOService()) { }

        public SOController(ISoService service)
        {
            //this.prakses = prakses;
            this.service = service;
            //this.comboListService = comboListService;
        }
        // GET: User/SO
        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            //var CustOrdNbr = "AAM1796/12/21";
            //ViewData["Filter"] = service.GetOs_SO(CustOrdNbr);
            return View();
        }
        public ActionResult Load_Os_SalesOrder(string CustOrdNbr)
        {
            var id = CustOrdNbr.ToString();
            return Json(new { redirectUrl = "/ViewOsSalesOrder/" + id });
        }

        public ActionResult ViewOsSalesOrder(string id)
        {
            //ViewData["Filter"] = service.GetOs_SO(dHelper.DecryptURL(id));
            ViewBag.filter = (id);
            ViewBag.Title = "Sales Order - " +  dHelper.DecryptURL(id);
            return View();
        }

        [HttpPost]
        public ActionResult CekCustOrdNbr(string CustOrdNbr)
        {
            try
            {
                bool isCek = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                List<BO_So> CekCustOrdNbr = new List<BO_So>();
                CekCustOrdNbr = service.GetOs_SO(CustOrdNbr);

                int hasil = CekCustOrdNbr.Count();

                if (hasil > 0)
                {
                    isCek = true;
                    return Json(new { success = isCek, message = "Data exist" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    isCek = false;
                    return Json(new { success = isCek, message = "Data Tidak Ditemukan" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult CekByOrdNbr(string OrdNbr)
        {
            try
            {
                bool isCek = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                List<BO_So> CekCustOrdNbr = new List<BO_So>();
                CekCustOrdNbr = service.GetByOrdeNbr(OrdNbr);

                int hasil = CekCustOrdNbr.Count();

                if (hasil > 0)
                {
                    isCek = true;
                    return Json(new { success = isCek, message = "Data exist" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    isCek = false;
                    return Json(new { success = isCek, message = "Data Tidak Ditemukan" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetOutstanding([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string CustOrdNbr)
        {

            //Budgetakses.Dept = dHelper.CurrentLoginDeptID();
            //Budgetakses.UserName = dHelper.CurrentLoginUserName();
            //var User_ = Settings.Instance.UserName;
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            var query = service.GetOs_SO(dHelper.DecryptURL(CustOrdNbr)).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CustOrdNbr.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.CustOrdNbr,
                //Module.No,
                Module.OrdNbr,
                Module.ShipName,
                Module.InvtID,
                Module.Descr,
                Module.AlternateID,
                Module.ReqDate,
                Module.QtyOrd,
                Module.QtyOpenShip,
                Module.QtyCloseShip,
                Module.QtyShip,
                Module.QtyBal,
                Module.SiteID,
                Module.NoSPAB,
                Module.Tujuan,
                Module.No,
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetdataByOrdNbr (string OrdNbr)
        {
            var id = OrdNbr.ToString();
            return Json(new { redirectUrl = "/viewbyordnbr/" + id });
        }

        public ActionResult ViewGetdataByOrdNbr (string id)
        {
            ViewBag.filter = (id);
            ViewBag.Title = "Sales Order - " + dHelper.DecryptURL(id);
            return View();
        }

        public ActionResult loaddataByOrdNbr([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string OrdNbr)
        {

            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            var query = service.GetByOrdeNbr(dHelper.DecryptURL(OrdNbr)).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CustOrdNbr.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.CustOrdNbr,
                Module.OrdNbr,
                Module.No,
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }


    }
}