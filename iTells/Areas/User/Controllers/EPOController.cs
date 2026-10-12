using ActionServices;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using BusinessObjects;
using iTells.Helper;
using DataObjects.Helpers;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using DevExpress.XtraReports.Web;

namespace iTells.Areas.User.Controllers
{
    public class EPOController : Controller
    {

        GeneralHelper dHelper = new GeneralHelper();


        POAksesUser poakses { get; set; }
        IPOServices service { get; set; }
        IPRService servicePR { get; set; }
        IComboListService comboListService { get; set; }

        public EPOController() : this(new PRAksesUser(), new POServices(), new ComboListService()) { }
        public EPOController(PRAksesUser prakses, IPOServices service, IComboListService comboListService)
        {
            this.poakses = poakses;
            this.service = service;
            this.comboListService = comboListService;
        }
        // GET: User/PR
        public void POUserAkses()
        {
            poakses = service.GetAkses(dHelper.CurrentLoginUserName());
            ViewData["AksesUser"] = poakses;
            
        }
        // GET: User/EPO
        public ActionResult Index()
        {
            POUserAkses();
            if (dHelper.CurrentLoginUserName() == null   )
            {
                return Redirect("/account/login");
            }

            List<ComboList> listYear = new List<ComboList>();
            int yearEnd = DateTime.Now.Year;
            for (int i = yearEnd; i >= 2016; i--)
            {
                listYear.Add(new ComboList
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            List<ComboList> listSupplier = new List<ComboList>();
            listSupplier = comboListService.GetSupplier();
            ViewData["FiscalYear"] = listYear;
            ViewData["ListSupplier"] = listSupplier;
            return View();
        }

        [HttpPost]
        public ActionResult GetModelPO([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            POUserAkses();
            var query = service.GetPO(poakses).AsQueryable();
            var totalCount = query.Count();
            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PONumber.ToLower().ToString().Contains(value.ToString().ToLower())
                                    || p.VendorName.ToLower().ToString().Contains(value.ToString().ToLower())
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
            query = query.OrderBy(orderByString == string.Empty ? "PRNo asc" : orderByString);
            #endregion Sorting
            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);
            var data = query.Select(_PO => new
            {
                _PO.PONumber,
                _PO.CreateUser,
                _PO.PODate,
                _PO.ClassID,
                _PO.VendorName,
                _PO.User6
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPOByUser([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, POHistory Filter)
        {
            POUserAkses();

            var query = service.GetPOByUser(poakses,Filter.DTAwal,Filter.DTakhir).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PONumber.ToLower().ToString().Contains(value.ToString().ToLower())
                                        || p.VendorName.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "PONo asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(_PO => new
            {
                _PO.PONumber,
                _PO.CreateUser,
                _PO.PODate,
                _PO.ClassID,
                _PO.VendorName,
                _PO.User6,
                _PO.Level1,
                _PO.Level2,
                _PO.Level3,
                _PO.Level4,

            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPOHistory([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, POHistory Filter)
        {
            var DeptID = "";
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            if (dHelper.CurrentLoginDeptID() != null)
            {
                DeptID = dHelper.CurrentLoginDeptID().ToString().Substring(1, 3);
            }

            var query = new BO_POExport[] { }.AsQueryable();
            //if (DeptID == "PUR")
            //{
            //    //query = service.GetModelPRHistoryPur(Filter).AsQueryable();
            //}
            //else
            //{
            //    Filter.UserName = dHelper.CurrentLoginUserName();
            //    //query = service.GetModelPRHistory(Filter).AsQueryable();
            //}
            query = service.GetModelPOHistory(Filter).AsQueryable();
            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PONbr.ToLower().ToString().Contains(value.ToString().ToLower())
                || p.Name.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "PONbr asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_PR => new
            {
                _PR.PONbr,
                _PR.PromDate,
                _PR.PODate,
                _PR.PRNo,
                _PR.Name,
                _PR.InvtID,
                _PR.TranDesc,
                _PR.Level
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModelPOApproveSM([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            POUserAkses();

            var query = service.GetPOApproveSM(poakses).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PONumber.ToLower().ToString().Contains(value.ToString().ToLower())
                || p.VendorName.ToLower().ToString().Contains(value.ToString().ToLower())
                || p.Level1.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "PRNo asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(_PO => new
            {
                _PO.PONumber,
                _PO.CreateUser,
                _PO.PODate,
                _PO.ClassID,
                _PO.VendorName,
                _PO.User6,
                _PO.Level1
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModelPONonConfirm([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            POUserAkses();

            var query = service.GetPONonConfirm(poakses).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PONumber.ToLower().ToString().Contains(value.ToString().ToLower())
                || p.VendorName.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "PRNo asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(_PO => new
            {
                _PO.PONumber,
                _PO.CreateUser,
                _PO.PODate,
                _PO.ClassID,
                _PO.VendorName,
                _PO.User6,
                _PO.Email,
                _PO.TglEmail



            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModelPOSupplierApprove([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            POUserAkses();

            var query = service.GetPOSupplierApprove(poakses).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PONumber.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "PRNo asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(_PO => new
            {
                _PO.PONumber,
                _PO.CreateUser,
                _PO.PODate,
                _PO.ClassID,
                _PO.VendorName,
                _PO.User6,
                _PO.Email,
                _PO.TglEmail



            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewPOUser(string PONo)
        {
            POUserAkses();
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            // string PRNoDecrypt = dHelper.DecryptURL(PONo);

            //POUserAkses();
            GetPOHeader(PONo, "");
            //GetPRDetailWithoutBudget(PRNoDecrypt);
            GetPOApprove(PONo);

            return View();
        }

        public ActionResult ViewPOApproveUser(string PONo)
        {
            POUserAkses();
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

           // string PRNoDecrypt = dHelper.DecryptURL(PONo);

            //POUserAkses();
            GetPOHeader(PONo, "");
            //GetPRDetailWithoutBudget(PRNoDecrypt);
            GetPOApprove(PONo);

            return View();
        }

        private void GetPOHeader(string PRNo, string Year)
        {
            BO_PO BO = new BO_PO();
            BO = service.GetPOHeaderById(PRNo);
            //BO.Year = Year;
            ViewData["POHeader"] = BO;

            GetPODetail(PRNo,"");
            GetTotalPOAmount(PRNo,"");
        }

        private void GetPODetail(string PRNo, string Year)
        {
            List<BO_PODetail> ListPODetail = new List<BO_PODetail>();
            ListPODetail = service.GetPODetail(PRNo);
            ViewData["ListPODetail"] = ListPODetail;
        }

        private void GetTotalPOAmount(string PRNo, string Year)
        {
            BO_PO BO = new BO_PO();
            BO = service.GetPoTotalAmount(PRNo);
            ViewData["POTotalAmount"] = BO;

          
        }

        private void GetPOApprove(string PONo)
        {
            List<BO_POApproval> ListApproval = new List<BO_POApproval>();
            ListApproval = service.GetPOApprove(PONo);
            ViewData["ListApproval"] = ListApproval;
        }

        // Dipanggil via AJAX dari popup InvtID di ViewPOUser. Mengembalikan kolom + baris dari SP PR_V_GetHistoryPrice.
        [HttpPost]
        public ActionResult GetHistoryPrice(string InvtID)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Json(new { success = false, message = "Session habis, silakan login kembali." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var rows = service.GetHistoryPrice(InvtID);
                var columns = rows.Count > 0 ? rows[0].Keys.ToList() : new List<string>();
                var result = Json(new { success = true, columns, rows }, JsonRequestBehavior.AllowGet);
                result.MaxJsonLength = int.MaxValue;
                return result;
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ApprovePO(string PONo)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                POUserAkses();

                int hasil = service.ApprovePO(poakses.user_id, poakses.Kol, PONo);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Approve Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Approve Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult RejectPO(string PONo)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                POUserAkses();

                int hasil = service.RejectPO(poakses.user_id, poakses.Kol, PONo);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Reject Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Reject Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UpdateUrgent(string PONo)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                POUserAkses();

                int hasil = service.UpdateUrgent(PONo);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Update Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult PrintPO(String PONo, String Year, string RptName)
        {

            this.HttpContext.Session["PO"] = PONo.Trim();
            this.HttpContext.Session["Year"] = Year;
            this.HttpContext.Session["RptName"] = RptName;

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult SendPO(string PONo)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                POUserAkses();

                int hasil = service.PrintPO(PONo);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Update Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SendEmail(string PONo)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                POUserAkses();

                int hasil = service.SendEmail(PONo);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Update Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ExportExcel(POHistory filter)
        {
            var DeptID = "";
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            if (dHelper.CurrentLoginDeptID() != null)
            {
                DeptID = dHelper.CurrentLoginDeptID().ToString().Substring(1, 3);
            }

            var fileName = filter.FileName + ".xlsx";

            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            List<BO_POExport> ListPO = new List<BO_POExport>();

            //if (DeptID == "PUR")
            //{
            //    ListPR = servicePR.GetModelPRHistoryExport(filter);
            //}
            //else
            //{
            //    filter.UserName = dHelper.CurrentLoginUserName();
            //    ListPR = servicePR.GetModelPRHistoryExport(filter);
            //}

            //ListPO = service.GetModelPOHistoryExport(filter);
            ListPO = service.GetModelPOHistory(filter);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "No";
                    worksheet.Cell(currentRow, 2).Value = "PO Nbr";
                    worksheet.Cell(currentRow, 3).Value = "PROM Date";
                    worksheet.Cell(currentRow, 4).Value = "PO Date";
                    worksheet.Cell(currentRow, 5).Value = "Line ID";
                    worksheet.Cell(currentRow, 6).Value = "PR No";
                    worksheet.Cell(currentRow, 7).Value = "Supplier";
                    worksheet.Cell(currentRow, 8).Value = "Invt ID";
                    worksheet.Cell(currentRow, 9).Value = "Alternate ID";
                    worksheet.Cell(currentRow, 10).Value = "Desc";
                    worksheet.Cell(currentRow, 11).Value = "Satuan";
                    worksheet.Cell(currentRow, 12).Value = "Qty PO";
                    worksheet.Cell(currentRow, 13).Value = "Qty RCV";
                    worksheet.Cell(currentRow, 14).Value = "Qty OS";
                    worksheet.Cell(currentRow, 15).Value = "Curry";
                    worksheet.Cell(currentRow, 16).Value = "Price";
                    worksheet.Cell(currentRow, 17).Value = "Approval";
                    worksheet.Cell(currentRow, 18).Value = "Remarks";
                    worksheet.Cell(currentRow, 19).Value = "Circulation No";
                    worksheet.Cell(currentRow, 20).Value = "Site";
                    worksheet.Cell(currentRow, 21).Value = "Terms";
                    worksheet.Range("A1:U1").Style.Font.Bold = true;
                    foreach (var PO in ListPO)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = currentRow - 1;
                        worksheet.Cell(currentRow, 2).Value = PO.PONbr;
                        worksheet.Cell(currentRow, 3).Value = PO.PromDate;
                        worksheet.Cell(currentRow, 4).Value = PO.PODate;
                        worksheet.Cell(currentRow, 5).Value = PO.LineID;
                        worksheet.Cell(currentRow, 6).Value = PO.PRNo;
                        worksheet.Cell(currentRow, 7).Value = PO.Name;
                        worksheet.Cell(currentRow, 8).Value = PO.InvtID;
                        worksheet.Cell(currentRow, 9).Value = PO.AlternateID;
                        worksheet.Cell(currentRow, 10).Value = PO.TranDesc;
                        worksheet.Cell(currentRow, 11).Value = PO.PurchUnit;
                        worksheet.Cell(currentRow, 12).Value = PO.QtyOrd;
                        worksheet.Cell(currentRow, 13).Value = PO.QtyRcvd;
                        worksheet.Cell(currentRow, 14).Value = PO.QtyOrd- PO.QtyRcvd;
                        worksheet.Cell(currentRow, 15).Value = PO.CuryID;
                        worksheet.Cell(currentRow, 16).Value = PO.CuryUnitCost;
                        worksheet.Cell(currentRow, 17).Value = PO.Level;
                        worksheet.Cell(currentRow, 18).Value = PO.Note;
                        worksheet.Cell(currentRow, 19).Value = PO.CirculationNo;
                        worksheet.Cell(currentRow, 20).Value = PO.SiteId;
                        worksheet.Cell(currentRow, 21).Value = PO.Terms;
                    }
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(fullPath);
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        [HttpPost]
        public ActionResult ApproveApproval(List<BO_PODetail> PODetail)
        {
            try
            {
                bool isSave = false;
                //int hasil = 1;
                POUserAkses();
                int hasil = service.UpdateApproval(PODetail,poakses.user_id, poakses.Kol);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Approve PO Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Approve  PO Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult RejectApproval(List<BO_PODetail> PODetail)
        {
            try
            {
                bool isSave = false;
                //int hasil = 1;
                POUserAkses();
                int hasil = service.RejectApproval(PODetail, poakses.user_id, poakses.Kol);

                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Reject PO Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Reject PO Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}