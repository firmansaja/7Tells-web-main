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
    public class MRPController : Controller
    {
        IMRPService service { get; set; }
        IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public MRPController() : this(new MRPService(), new ComboListService()) { }
        public MRPController(IMRPService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }

        // GET: User/MRP
        public ActionResult Index()
        {
            return View();
        }

        #region Module Forecast
        public ActionResult ForecastCust()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

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

            ViewData["Year"] = listYear;

            return View();
        }

        [HttpPost]
        public ActionResult GetForecastCust([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string Year)
        {
            string CurrentYear = DateTime.Now.Year.ToString();

            var query = service.GetListForecatCust(Year == "" ? CurrentYear : Year).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CustID.ToLower().ToString().Contains(value.ToString().ToLower())
                                    || p.Customer.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "CustID asc" : orderByString);

            #endregion Sorting

            // Paging
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_Cek => new
            {
                _Cek.CustID,
                _Cek.Customer,
                _Cek.FCJan,
                _Cek.FCFeb,
                _Cek.FCMar,
                _Cek.FCApr,
                _Cek.FCMei,
                _Cek.FCJun,
                _Cek.FCJul,
                _Cek.FCAgt,
                _Cek.FCSep,
                _Cek.FCOkt,
                _Cek.FCNov,
                _Cek.FCDes
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewForecastModel(string CustID, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string custIDDecrypt = dHelper.DecryptURL(CustID);

            ViewBag.Year = Year;
            List<MRPForecastModel> ListForecastModel = new List<MRPForecastModel>();
            ListForecastModel = service.GetListForecatModel(Year, custIDDecrypt);

            ViewData["ListForecastModel"] = ListForecastModel;
            if (ListForecastModel.Count > 0)
            {
                ViewBag.CustID = custIDDecrypt + " - " + ListForecastModel[0].Customer.ToString();
            }

            return View();
        }

        public ActionResult ViewForecastDetail(string CustID, string Model, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string custIDDecrypt = dHelper.DecryptURL(CustID);
            string modelDecrypt = dHelper.DecryptURL(Model);

            ViewBag.Year = Year;
            ViewBag.Model = modelDecrypt;
            ViewBag.CustID = custIDDecrypt;
            List<MRPForecastDetail> ListForecastDetail = new List<MRPForecastDetail>();
            ListForecastDetail = service.GetListForecatDetail(Year, custIDDecrypt, modelDecrypt);

            ViewData["ListForecastDetail"] = ListForecastDetail;

            return View();
        }

        public ActionResult ExportExcelForecastModel(string CustID, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            List<MRPForecastModel> ListForecastModel = new List<MRPForecastModel>();
            ListForecastModel = service.GetListForecatModel(Year, CustID.Substring(0, 3));

            var fileName = "Report Forecast Model.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");

                    worksheet.Cell(1, 1).Value = "Tahun : " + Year.ToString();
                    worksheet.Cell(1, 4).Value = "Customer : " + CustID;
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Cell(2, 2).Value = "Model";
                    worksheet.Cell(2, 3).Value = "Januari";
                    worksheet.Cell(2, 4).Value = "Februari";
                    worksheet.Cell(2, 5).Value = "Maret";
                    worksheet.Cell(2, 6).Value = "April";
                    worksheet.Cell(2, 7).Value = "Mei";
                    worksheet.Cell(2, 8).Value = "Juni";
                    worksheet.Cell(2, 9).Value = "Juli";
                    worksheet.Cell(2, 10).Value = "Agustus";
                    worksheet.Cell(2, 11).Value = "September";
                    worksheet.Cell(2, 12).Value = "Oktober";
                    worksheet.Cell(2, 13).Value = "November";
                    worksheet.Cell(2, 14).Value = "Desember";
                    worksheet.Range("A1:C1").Merge();
                    worksheet.Range("D1:N1").Merge();
                    worksheet.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:N2").Style.Font.Bold = true;
                    worksheet.Range("A2:N2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:N2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:N2").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    var currentRow = 2;
                    int No = 0;
                    foreach (var Forecast in ListForecastModel)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = Forecast.Model;
                        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = Forecast.FCJan;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 4).Value = Forecast.FCFeb;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 5).Value = Forecast.FCMar;
                        worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 6).Value = Forecast.FCApr;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 7).Value = Forecast.FCMei;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 8).Value = Forecast.FCJun;
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = Forecast.FCJul;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = Forecast.FCAgt;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = Forecast.FCSep;
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = Forecast.FCOkt;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = Forecast.FCNov;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = Forecast.FCDes;
                        worksheet.Cell(currentRow, 14).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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

        public ActionResult ExportExcelForecastCust(string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            List<MRPForecastCust> ListForecastCust = new List<MRPForecastCust>();
            ListForecastCust = service.GetListForecatCust(Year);

            var fileName = "Report Forecast Customer.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");

                    worksheet.Cell(1, 1).Value = "Tahun : " + Year.ToString();
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Cell(2, 2).Value = "CustID";
                    worksheet.Cell(2, 3).Value = "Customer";
                    worksheet.Cell(2, 4).Value = "Januari";
                    worksheet.Cell(2, 5).Value = "Februari";
                    worksheet.Cell(2, 6).Value = "Maret";
                    worksheet.Cell(2, 7).Value = "April";
                    worksheet.Cell(2, 8).Value = "Mei";
                    worksheet.Cell(2, 9).Value = "Juni";
                    worksheet.Cell(2, 10).Value = "Juli";
                    worksheet.Cell(2, 11).Value = "Agustus";
                    worksheet.Cell(2, 12).Value = "September";
                    worksheet.Cell(2, 13).Value = "Oktober";
                    worksheet.Cell(2, 14).Value = "November";
                    worksheet.Cell(2, 15).Value = "Desember";
                    worksheet.Range("A1:C1").Merge();
                    worksheet.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:O2").Style.Font.Bold = true;
                    worksheet.Range("A2:O2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:O2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:O2").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    var currentRow = 2;
                    int No = 0;
                    foreach (var Forecast in ListForecastCust)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = Forecast.CustID;
                        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = Forecast.Customer;
                        worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Value = Forecast.FCJan;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 5).Value = Forecast.FCFeb;
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 6).Value = Forecast.FCMar;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 7).Value = Forecast.FCApr;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 8).Value = Forecast.FCMei;
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = Forecast.FCJun;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = Forecast.FCJul;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = Forecast.FCAgt;
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = Forecast.FCSep;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = Forecast.FCOkt;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = Forecast.FCNov;
                        worksheet.Cell(currentRow, 14).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 15).Value = Forecast.FCDes;
                        worksheet.Cell(currentRow, 15).Style.NumberFormat.Format = "#,##0";
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

        public ActionResult ExportExcelForecastDetail(string CustID, string Year, string Model)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            List<MRPForecastDetail> ListForecastDetail = new List<MRPForecastDetail>();
            ListForecastDetail = service.GetListForecatDetail(Year, CustID.Substring(0, 3), Model);

            var fileName = "Report Forecast Detail.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");

                    worksheet.Cell(1, 1).Value = "Tahun : " + Year.ToString();
                    worksheet.Cell(1, 4).Value = "Customer : " + CustID;
                    worksheet.Cell(1, 10).Value = "Model : " + Model;
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Cell(2, 2).Value = "Invt ID";
                    worksheet.Cell(2, 3).Value = "Description";
                    worksheet.Cell(2, 4).Value = "Januari";
                    worksheet.Cell(2, 5).Value = "Februari";
                    worksheet.Cell(2, 6).Value = "Maret";
                    worksheet.Cell(2, 7).Value = "April";
                    worksheet.Cell(2, 8).Value = "Mei";
                    worksheet.Cell(2, 9).Value = "Juni";
                    worksheet.Cell(2, 10).Value = "Juli";
                    worksheet.Cell(2, 11).Value = "Agustus";
                    worksheet.Cell(2, 12).Value = "September";
                    worksheet.Cell(2, 13).Value = "Oktober";
                    worksheet.Cell(2, 14).Value = "November";
                    worksheet.Cell(2, 15).Value = "Desember";
                    worksheet.Range("A1:C1").Merge();
                    worksheet.Range("D1:I1").Merge();
                    worksheet.Range("J1:O1").Merge();
                    worksheet.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:O2").Style.Font.Bold = true;
                    worksheet.Range("A2:O2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:O2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:O2").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    var currentRow = 2;
                    int No = 0;
                    foreach (var Forecast in ListForecastDetail)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = Forecast.InvtID;
                        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = Forecast.Nama;
                        worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Value = Forecast.FCJan;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 5).Value = Forecast.FCFeb;
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 6).Value = Forecast.FCMar;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 7).Value = Forecast.FCApr;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 8).Value = Forecast.FCMei;
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = Forecast.FCJun;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = Forecast.FCJul;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = Forecast.FCAgt;
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = Forecast.FCSep;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = Forecast.FCOkt;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = Forecast.FCNov;
                        worksheet.Cell(currentRow, 14).Style.NumberFormat.Format = "#,##0";
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 15).Value = Forecast.FCDes;
                        worksheet.Cell(currentRow, 15).Style.NumberFormat.Format = "#,##0";
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

        #endregion

        #region Modul Bom
        public ActionResult BoMCalculate()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

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

            ViewData["Year"] = listYear;

            return View();
        }

        [HttpPost]
        public ActionResult GetBoMCalculate([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string Year, string Site)
        {
            string CurrentYear = DateTime.Now.Year.ToString();

            var query = service.GetListBoMCalculate(Year == "" ? CurrentYear : Year, Site).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Site.ToLower().ToString().Contains(value.ToString().ToLower())
                                    || p.InvtID.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "InvtId asc" : orderByString);

            #endregion Sorting

            // Paging
            //query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_Cek => new
            {
                _Cek.Account,
                _Cek.Site,
                _Cek.InvtID,
                _Cek.Nama,
                _Cek.Unit,
                _Cek.FCJan,
                _Cek.FCFeb,
                _Cek.FCMar,
                _Cek.FCApr,
                _Cek.FCMei,
                _Cek.FCJun,
                _Cek.FCJul,
                _Cek.FCAgt,
                _Cek.FCSep,
                _Cek.FCOkt,
                _Cek.FCNov,
                _Cek.FCDes
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewBoMCalculateDetail(string MaterialID, string Site, string Year)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string materialIDDecrypt = dHelper.DecryptURL(MaterialID);
            string siteDecrypt = dHelper.DecryptURL(Site);

            ViewBag.Year = Year;
            ViewBag.MaterialID = materialIDDecrypt;
            ViewBag.Site = siteDecrypt;

            List<MRPBoMCalculateDetail> ListBoMCalculateDetail = new List<MRPBoMCalculateDetail>();
            ListBoMCalculateDetail = service.GetListBoMCalculateDetail(Year, siteDecrypt, materialIDDecrypt);

            ViewData["ListBoMCalculateDetail"] = ListBoMCalculateDetail;

            return View();
        }

        public ActionResult ExportExcelBoMCalculate(string Year, string Site)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            List<MRPBoMCalculate> ListBoMCalculate = new List<MRPBoMCalculate>();
            ListBoMCalculate = service.GetListBoMCalculate(Year, Site);

            var fileName = "Report BoM Calculate.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");

                    worksheet.Cell(1, 1).Value = "Tahun : " + Year.ToString();
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Cell(2, 2).Value = "Site";
                    worksheet.Cell(2, 3).Value = "Material ID";
                    worksheet.Cell(2, 4).Value = "Nama";
                    worksheet.Cell(2, 5).Value = "Unit";
                    worksheet.Cell(2, 6).Value = "Januari";
                    worksheet.Cell(2, 7).Value = "Februari";
                    worksheet.Cell(2, 8).Value = "Maret";
                    worksheet.Cell(2, 9).Value = "April";
                    worksheet.Cell(2, 10).Value = "Mei";
                    worksheet.Cell(2, 11).Value = "Juni";
                    worksheet.Cell(2, 12).Value = "Juli";
                    worksheet.Cell(2, 13).Value = "Agustus";
                    worksheet.Cell(2, 14).Value = "September";
                    worksheet.Cell(2, 15).Value = "Oktober";
                    worksheet.Cell(2, 16).Value = "November";
                    worksheet.Cell(2, 17).Value = "Desember";
                    worksheet.Range("A1:C1").Merge();
                    worksheet.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Q1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:Q2").Style.Font.Bold = true;
                    worksheet.Range("A2:Q2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:Q2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:Q2").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    var currentRow = 2;
                    int No = 0;
                    foreach (var Forecast in ListBoMCalculate)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = Forecast.Site;
                        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = Forecast.InvtID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Value = Forecast.Nama;
                        worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 5).Value = Forecast.Unit;
                        worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 6).Value = Forecast.FCJan;
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 7).Value = Forecast.FCFeb;
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 8).Value = Forecast.FCMar;
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = Forecast.FCApr;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = Forecast.FCMei;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = Forecast.FCJun;
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = Forecast.FCJul;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = Forecast.FCAgt;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = Forecast.FCSep;
                        worksheet.Cell(currentRow, 14).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 15).Value = Forecast.FCOkt;
                        worksheet.Cell(currentRow, 15).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 16).Value = Forecast.FCNov;
                        worksheet.Cell(currentRow, 16).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 17).Value = Forecast.FCDes;
                        worksheet.Cell(currentRow, 17).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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

        public ActionResult ExportExcelBoMCalculateDetail(string Year, string Site, string MaterialID)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            List<MRPBoMCalculateDetail> ListBoMCalculateDetail = new List<MRPBoMCalculateDetail>();
            ListBoMCalculateDetail = service.GetListBoMCalculateDetail(Year, Site, MaterialID);

            var fileName = "Report BoM Calculate Detail.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");

                    worksheet.Cell(1, 1).Value = "Tahun : " + Year.ToString();
                    worksheet.Cell(1, 4).Value = "Site : " + Site.ToString();
                    worksheet.Cell(1, 5).Value = "Material : " + MaterialID.ToString();
                    worksheet.Cell(2, 1).Value = "No";
                    worksheet.Cell(2, 2).Value = "Parent ID";
                    worksheet.Cell(2, 3).Value = "Parent Lvl 0";
                    worksheet.Cell(2, 4).Value = "Desc";
                    worksheet.Cell(2, 5).Value = "Januari";
                    worksheet.Cell(2, 6).Value = "Februari";
                    worksheet.Cell(2, 7).Value = "Maret";
                    worksheet.Cell(2, 8).Value = "April";
                    worksheet.Cell(2, 9).Value = "Mei";
                    worksheet.Cell(2, 10).Value = "Juni";
                    worksheet.Cell(2, 11).Value = "Juli";
                    worksheet.Cell(2, 12).Value = "Agustus";
                    worksheet.Cell(2, 13).Value = "September";
                    worksheet.Cell(2, 14).Value = "Oktober";
                    worksheet.Cell(2, 15).Value = "November";
                    worksheet.Cell(2, 16).Value = "Desember";
                    worksheet.Range("A1:C1").Merge();
                    worksheet.Range("D1:D1").Merge();
                    worksheet.Range("E1:P1").Merge();
                    worksheet.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(2, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:P1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:P2").Style.Font.Bold = true;
                    worksheet.Range("A2:P2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("A2:P2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:P2").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    var currentRow = 2;
                    int No = 0;
                    foreach (var Forecast in ListBoMCalculateDetail)
                    {
                        currentRow++;
                        No++;
                        worksheet.Cell(currentRow, 1).Value = No;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 2).Value = Forecast.ParentID;
                        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 3).Value = Forecast.ParentIDLvl0;
                        worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 4).Value = Forecast.Desc;
                        worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 5).Value = Forecast.FCJan;
                        worksheet.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 6).Value = Forecast.FCFeb;
                        worksheet.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 7).Value = Forecast.FCMar;
                        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 8).Value = Forecast.FCApr;
                        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 9).Value = Forecast.FCMei;
                        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 10).Value = Forecast.FCJun;
                        worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 11).Value = Forecast.FCJul;
                        worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 12).Value = Forecast.FCAgt;
                        worksheet.Cell(currentRow, 12).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 13).Value = Forecast.FCSep;
                        worksheet.Cell(currentRow, 13).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 14).Value = Forecast.FCOkt;
                        worksheet.Cell(currentRow, 14).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 15).Value = Forecast.FCNov;
                        worksheet.Cell(currentRow, 15).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, 16).Value = Forecast.FCDes;
                        worksheet.Cell(currentRow, 16).Style.NumberFormat.Format = "#,##0.##0";
                        worksheet.Cell(currentRow, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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

        #endregion
        [HttpGet]
        public ActionResult Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }
    }
}