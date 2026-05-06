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
    public class EBudgetPLController : Controller
    {
        IEbudgetPLService service { get; set; }
        IEBudgetService service1 { get; set; }
        BudgetAksesUser Budgetakses { get; set; }

        GeneralHelper dHelper = new GeneralHelper();

        public EBudgetPLController() : this(new EBudgetPLService()) { }
        public EBudgetPLController(IEbudgetPLService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var data_ = service.GetFormatPL();
            return View(data_);

        }
        public ActionResult ProfitLost(string tahun)
        {
            ViewBag.Title = "Profit Lost - " + tahun;
            var data_ = service.GetProfitLost(tahun);
            return View(data_);

        }

        public ActionResult budgetPL_Header()
        {

            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            var usetest = User.Identity.Name;
            List<ComboList> listYear = new List<ComboList>();
            int yearEnd = DateTime.Now.Year + 1;
            for (int i = yearEnd; i >= 2017; i--)
            {
                listYear.Add(new ComboList
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            //ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["ComboTahun"] = listYear;
            ViewBag.Title = "Profit Lost";
            return View();

        }
        public ActionResult ShowProfitLostByAccount(string acctid, string tahun )
        {
            ViewBag.Title = "Profit Lost - " + tahun;
            var data_ = service.GetProfitLostByAccount(acctid,tahun);
            return View(data_);

        }

        public ActionResult CreateDataToExcel_ProfitlostByAccount(string acctid, string tahun)
        {
            string Deptid = dHelper.CurrentLoginDeptID();
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_ProfitlostByAccount(acctid, tahun);
            //dt.TableName = "Budget";
            string fileName = "Budget_Profitlost.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //wb.Worksheets.Add(dt);
                //wb.SaveAs(fullPath);

                IXLWorksheet worksheet = wb.Worksheets.Add("Profitlost");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Dept";
                worksheet.Cell(currentRow, 3).Value = "Account";
                worksheet.Cell(currentRow, 4).Value = "Account Name";
                worksheet.Cell(currentRow, 5).Value = "Januari";
                worksheet.Cell(currentRow, 6).Value = "Februari";
                worksheet.Cell(currentRow, 7).Value = "Maret";
                worksheet.Cell(currentRow, 8).Value = "April";
                worksheet.Cell(currentRow, 9).Value = "Mei";
                worksheet.Cell(currentRow, 10).Value = "Juni";
                worksheet.Cell(currentRow, 11).Value = "Juli";
                worksheet.Cell(currentRow, 12).Value = "Agustus";
                worksheet.Cell(currentRow, 13).Value = "September";
                worksheet.Cell(currentRow, 14).Value = "Oktober";
                worksheet.Cell(currentRow, 15).Value = "November";
                worksheet.Cell(currentRow, 16).Value = "Desember";
                worksheet.Cell(currentRow, 17).Value = "Total";
                worksheet.Cell(currentRow, 18).Value = "Tahun";



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i+1;
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["DeptName"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["jan"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["feb"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["mar"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["apr"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["mei"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["jun"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["jul"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["ags"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["sep"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["okt"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["nov"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["des"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["total"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["Tahun"];



                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }


            //byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            //return File(fileByteArray, "application/vnd.ms-excel", fileName);


        }
    }
}