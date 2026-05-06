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
    public class PRController : Controller
    {
        PRAksesUser prakses { get; set; }
        IPRService service { get; set; }
        IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public PRController() : this(new PRAksesUser(), new PRService(), new ComboListService()) { }
        public PRController(PRAksesUser prakses, IPRService service, IComboListService comboListService)
        {
            this.prakses = prakses;
            this.service = service;
            this.comboListService = comboListService;
        }
        // GET: User/PR
        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            PRUserAkses();
            List<ComboList> listYear = new List<ComboList>();
            int yearEnd = DateTime.Now.Year + 1;
            for (int i = yearEnd; i >= 2021; i--)
            {
                listYear.Add(new ComboList
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            List<ComboList> listMenu = new List<ComboList>();
            listMenu = comboListService.GetSubMenuPR(prakses.Dept.Substring(1));

            List<ComboList> listDept = new List<ComboList>();
            listDept = comboListService.GetDeptPR(); 

            List<ComboList> listSubAcct = new List<ComboList>();
            listSubAcct = comboListService.GetSubAcct(dHelper.CurrentLoginUserName());

            ViewData["FiscalYear"] = listYear;
            ViewData["ListMenu"] = listMenu;
            ViewData["ListDept"] = listDept;
            ViewData["ListSubAcct"] = listSubAcct;
            return View();
        }

        public void PRUserAkses()
        {            
            prakses = service.GetAkses(dHelper.CurrentLoginUserName());
            prakses.Dept = dHelper.CurrentLoginDeptID(); 
            ViewData["AksesUser"] = prakses;
        }

        [HttpPost]
        public ActionResult GetModelPR([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            prakses = service.GetAkses(dHelper.CurrentLoginUserName());

            var query = service.GetModelPR(prakses).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PRNo.ToLower().ToString().Contains(value.ToString().ToLower())
                                    || p.UserBuat.ToLower().ToString().Contains(value.ToString().ToLower())
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
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);
            
            var data = query.Select(_PR => new
            {
                _PR.PRNo,
                _PR.Description,
                _PR.PRDate,
                _PR.PurchaseType,
                _PR.ApprovalPIC,
                _PR.Remark,
                _PR.UserBuat,
                _PR.DeptCreate
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPRByUser([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            prakses = service.GetAkses(dHelper.CurrentLoginUserName());
            var query = service.GetModelPRByUser(prakses).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PRNo.ToLower().ToString().Contains(value.ToString().ToLower())
                                    || p.UserBuat.ToLower().ToString().Contains(value.ToString().ToLower())
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
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_PR => new
            {
                _PR.PRNo,
                _PR.Description,
                _PR.PRDate,
                _PR.PurchaseType,
                _PR.QtyPO,
                _PR.Kol2,
                _PR.Kol3,
                _PR.UserBuat
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPRByAll([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            prakses = service.GetAkses(dHelper.CurrentLoginUserName());
            var query = service.GetModelPRByAll(prakses).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PRNo.ToLower().ToString().Contains(value.ToString().ToLower())
                                    || p.UserBuat.ToLower().ToString().Contains(value.ToString().ToLower())
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
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_PR => new
            {
                _PR.PRNo,
                _PR.Description,
                _PR.PRDate,
                _PR.PurchaseType,
                _PR.UserBuat
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPRPurchase([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = service.GetModelPRPurchase().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PRNo.ToLower().ToString().Contains(value.ToString().ToLower())
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
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_PR => new
            {
                _PR.PRNo,
                _PR.Description,
                _PR.PRDate,
                _PR.PurchaseType,
                _PR.ApprovalPIC,
                _PR.Tanggal,
                _PR.Remark,
                _PR.QtyPO,
                _PR.UserBuat

            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPRHistory([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, PRHistory Filter)
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
            
            var query = new PR[] { }.AsQueryable();
            if (DeptID == "PUR" || DeptID == "PVM")
            {
                query = service.GetModelPRHistoryPur(Filter).AsQueryable();
            }
            else
            {
                Filter.UserName = dHelper.CurrentLoginUserName();
                query = service.GetModelPRHistory(Filter).AsQueryable();
            }

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PRNo.ToLower().ToString().Contains(value.ToString().ToLower())
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
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_PR => new
            {
                _PR.PRNo,
                _PR.Description,
                _PR.PRDate,
                _PR.PurchaseType
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModelPRProgress([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, PRProgress Filter)
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

            var query = new PR[] { }.AsQueryable();
            query = service.GetModelPRProgress(Filter).AsQueryable();            

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PRNo.ToLower().ToString().Contains(value.ToString().ToLower())
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
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_PR => new
            {
                _PR.PRNo,
                _PR.Description,
                _PR.PRDate,
                _PR.PurchaseType
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportExcel(PRHistory filter)
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

            List<PRDetail> ListPR = new List<PRDetail>();

            if (DeptID == "PUR" || DeptID == "PVM")
            {
                ListPR = service.GetModelPRHistoryExportPur(filter);
            }
            else
            {
                filter.UserName = dHelper.CurrentLoginUserName();
                ListPR = service.GetModelPRHistoryExport(filter);
            }

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "PRNo";
                    worksheet.Cell(currentRow, 2).Value = "Tanggal";
                    worksheet.Cell(currentRow, 3).Value = "Dept";
                    worksheet.Cell(currentRow, 4).Value = "No";
                    worksheet.Cell(currentRow, 5).Value = "Nama Barang";
                    worksheet.Cell(currentRow, 6).Value = "Spesifikasi";
                    worksheet.Cell(currentRow, 7).Value = "InvtID";
                    worksheet.Cell(currentRow, 8).Value = "Qty";
                    worksheet.Cell(currentRow, 9).Value = "Satuan";
                    worksheet.Cell(currentRow, 10).Value = "Remark";
                    worksheet.Cell(currentRow, 11).Value = "Acct";
                    worksheet.Cell(currentRow, 12).Value = "Descr Acct";
                    worksheet.Cell(currentRow, 13).Value = "Sub Account";
                    worksheet.Cell(currentRow, 14).Value = "Deldate";
                    worksheet.Cell(currentRow, 15).Value = "PO Date";
                    worksheet.Cell(currentRow, 16).Value = "Purchase Type";
                    worksheet.Cell(currentRow, 17).Value = "Status";
                    worksheet.Cell(currentRow, 18).Value = "Unit Price";
                    worksheet.Cell(currentRow, 19).Value = "Informasi/Suplier";
                    worksheet.Cell(currentRow, 20).Value = "Tanggal Approval";
                    worksheet.Range("A1:T1").Style.Font.Bold = true;
                    foreach (var PR in ListPR)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = PR.PRNo;
                        worksheet.Cell(currentRow, 2).Value = PR.Tanggal;
                        worksheet.Cell(currentRow, 3).Value = PR.Dept;
                        worksheet.Cell(currentRow, 4).Value = PR.No;
                        worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 5).Value = PR.NamaBarang;
                        worksheet.Cell(currentRow, 6).Value = PR.InvtSpec;
                        worksheet.Cell(currentRow, 7).Value = PR.InvtID;
                        worksheet.Cell(currentRow, 8).Value = PR.Qty;
                        worksheet.Cell(currentRow, 9).Value = PR.UnitQty;
                        worksheet.Cell(currentRow, 10).Value = PR.Remark;
                        worksheet.Cell(currentRow, 11).Value = PR.Account;
                        worksheet.Cell(currentRow, 12).Value = PR.AccountDesc;
                        worksheet.Cell(currentRow, 13).Value = PR.SubAccount;
                        worksheet.Cell(currentRow, 14).Value = PR.DelDate;
                        worksheet.Cell(currentRow, 15).Value = PR.PODate;
                        worksheet.Cell(currentRow, 16).Value = PR.PurchaseType;
                        worksheet.Cell(currentRow, 17).Value = PR.Status;
                        worksheet.Cell(currentRow, 18).Value = PR.Price;
                        worksheet.Cell(currentRow, 19).Value = PR.Informasi;
                        worksheet.Cell(currentRow, 20).Value = PR.TanggalApprove;
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
        public ActionResult ExportExcelProgress(PRProgress filter)
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

            List<PRDetail> ListPR = new List<PRDetail>();

            ListPR = service.GetModelPRProgressExport(filter);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "PRNo";
                    worksheet.Cell(currentRow, 2).Value = "Tanggal";
                    worksheet.Cell(currentRow, 3).Value = "Dept";
                    worksheet.Cell(currentRow, 4).Value = "No";
                    worksheet.Cell(currentRow, 5).Value = "Nama Barang";
                    worksheet.Cell(currentRow, 6).Value = "Spesifikasi";
                    worksheet.Cell(currentRow, 7).Value = "InvtID";
                    worksheet.Cell(currentRow, 8).Value = "Qty";
                    worksheet.Cell(currentRow, 9).Value = "Satuan";
                    worksheet.Cell(currentRow, 10).Value = "Remark";
                    worksheet.Cell(currentRow, 11).Value = "Acct";
                    worksheet.Cell(currentRow, 12).Value = "Descr Acct";
                    worksheet.Cell(currentRow, 13).Value = "Sub Account";
                    worksheet.Cell(currentRow, 14).Value = "Deldate";
                    worksheet.Cell(currentRow, 15).Value = "PO Date";
                    worksheet.Cell(currentRow, 16).Value = "Purchase Type";
                    worksheet.Cell(currentRow, 17).Value = "Status";
                    worksheet.Cell(currentRow, 18).Value = "Unit Price";
                    worksheet.Cell(currentRow, 19).Value = "Informasi/Suplier";
                    worksheet.Cell(currentRow, 20).Value = "Tanggal Approval";
                    worksheet.Range("A1:T1").Style.Font.Bold = true;
                    foreach (var PR in ListPR)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = PR.PRNo;
                        worksheet.Cell(currentRow, 2).Value = PR.Tanggal;
                        worksheet.Cell(currentRow, 3).Value = PR.Dept;
                        worksheet.Cell(currentRow, 4).Value = PR.No;
                        worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 5).Value = PR.NamaBarang;
                        worksheet.Cell(currentRow, 6).Value = PR.InvtSpec;
                        worksheet.Cell(currentRow, 7).Value = PR.InvtID;
                        worksheet.Cell(currentRow, 8).Value = PR.Qty;
                        worksheet.Cell(currentRow, 9).Value = PR.UnitQty;
                        worksheet.Cell(currentRow, 10).Value = PR.Remark;
                        worksheet.Cell(currentRow, 11).Value = PR.Account;
                        worksheet.Cell(currentRow, 12).Value = PR.AccountDesc;
                        worksheet.Cell(currentRow, 13).Value = PR.SubAccount;
                        worksheet.Cell(currentRow, 14).Value = PR.DelDate;
                        worksheet.Cell(currentRow, 15).Value = PR.PODate;
                        worksheet.Cell(currentRow, 16).Value = PR.PurchaseType;
                        worksheet.Cell(currentRow, 17).Value = PR.Status;
                        worksheet.Cell(currentRow, 18).Value = PR.Price;
                        worksheet.Cell(currentRow, 19).Value = PR.Informasi;
                        worksheet.Cell(currentRow, 20).Value = PR.TanggalApprove;
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

        [HttpGet]
        public ActionResult Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }

        #region Approve Or Update   
        public ActionResult ViewPR(string PRNo, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string PRNoDecrypt = dHelper.DecryptURL(PRNo);
            string YearDecrypt = dHelper.DecryptURL(Year);

            PRUserAkses();
            GetPRHeader(PRNoDecrypt, YearDecrypt);
            GetPRDetail(PRNoDecrypt, YearDecrypt);
            GetPRApprove(PRNoDecrypt);

            return View();
        }               

        public ActionResult ViewPRUser(string PRNo)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string PRNoDecrypt = dHelper.DecryptURL(PRNo);

            PRUserAkses();
            GetPRHeader(PRNoDecrypt, "");
            GetPRDetailWithoutBudget(PRNoDecrypt);
            GetPRApprove(PRNoDecrypt);

            return View();
        }

        public ActionResult ViewPRAll(string PRNo)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string PRNoDecrypt = dHelper.DecryptURL(PRNo);

            PRUserAkses();
            GetPRHeader(PRNoDecrypt, "");
            GetPRDetailWithoutBudget(PRNoDecrypt);
            GetPRApprove(PRNoDecrypt);

            return View();
        }

        public ActionResult ViewPRPurchase(string PRNo)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string PRNoDecrypt = dHelper.DecryptURL(PRNo);

            PRUserAkses();
            GetPRHeader(PRNoDecrypt, "");
            GetPRDetailWithoutBudget(PRNoDecrypt);
            GetPRApprove(PRNoDecrypt);

            return View();
        }

        private void GetPRHeader(string PRNo , string Year)
        {
            PR pr = new PR();
            pr = service.GetPRHeaderById(PRNo);
            pr.Year = Year;
            ViewData["PRHeader"] = pr;
        }

        private void GetPRDetail(string PRNo, string Year)
        {
            List<PRDetail> ListPRDetail = new List<PRDetail>();
            ListPRDetail = service.GetPRDetail(PRNo, Year);
            ViewData["ListPRDetail"] = ListPRDetail;
        }

        private void GetPRDetailWithoutBudget(string PRNo)
        {
            List<PRDetail> ListPRDetailUser = new List<PRDetail>();
            ListPRDetailUser = service.GetPRDetailWithoutBudget(PRNo);
            ViewData["ListPRDetail"] = ListPRDetailUser;
        }

        private void GetPRApprove(string PRNo)
        {
            List<PRApproval> ListApproval = new List<PRApproval>();
            ListApproval = service.GetPRApprove(PRNo);
            ViewData["ListApproval"] = ListApproval;
        }        
        #endregion

        [HttpPost]
        public ActionResult UpdateFlagPR(string PRNo, string ApprovalPIC)
        {            
            try
            {
                bool isSave = false;
                int hasil = service.UpdateFlagPR(PRNo, ApprovalPIC);
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
        public ActionResult UpdateStatusBudget(string PRNo)
        {
            try
            {
                bool isSave = false;
                int hasil = service.UpdateStatusBudget(PRNo);
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
        public ActionResult UpdateFlagPRReject(string PRNo, int Kol)
        {
            try
            {
                bool isSave = false;
                int hasil = service.UpdateFlagPRReject(PRNo, Kol);
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

        [HttpPost]
        public ActionResult UpdatePilihanPurchase(int Action, string PRNo)
        {
            try
            {
                bool isSave = false;
                int hasil = service.UpdatePilihanPurchase(Action, PRNo);
                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = isSave, message = "Simpan Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewBudget(string Dept, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string DeptDecrypt = dHelper.DecryptURL(Dept);

            prakses = service.GetAkses(dHelper.CurrentLoginUserName());

            ViewBag.Kol = prakses.Kol;
            ViewBag.Year = Year;
            List<PRBudget> ListPRBudget = new List<PRBudget>();
            ListPRBudget = service.GetPRBudget(Year, DeptDecrypt, prakses.Kol.ToString());
            ViewData["ListPRBudget"] = ListPRBudget;
            if (ListPRBudget.Count > 0)
            {
                ViewBag.Sub = DeptDecrypt + " - " + ListPRBudget[0].SubDesc.ToString();
            }

            return View();
        }

        public ActionResult ViewBudgetDetail(String Year, String Month, String Sub, String Acct)
        {
            List<PRBudgetDetail> ListPRBudgetDetail = new List<PRBudgetDetail>();
            ListPRBudgetDetail = service.GetPRBudgetDetail(Year, Month, Sub, Acct);
            ViewData["ListPRBudgetDetail"] = ListPRBudgetDetail;
            ViewBag.Month = Month;
            if (ListPRBudgetDetail.Count != 0)
            {
                ViewBag.Sub = ListPRBudgetDetail[0].SubAccount.ToString() + " - " + ListPRBudgetDetail[0].SubAccountDesc.ToString();
                ViewBag.Account = ListPRBudgetDetail[0].Account.ToString() + " - " + ListPRBudgetDetail[0].AccountDesc.ToString();
            }
            return PartialView("_BudgetDetail");
        }

        [HttpPost]
        public ActionResult UpdateApvRemark(string PRNo, string Remark)
        {
            try
            {
                bool isSave = false;
                int hasil = service.UpdateApvRemark(PRNo, Remark);
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
        public ActionResult ApprovePR(string PRNo)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                 prakses = service.GetAkses(dHelper.CurrentLoginUserName());

                int hasil = service.ApprovePR(prakses.UserName, prakses.Kol, PRNo);

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

        [HttpPost]
        public ActionResult UpdateKomenPR(List<PRDetail> PRDetail)
        {
            try
            {
                bool isSave = false;

                int hasil = service.UpdateKomenPR(PRDetail);

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
        public ActionResult UpdateInfoHargaPR(List<PRDetail> PRDetail)
        {
            try
            {
                bool isSave = false;

                int hasil = service.UpdateInfoHargaPR(PRDetail);

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
        public ActionResult UpdateOverBudgetPR(string PRNo)
        {
            try
            {
                bool isSave = false;
                PR DataPR = new PR();
                DataPR = service.GetPRHeaderById(PRNo);
                if (DataPR != null)
                {
                    if (dHelper.CurrentLoginUserName() == null)
                    {
                        return Redirect("/account/login");
                    }

                    int hasil = service.UpdateOverBudgetPR(dHelper.CurrentLoginUserName(), PRNo);
                    if (hasil != -1)
                    {
                        isSave = true;
                        return Json(new { success = isSave, message = "Proses Selesai" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = isSave, message = "Proses Gagal" }, JsonRequestBehavior.AllowGet);
                    }
                } else
                {
                    return Json(new { success = isSave, message = "Nomor PR Tidak Ditemukan" }, JsonRequestBehavior.AllowGet);
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ExportExcelBudget(string Dept, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            prakses = service.GetAkses(dHelper.CurrentLoginUserName());

            List<PRBudget> ListPRBudget = new List<PRBudget>();
            ListPRBudget = service.GetPRBudget(Year, Dept.Substring(0,5), prakses.Kol.ToString());

            var fileName = "Report Budget.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");
                    
                    worksheet.Cell(1, 1).Value = "Fiscal Year : " + Year.ToString();
                    worksheet.Cell(1, 5).Value = "Sub Account : " + Dept;
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Cell(2, 2).Value = "Acct";
                    worksheet.Cell(2, 3).Value = "Desct Acct";
                    worksheet.Cell(2, 4).Value = "Januari";
                    worksheet.Cell(2, 7).Value = "Februari";
                    worksheet.Cell(2, 10).Value = "Maret";
                    worksheet.Cell(2, 13).Value = "April";
                    worksheet.Cell(2, 16).Value = "Mei";
                    worksheet.Cell(2, 19).Value = "Juni";
                    worksheet.Cell(2, 22).Value = "Juli";
                    worksheet.Cell(2, 25).Value = "Agustus";
                    worksheet.Cell(2, 28).Value = "September";
                    worksheet.Cell(2, 31).Value = "Oktober";
                    worksheet.Cell(2, 34).Value = "November";
                    worksheet.Cell(2, 37).Value = "Desember";
                    worksheet.Cell(3, 4).Value = "Budget";
                    worksheet.Cell(3, 5).Value = "Actual";
                    worksheet.Cell(3, 6).Value = "Sisa";
                    worksheet.Cell(3, 7).Value = "Budget";
                    worksheet.Cell(3, 8).Value = "Actual";
                    worksheet.Cell(3, 9).Value = "Sisa";
                    worksheet.Cell(3, 10).Value = "Budget";
                    worksheet.Cell(3, 11).Value = "Actual";
                    worksheet.Cell(3, 12).Value = "Sisa";
                    worksheet.Cell(3, 13).Value = "Budget";
                    worksheet.Cell(3, 14).Value = "Actual";
                    worksheet.Cell(3, 15).Value = "Sisa";
                    worksheet.Cell(3, 16).Value = "Budget";
                    worksheet.Cell(3, 17).Value = "Actual";
                    worksheet.Cell(3, 18).Value = "Sisa";
                    worksheet.Cell(3, 19).Value = "Budget";
                    worksheet.Cell(3, 20).Value = "Actual";
                    worksheet.Cell(3, 21).Value = "Sisa";
                    worksheet.Cell(3, 22).Value = "Budget";
                    worksheet.Cell(3, 23).Value = "Actual";
                    worksheet.Cell(3, 24).Value = "Sisa";
                    worksheet.Cell(3, 25).Value = "Budget";
                    worksheet.Cell(3, 26).Value = "Actual";
                    worksheet.Cell(3, 27).Value = "Sisa";
                    worksheet.Cell(3, 28).Value = "Budget";
                    worksheet.Cell(3, 29).Value = "Actual";
                    worksheet.Cell(3, 30).Value = "Sisa";
                    worksheet.Cell(3, 31).Value = "Budget";
                    worksheet.Cell(3, 32).Value = "Actual";
                    worksheet.Cell(3, 33).Value = "Sisa";
                    worksheet.Cell(3, 34).Value = "Budget";
                    worksheet.Cell(3, 35).Value = "Actual";
                    worksheet.Cell(3, 36).Value = "Sisa";
                    worksheet.Cell(3, 37).Value = "Budget";
                    worksheet.Cell(3, 38).Value = "Actual";
                    worksheet.Cell(3, 39).Value = "Sisa";
                    worksheet.Range("A1:C1").Merge();                    
                    worksheet.Range("E1:M1").Merge();
                    worksheet.Range("A2:A3").Merge();
                    worksheet.Range("A2:A3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("B2:B3").Merge();
                    worksheet.Range("B2:B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("C2:C3").Merge();
                    worksheet.Range("C2:C3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("D2:F2").Merge();
                    worksheet.Range("D2:F2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("G2:I2").Merge();
                    worksheet.Range("G2:I2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("J2:L2").Merge();
                    worksheet.Range("J2:L2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("M2:O2").Merge();
                    worksheet.Range("M2:O2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("P2:R2").Merge();
                    worksheet.Range("P2:R2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("S2:U2").Merge();
                    worksheet.Range("S2:U2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("V2:X2").Merge();
                    worksheet.Range("V2:X2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("Y2:AA2").Merge();
                    worksheet.Range("Y2:AA2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("AB2:AD2").Merge();
                    worksheet.Range("AB2:AD2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("AE2:AG2").Merge();
                    worksheet.Range("AE2:AG2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 31).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 32).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 33).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("AH2:AJ2").Merge();
                    worksheet.Range("AH2:AJ2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 34).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 35).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 36).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("AK2:AM2").Merge();
                    worksheet.Range("AK2:AM2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 37).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 38).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 39).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:AM1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:AM3").Style.Font.Bold = true;
                    worksheet.Range("A2:AM3").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:AM3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:AM3").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    var currentRow = 3;
                    int No = 0;
                    foreach (var Budget in ListPRBudget)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = Budget.Account;
                        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = Budget.AccountDesc;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Value = Budget.Budget01;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 5).Value = Budget.Actual01;
                        if (Budget.Actual01 > Budget.Budget01)
                        {
                            worksheet.Cell(currentRow, 5).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 6).Value = Budget.Sisa01;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 7).Value = Budget.Budget02;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 8).Value = Budget.Actual02;
                        if (Budget.Actual02 > Budget.Budget02)
                        {
                            worksheet.Cell(currentRow, 8).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = Budget.Sisa02;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = Budget.Budget03;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = Budget.Actual03;
                        if (Budget.Actual03 > Budget.Budget03)
                        {
                            worksheet.Cell(currentRow, 11).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = Budget.Sisa03;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = Budget.Budget04;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = Budget.Actual04;
                        if (Budget.Actual04 > Budget.Budget04)
                        {
                            worksheet.Cell(currentRow, 14).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 14).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 15).Value = Budget.Sisa04;
                        worksheet.Cell(currentRow, 15).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 16).Value = Budget.Budget05;
                        worksheet.Cell(currentRow, 16).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 17).Value = Budget.Actual05;
                        if (Budget.Actual05 > Budget.Budget05)
                        {
                            worksheet.Cell(currentRow, 17).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 17).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 18).Value = Budget.Sisa05;
                        worksheet.Cell(currentRow, 18).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 19).Value = Budget.Budget06;
                        worksheet.Cell(currentRow, 19).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 20).Value = Budget.Actual06;
                        if (Budget.Actual06 > Budget.Budget06)
                        {
                            worksheet.Cell(currentRow, 20).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 20).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 21).Value = Budget.Sisa06;
                        worksheet.Cell(currentRow, 21).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 22).Value = Budget.Budget07;
                        worksheet.Cell(currentRow, 22).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 23).Value = Budget.Actual07;
                        if (Budget.Actual07 > Budget.Budget07)
                        {
                            worksheet.Cell(currentRow, 23).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 23).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 24).Value = Budget.Sisa07;
                        worksheet.Cell(currentRow, 24).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 25).Value = Budget.Budget08;
                        worksheet.Cell(currentRow, 25).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 26).Value = Budget.Actual08;
                        if (Budget.Actual08 > Budget.Budget08)
                        {
                            worksheet.Cell(currentRow, 26).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 26).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 27).Value = Budget.Sisa08;
                        worksheet.Cell(currentRow, 27).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 28).Value = Budget.Budget09;
                        worksheet.Cell(currentRow, 28).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 29).Value = Budget.Actual09;
                        if (Budget.Actual09 > Budget.Budget09)
                        {
                            worksheet.Cell(currentRow, 29).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 29).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 30).Value = Budget.Sisa09;
                        worksheet.Cell(currentRow, 30).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 31).Value = Budget.Budget10;
                        worksheet.Cell(currentRow, 31).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 31).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 32).Value = Budget.Actual10;
                        if (Budget.Actual10 > Budget.Budget10)
                        {
                            worksheet.Cell(currentRow, 32).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 32).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 32).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 33).Value = Budget.Sisa10;
                        worksheet.Cell(currentRow, 33).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 33).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 34).Value = Budget.Budget11;
                        worksheet.Cell(currentRow, 34).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 34).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 35).Value = Budget.Actual11;
                        if (Budget.Actual11 > Budget.Budget11)
                        {
                            worksheet.Cell(currentRow, 35).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 35).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 35).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 36).Value = Budget.Sisa11;
                        worksheet.Cell(currentRow, 36).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 36).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 37).Value = Budget.Budget12;
                        worksheet.Cell(currentRow, 37).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 37).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 38).Value = Budget.Actual12;
                        if (Budget.Actual12 > Budget.Budget12)
                        {
                            worksheet.Cell(currentRow, 38).Style.Font.FontColor = XLColor.Red;
                        }
                        worksheet.Cell(currentRow, 38).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 38).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 39).Value = Budget.Sisa12;
                        worksheet.Cell(currentRow, 39).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 39).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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

        public ActionResult ExportExcelBudgetDetail(String Year, String Month, String Sub, String Acct)
        {
            List<PRBudgetDetail> ListPRBudgetDetail = new List<PRBudgetDetail>();
            ListPRBudgetDetail = service.GetPRBudgetDetail(Year, Month, Sub.Substring(0,5), Acct.Substring(0, 5));

            var fileName = "Report Budget Detail.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");

                    worksheet.Cell(1, 1).Value = "Sub Account : " + Sub;
                    worksheet.Range("A1:F1").Merge();
                    worksheet.Cell(1, 7).Value = "Account : " + Acct;
                    worksheet.Range("G1:O1").Merge();
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Range("A2:A3").Merge();
                    worksheet.Range("A2:A3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 2).Value = "No PR";
                    worksheet.Range("B2:B3").Merge();
                    worksheet.Range("B2:B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 3).Value = "No Urut";
                    worksheet.Range("C2:C3").Merge();
                    worksheet.Range("C2:C3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 4).Value = "Nama Barang";
                    worksheet.Range("D2:D3").Merge();
                    worksheet.Range("D2:D3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 5).Value = "Spesifikasi";
                    worksheet.Range("E2:E3").Merge();
                    worksheet.Range("E2:E3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 6).Value = "InvtID";
                    worksheet.Range("F2:F3").Merge();
                    worksheet.Range("F2:F3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 7).Value = "Qty";
                    worksheet.Range("G2:I2").Merge();
                    worksheet.Range("G2:I2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 10).Value = "Harga";
                    worksheet.Range("J2:K2").Merge();
                    worksheet.Range("J2:K2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 12).Value = "Amount Actual";
                    worksheet.Range("L2:M2").Merge();
                    worksheet.Range("L2:M2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 14).Value = "Dipergunakan Untuk";
                    worksheet.Range("N2:N3").Merge();
                    worksheet.Range("N2:N3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 15).Value = "Target Datang";
                    worksheet.Range("O2:O3").Merge();
                    worksheet.Range("O2:O3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 7).Value = "PR";
                    worksheet.Cell(3, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 8).Value = "PO";
                    worksheet.Cell(3, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 9).Value = "Sisa";
                    worksheet.Cell(3, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 10).Value = "PR";
                    worksheet.Cell(3, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 11).Value = "PO";
                    worksheet.Cell(3, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 12).Value = "PR";
                    worksheet.Cell(3, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(3, 13).Value = "PO";
                    worksheet.Cell(3, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:O3").Style.Font.Bold = true;
                    worksheet.Range("A2:O3").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:O3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:O3").Style.Fill.BackgroundColor = XLColor.DarkOrange;

                    var currentRow = 3;
                    int No = 0;
                    foreach (var BudgetDetail in ListPRBudgetDetail)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = BudgetDetail.PRNo;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = BudgetDetail.XSeq;
                        worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Value = BudgetDetail.InvtDescr;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 5).Value = BudgetDetail.InvtSpec;
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 6).Value = BudgetDetail.InvtId;
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 7).Value = BudgetDetail.QtyPR;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 8).Value = BudgetDetail.QtyPO;
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = BudgetDetail.QtySisa;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = BudgetDetail.HargaPR;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = BudgetDetail.HargaPO;
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = BudgetDetail.AmountPR;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = BudgetDetail.AmountPO;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = BudgetDetail.Remark;
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 15).Value = BudgetDetail.DelDate;
                        worksheet.Cell(currentRow, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;                        
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
        
        public ActionResult ViewReportPR(String PRNo, String Year, string RptName)
        {
            this.HttpContext.Session["PRNo"] = PRNo;
            this.HttpContext.Session["Year"] = Year;
            this.HttpContext.Session["RptName"] = RptName;

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}