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
    public class EBudgetController : Controller
    {

        IEBudgetService service { get; set; }
        BudgetAksesUser Budgetakses { get; set; }

        GeneralHelper dHelper = new GeneralHelper();

        public EBudgetController() : this(new EBudgetService(), new BudgetAksesUser()){ }

        public EBudgetController(IEBudgetService service, BudgetAksesUser budgetAksesUser)
        {
            this.service = service;
            this.Budgetakses = budgetAksesUser;
        }
        // GET: User/EBudget
        public ActionResult Index()
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

            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["ComboTahun"] = listYear;
            ViewBag.Title = "Budget";

            return View();
        }

        public ActionResult LihatBudget(string id)
        {
            ViewData["Filter"] = service.Filter(id, "", "");
            ViewBag.Title = "Budget Tahun  " + id;
            return View();
        }
        public ActionResult LoadBudget(string Tahun)
        {
            var id = Tahun.ToString();
            return Json(new { redirectUrl = "/lihatbudget/" + id });
        }

        public ActionResult GetBudget([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string tahun,string dept)
        {

            Budgetakses.Dept = dHelper.CurrentLoginDeptID();
            Budgetakses.UserName = dHelper.CurrentLoginUserName();
            //var User_ = Settings.Instance.UserName;
            var query = service.GetModules(tahun, Budgetakses.Dept, Budgetakses.UserName).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.AcctID.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.RowNumber,
                Module.No,
                Module.AcctID,
                Module.AcctName,
                Module.Tahun,
                Module.DeptID,
                Module.Jan,
                Module.Feb,
                Module.Mar,
                Module.Apr,
                Module.Mei,
                Module.Jun,
                Module.Jul,
                Module.Agst,
                Module.Sept,
                Module.Okt,
                Module.Nov,
                Module.Des,
                Module.Subtotal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadDetailAccount(BO_EBudget BO)
        {
            var id = BO.FilterTahun.ToString();
            return Json(new { redirectUrl = "/lihatbudget/" + id });
        }

        public ActionResult ShowLihatBudgetDetail(string AcctId, string Tahun, string DeptID)
        {
            //ViewData["FilterTahun"] = service.Filter(id);

            List<CurrentLogin> Userlogin = new List<CurrentLogin>();
            Userlogin.Add(new CurrentLogin
            {
                Dept = dHelper.CurrentLoginDeptID(),
                UserName = dHelper.CurrentLoginUserName()
            });
            ViewData["userlogin"] = Userlogin;

            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["FilterDetailBudget"] = service.FilterDetailBudget(dHelper.DecryptURL(Tahun), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(AcctId));
            GetBudgetDetail2(dHelper.DecryptURL(AcctId), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(Tahun));
            ViewBag.Title = "Budget Tahun  " + dHelper.DecryptURL(Tahun) + "   -   " + dHelper.DecryptURL(DeptID) + "   -   " + dHelper.DecryptURL(AcctId) ;
            return View();
        }

        public ActionResult ShowLihatBudgetDetail_PL(string AcctId, string Tahun, string DeptID)
        {
            //ViewData["FilterTahun"] = service.Filter(id);

            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            //ViewData["FilterDetailBudget"] = service.FilterDetailBudget(dHelper.DecryptURL(Tahun), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(AcctId));
            ViewData["FilterDetailBudget_Pl"] = service.FilterDetailBudget_Pl(Tahun, DeptID, AcctId);
            GetBudgetDetail2(AcctId, DeptID, Tahun);
            ViewBag.Title = "Budget Tahun  " + Tahun+ "(" + DeptID + ")";
            return View();
        }


        public ActionResult NewAccountDept()
        {

            
            string _site ;
            var dept = dHelper.CurrentLoginDeptID().Substring(0,1);

            if (dept == "1")
            {
                _site = "TNG-U";
            }
            else
            {
                _site = "TSC3-U";
            }
            IList<BO_EBudget> BudgetData = new List<BO_EBudget>();
            IList<ComboList> ComboAccount = new List<ComboList>();
            IList<ComboList> ComboDept = new List<ComboList>();
            BudgetData.Add(new BO_EBudget() { AcctID = "", AcctName = "", Tahun = "", DeptID = dHelper.CurrentLoginDeptID(), SiteID = _site });
            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["budgetdata"] = BudgetData;
            ComboAccount = service.ListComboAccount();
            ComboDept = service.ListComboDept(dHelper.CurrentLoginUserName());
            ViewData["ComboAccount"] = ComboAccount;
            ViewData["ComboAccountDept"] = ComboDept;
            //ViewBag.Title = "Budget Tahun  " + "( " + dHelper.CurrentLoginDeptID() + " )";
            ViewBag.Title = "Budget Account Baru" ;
            return View();
        }

        public ActionResult GetBudgetDetail([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string acctid, string deptid, string tahun)
        {
            var dept = dHelper.CurrentLoginDeptID();
            var query = service.GetBudgetDetail(acctid, deptid, tahun).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.AcctID.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.AcctID,
                Module.AcctName,
                Module.Tahun,
                Module.DeptID,
                Module.Keterangan,

            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetBudgetDetail2(string acctid, string deptid, string tahun)

        {
            try

            {
                //Service = new PaintingProduksiServices();
                var data = service.GetBudgetDetail2(acctid, deptid, tahun);
                return PartialView("PartialBudgetDetail2", data);
            }
            catch (Exception)
            {



                throw;
            }

        }

        public ActionResult LoadBudget_Isi(string Tahun)
        {
            //var id = Tahun.ToString();
           
            return Json(new { redirectUrl = "/lihatbudget_isi/" + Tahun });
        }

        public ActionResult LihatBudget_isi(string id)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            string tahun = dHelper.DecryptURL(id);
            ViewData["Filter"] = service.Filter(tahun, "", "");
            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            //ViewBag.Title = "Budget Tahun  " + id + " (" + dHelper.CurrentLoginDeptID() + ")"; 
            // ViewBag.Title = "Budget Departement  " + "( " + dHelper.CurrentLoginDeptID() + " )" + "  Tahun : " + id;
           // config.AppSettings.Settings["SyncDate"].Value = date.ToString();
            return View();
        }

        public ActionResult ShowLihatBudgetDetail_isi(string AcctId, string Tahun, string DeptID)
        {

            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            ViewData["FilterDetailBudget"] = service.FilterDetailBudget(dHelper.DecryptURL(Tahun), dHelper.DecryptURL(DeptID) , dHelper.DecryptURL(AcctId));
            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            var data = service.GetBudgetDetail2(dHelper.DecryptURL(AcctId), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(Tahun));
            ViewBag.Title = "Budget Departement  " + "( " + dHelper.CurrentLoginDeptID() + " )" + "  Tahun : " + dHelper.DecryptURL(Tahun);
            return View(data);
        }

        public ActionResult ShowEditData(string data ,string tahun,string DeptID, string acctid,string semester)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["GetBudgetDetailEdit_Before"] = service.GetBudgetDetailEdit_Before(dHelper.DecryptURL(data),semester);
            ViewData["GettotalAccount"] = service.GetTotalAccount(dHelper.DecryptURL(acctid), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(tahun));
            ViewData["GetBudgetApproveBOD"] = service.GetApproveBOD(dHelper.DecryptURL(acctid), dHelper.DecryptURL(acctid) , dHelper.DecryptURL(tahun) );
            ViewBag.Title = "Budget Departement  " + "( " + dHelper.DecryptURL(DeptID) + " )" + "  Tahun : " + dHelper.DecryptURL(tahun);
            var data_ = service.GetBudgetDetailEdit(dHelper.DecryptURL(data));
            return View(data_);
        }

        public ActionResult ShowEditDataFAC(string data, string tahun, string DeptID, string acctid, string semester)
        {

            List<CurrentLogin> Userlogin = new List<CurrentLogin>();

            Userlogin.Add(new CurrentLogin
            {
                Dept = dHelper.CurrentLoginDeptID(),
                UserName = dHelper.CurrentLoginUserName()
            });
            


            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["GetBudgetDetailEdit_Before"] = service.GetBudgetDetailEdit_Before(data, semester);
            ViewData["GettotalAccount"] = service.GetTotalAccount(acctid, DeptID, tahun);
            ViewData["GetBudgetApproveBOD"] = service.GetApproveBOD(acctid, DeptID, tahun);
            ViewBag.Title = "Budget Departement  " + "( " + DeptID + " )" + "  Tahun : " + tahun;
            ViewData["userlogin"] = Userlogin;
            var data_ = service.GetBudgetDetailEdit(data);
            return View(data_);
        }

        public ActionResult DeleteBudget(BO_EBudget data)
        {
            try
            {
                bool isSave = false;
                var cek = service.ListBudgetOpen("Open");
                if (cek[0].OpenStatus == "Open")
                {
                    int hasil = service.DeleteBudget(data);
                    if (hasil != -1)
                    {
                        isSave = true;
                        return Json(new
                        {
                            success = isSave,
                            message = "Update Data Berhasil",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new
                        {
                            success = isSave,
                            message = "Update GAGAL",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = isSave,
                        message = "Budget Sudah ditutup",
                        account = data.AcctID,
                        tahun = data.Tahun,
                        dept = data.DeptID
                    }, JsonRequestBehavior.AllowGet);
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SimpanBudget(BO_EBudget data)
        {
            try
            {
                bool isSave = false;
                var cek = service.ListBudgetOpen("Open");             
                if (cek[0].OpenStatus == "Open")
                {
                   
                    int hasil = service.SimpanBudget(data);
                    if (hasil != -1)
                    {
                        isSave = true;
                        return Json(new
                        {
                            success = isSave,
                            message = "Update Data Berhasil",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new
                        {
                            success = isSave,
                            message = "Update GAGAL",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = isSave,
                        message = "Budget Sudah Ditutup",
                        account = data.AcctID,
                        tahun = data.Tahun,
                        dept = data.DeptID
                    }, JsonRequestBehavior.AllowGet);

                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SimpanBudget_FAC(BO_EBudget data)
        {
            try
            {
                bool isSave = false;
                
                    int hasil = service.SimpanBudget(data);
                    if (hasil != -1)
                    {
                        isSave = true;
                        return Json(new
                        {
                            success = isSave,
                            message = "Update Data Berhasil",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new
                        {
                            success = isSave,
                            message = "Update GAGAL",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);
                    }
      

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult NewBudget(string AcctId, string AcctName, string Tahun, string DeptId, string SideID)
        {
            IList<BO_EBudget> BudgetData = new List<BO_EBudget>();
            BudgetData.Add(new BO_EBudget() { AcctID = AcctId, AcctName = AcctName, Tahun = Tahun, DeptID = DeptId, SiteID = SideID });
            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["budgetdata"] = BudgetData;
            //ViewBag.Title = "Budget Tahun  " + Tahun + "( " + DeptId + " )";

            ViewBag.Title = "Budget Departement  " + "( " + DeptId + " )" + "  Tahun : " + Tahun;
            return View();
        }

        public ActionResult SimpanBudget_Baru(BO_EBudget data)
        {
            try
            {
                bool isSave = false;
                var cek = service.ListBudgetOpen("Open");
                if (cek[0].OpenStatus == "Open")
                {
                    int hasil = service.SimpanBudget_baru(data);
                    if (hasil != -1)
                    {
                        isSave = true;
                        return Json(new
                        {
                            success = isSave,
                            message = "Update Data Berhasil",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new
                        {
                            success = isSave,
                            message = "Update GAGAL",
                            account = data.AcctID,
                            tahun = data.Tahun,
                            dept = data.DeptID
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = isSave,
                        message = "Budget Sudah Ditutup",
                        account = data.AcctID,
                        tahun = data.Tahun,
                        dept = data.DeptID
                    }, JsonRequestBehavior.AllowGet);
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult CreateDataToExcel(string tahun)
        {
            string Deptid = dHelper.CurrentLoginDeptID();
            DataTable dt = service.CreateExcell(Deptid, tahun);
            //dt.TableName = "Budget";
            string fileName = "Budget.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //wb.Worksheets.Add(dt);
                //wb.SaveAs(fullPath);

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "IdNo";
                worksheet.Cell(currentRow, 3).Value = "AcctID";
                worksheet.Cell(currentRow, 4).Value = "AcctName";
                worksheet.Cell(currentRow, 5).Value = "DeptID";
                worksheet.Cell(currentRow, 6).Value = "SiteID";
                worksheet.Cell(currentRow, 7).Value = "Tahun";
                worksheet.Cell(currentRow, 8).Value = "Keterangan";
                worksheet.Cell(currentRow, 9).Value = "Unit";

                worksheet.Cell(currentRow, 10).Value = "jan_qty";
                worksheet.Cell(currentRow, 11).Value = "feb_qty";
                worksheet.Cell(currentRow, 12).Value = "mar_qty";
                worksheet.Cell(currentRow, 13).Value = "apr_qty";
                worksheet.Cell(currentRow, 14).Value = "mei_qty";
                worksheet.Cell(currentRow, 15).Value = "jun_qty";
                worksheet.Cell(currentRow, 16).Value = "jul_qty";
                worksheet.Cell(currentRow, 17).Value = "agt_qty";
                worksheet.Cell(currentRow, 18).Value = "sep_qty";
                worksheet.Cell(currentRow, 19).Value = "okt_qty";
                worksheet.Cell(currentRow, 20).Value = "nov_qty";
                worksheet.Cell(currentRow, 21).Value = "des_qty";

                worksheet.Cell(currentRow, 22).Value = "jan_harga";
                worksheet.Cell(currentRow, 23).Value = "feb_harga";
                worksheet.Cell(currentRow, 24).Value = "mar_harga";
                worksheet.Cell(currentRow, 25).Value = "apr_harga";
                worksheet.Cell(currentRow, 26).Value = "mei_harga";
                worksheet.Cell(currentRow, 27).Value = "jun_harga";
                worksheet.Cell(currentRow, 28).Value = "jul_harga";
                worksheet.Cell(currentRow, 29).Value = "agt_harga";
                worksheet.Cell(currentRow, 30).Value = "sep_harga";
                worksheet.Cell(currentRow, 31).Value = "okt_harga";
                worksheet.Cell(currentRow, 32).Value = "nov_harga";
                worksheet.Cell(currentRow, 33).Value = "des_harga";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["IdNo"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Keterangan"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["Unit"];

                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["jan_qty"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["feb_qty"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["mar_qty"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["apr_qty"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["mei_qty"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["jun_qty"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["jul_qty"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["agt_qty"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["sep_qty"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["okt_qty"];
                    worksheet.Cell(currentRow, 20).Value = dt.Rows[i]["nov_qty"];
                    worksheet.Cell(currentRow, 21).Value = dt.Rows[i]["des_qty"];

                    worksheet.Cell(currentRow, 22).Value = dt.Rows[i]["jan_harga"];
                    worksheet.Cell(currentRow, 23).Value = dt.Rows[i]["feb_harga"];
                    worksheet.Cell(currentRow, 24).Value = dt.Rows[i]["mar_harga"];
                    worksheet.Cell(currentRow, 25).Value = dt.Rows[i]["apr_harga"];
                    worksheet.Cell(currentRow, 26).Value = dt.Rows[i]["mei_harga"];
                    worksheet.Cell(currentRow, 27).Value = dt.Rows[i]["jun_harga"];
                    worksheet.Cell(currentRow, 28).Value = dt.Rows[i]["jul_harga"];
                    worksheet.Cell(currentRow, 29).Value = dt.Rows[i]["agt_harga"];
                    worksheet.Cell(currentRow, 30).Value = dt.Rows[i]["sep_harga"];
                    worksheet.Cell(currentRow, 31).Value = dt.Rows[i]["okt_harga"];
                    worksheet.Cell(currentRow, 32).Value = dt.Rows[i]["nov_harga"];
                    worksheet.Cell(currentRow, 33).Value = dt.Rows[i]["des_harga"];
                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true });
            }
           
          
            //byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            //return File(fileByteArray, "application/vnd.ms-excel", fileName);


        }


        [HttpPost]
        public ActionResult CreateDataToExcel_SummaryDept(string tahun)
        {
            string Deptid = dHelper.CurrentLoginDeptID();
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_SummaryDept(Deptid, tahun, user);
            //dt.TableName = "Budget";
            string fileName = "Budget_Summary_Dept.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //wb.Worksheets.Add(dt);
                //wb.SaveAs(fullPath);

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "DeptID";
                worksheet.Cell(currentRow, 3).Value = "Desc";
                worksheet.Cell(currentRow, 4).Value = "SiteID";
                worksheet.Cell(currentRow, 5).Value = "Tahun";
                worksheet.Cell(currentRow, 6).Value = "Subtotal";



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["Desc"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Subtotal"];


                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true ,namafile = fileName });
            }


            //byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            //return File(fileByteArray, "application/vnd.ms-excel", fileName);


        }

        public ActionResult CreateDataToExcel_Detail_ByAccount(string tahun, string dept, string account)
        {
            string Deptid = dHelper.CurrentLoginDeptID();
            string user = dHelper.CurrentLoginUserName();

            DataTable dt = service.CreateDataToExcel_BudgetDetail_ByAccount(Deptid, tahun,account,user);
            string fileName = "Budget_Detail_ByAccount.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                worksheet.Cell(currentRow, 7).Value = "Description";
                worksheet.Cell(currentRow, 8).Value = "Unit";


                worksheet.Cell(currentRow, 9).Value = "Jan Qty";
                worksheet.Cell(currentRow, 10).Value = "Jan Harga";
                worksheet.Cell(currentRow, 11).Value = "Jan Total";

                worksheet.Cell(currentRow, 12).Value = "Feb Qty";
                worksheet.Cell(currentRow, 13).Value = "Feb Harga";
                worksheet.Cell(currentRow, 14).Value = "Feb Total";

                worksheet.Cell(currentRow, 15).Value = "Mar Qty";
                worksheet.Cell(currentRow, 16).Value = "Mar Harga";
                worksheet.Cell(currentRow, 17).Value = "Mar Total";

                worksheet.Cell(currentRow, 18).Value = "Apr Qty";
                worksheet.Cell(currentRow, 19).Value = "Apr Harga";
                worksheet.Cell(currentRow, 20).Value = "Apr Total";

                worksheet.Cell(currentRow, 21).Value = "Mei Qty";
                worksheet.Cell(currentRow, 22).Value = "Mei Harga";
                worksheet.Cell(currentRow, 23).Value = "Mei Total";

                worksheet.Cell(currentRow, 24).Value = "Jun Qty";
                worksheet.Cell(currentRow, 25).Value = "Jun Harga";
                worksheet.Cell(currentRow, 26).Value = "Jun Total";

                worksheet.Cell(currentRow, 27).Value = "Jul Qty";
                worksheet.Cell(currentRow, 28).Value = "Jul Harga";
                worksheet.Cell(currentRow, 29).Value = "Jul Total";

                worksheet.Cell(currentRow, 30).Value = "Agus Qty";
                worksheet.Cell(currentRow, 31).Value = "Agus Harga";
                worksheet.Cell(currentRow, 32).Value = "Agus Total";

                worksheet.Cell(currentRow, 33).Value = "Sep Qty";
                worksheet.Cell(currentRow, 34).Value = "Sep Harga";
                worksheet.Cell(currentRow, 35).Value = "Sep Total";

                worksheet.Cell(currentRow, 36).Value = "Okt Qty";
                worksheet.Cell(currentRow, 37).Value = "Okt Harga";
                worksheet.Cell(currentRow, 38).Value = "Okt Total";

                worksheet.Cell(currentRow, 39).Value = "Nov Qty";
                worksheet.Cell(currentRow, 40).Value = "Nov Harga";
                worksheet.Cell(currentRow, 41).Value = "Nov Total";

                worksheet.Cell(currentRow, 42).Value = "Des Qty";
                worksheet.Cell(currentRow, 43).Value = "Des Harga";
                worksheet.Cell(currentRow, 44).Value = "Des Total";


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i + 1;
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Keterangan"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Unit"];

                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["jan_qty"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["jan_harga"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Jan"];

                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["feb_qty"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["feb_harga"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Feb"];

                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["mar_qty"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["mar_harga"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Mar"];

                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["apr_qty"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["apr_harga"];
                    worksheet.Cell(currentRow, 20).Value = dt.Rows[i]["Apr"];

                    worksheet.Cell(currentRow, 21).Value = dt.Rows[i]["mei_qty"];
                    worksheet.Cell(currentRow, 22).Value = dt.Rows[i]["mei_harga"];
                    worksheet.Cell(currentRow, 23).Value = dt.Rows[i]["Mei"];

                    worksheet.Cell(currentRow, 24).Value = dt.Rows[i]["jun_qty"];
                    worksheet.Cell(currentRow, 25).Value = dt.Rows[i]["jun_harga"];
                    worksheet.Cell(currentRow, 26).Value = dt.Rows[i]["Jun"];

                    worksheet.Cell(currentRow, 27).Value = dt.Rows[i]["jul_qty"];
                    worksheet.Cell(currentRow, 28).Value = dt.Rows[i]["jul_harga"];
                    worksheet.Cell(currentRow, 29).Value = dt.Rows[i]["Jul"];

                    worksheet.Cell(currentRow, 30).Value = dt.Rows[i]["agt_qty"];
                    worksheet.Cell(currentRow, 31).Value = dt.Rows[i]["agt_harga"];
                    worksheet.Cell(currentRow, 32).Value = dt.Rows[i]["Ags"];

                    worksheet.Cell(currentRow, 33).Value = dt.Rows[i]["sep_qty"];
                    worksheet.Cell(currentRow, 34).Value = dt.Rows[i]["sep_harga"];
                    worksheet.Cell(currentRow, 35).Value = dt.Rows[i]["Sep"];

                    worksheet.Cell(currentRow, 36).Value = dt.Rows[i]["okt_qty"];
                    worksheet.Cell(currentRow, 37).Value = dt.Rows[i]["okt_harga"];
                    worksheet.Cell(currentRow, 38).Value = dt.Rows[i]["Okt"];

                    worksheet.Cell(currentRow, 39).Value = dt.Rows[i]["nov_qty"];
                    worksheet.Cell(currentRow, 40).Value = dt.Rows[i]["nov_harga"];
                    worksheet.Cell(currentRow, 41).Value = dt.Rows[i]["Nov"];

                    worksheet.Cell(currentRow, 42).Value = dt.Rows[i]["des_qty"];
                    worksheet.Cell(currentRow, 43).Value = dt.Rows[i]["des_harga"];
                    worksheet.Cell(currentRow, 44).Value = dt.Rows[i]["Des"];


                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }



        }
        
        public ActionResult CreateDataToExcel_SummaryDept_Detail(string tahun)
        {
            string Deptid = dHelper.CurrentLoginDeptID();
            string user = dHelper.CurrentLoginUserName();

            DataTable dt = service.CreateDataToExcel_SummaryDept_Detail(Deptid, tahun, user);
            string fileName = "Budget_Detail.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                worksheet.Cell(currentRow, 7).Value = "Description";
                worksheet.Cell(currentRow, 8).Value = "Unit";


                worksheet.Cell(currentRow, 9).Value = "Jan Qty";
                worksheet.Cell(currentRow, 10).Value = "Jan Harga";
                worksheet.Cell(currentRow, 11).Value = "Jan Total";

                worksheet.Cell(currentRow, 12).Value = "Feb Qty";
                worksheet.Cell(currentRow, 13).Value = "Feb Harga";
                worksheet.Cell(currentRow, 14).Value = "Feb Total";

                worksheet.Cell(currentRow, 15).Value = "Mar Qty";
                worksheet.Cell(currentRow, 16).Value = "Mar Harga";
                worksheet.Cell(currentRow, 17).Value = "Mar Total";

                worksheet.Cell(currentRow, 18).Value = "Apr Qty";
                worksheet.Cell(currentRow, 19).Value = "Apr Harga";
                worksheet.Cell(currentRow, 20).Value = "Apr Total";

                worksheet.Cell(currentRow, 21).Value = "Mei Qty";
                worksheet.Cell(currentRow, 22).Value = "Mei Harga";
                worksheet.Cell(currentRow, 23).Value = "Mei Total";

                worksheet.Cell(currentRow, 24).Value = "Jun Qty";
                worksheet.Cell(currentRow, 25).Value = "Jun Harga";
                worksheet.Cell(currentRow, 26).Value = "Jun Total";

                worksheet.Cell(currentRow, 27).Value = "Jul Qty";
                worksheet.Cell(currentRow, 28).Value = "Jul Harga";
                worksheet.Cell(currentRow, 29).Value = "Jul Total";

                worksheet.Cell(currentRow, 30).Value = "Agus Qty";
                worksheet.Cell(currentRow, 31).Value = "Agus Harga";
                worksheet.Cell(currentRow, 32).Value = "Agus Total";

                worksheet.Cell(currentRow, 33).Value = "Sep Qty";
                worksheet.Cell(currentRow, 34).Value = "Sep Harga";
                worksheet.Cell(currentRow, 35).Value = "Sep Total";

                worksheet.Cell(currentRow, 36).Value = "Okt Qty";
                worksheet.Cell(currentRow, 37).Value = "Okt Harga";
                worksheet.Cell(currentRow, 38).Value = "Okt Total";

                worksheet.Cell(currentRow, 39).Value = "Nov Qty";
                worksheet.Cell(currentRow, 40).Value = "Nov Harga";
                worksheet.Cell(currentRow, 41).Value = "Nov Total";

                worksheet.Cell(currentRow, 42).Value = "Des Qty";
                worksheet.Cell(currentRow, 43).Value = "Des Harga";
                worksheet.Cell(currentRow, 44).Value = "Des Total";


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i + 1;
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Keterangan"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Unit"];

                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["jan_qty"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["jan_harga"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Jan"];

                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["feb_qty"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["feb_harga"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Feb"];

                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["mar_qty"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["mar_harga"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Mar"];

                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["apr_qty"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["apr_harga"];
                    worksheet.Cell(currentRow, 20).Value = dt.Rows[i]["Apr"];

                    worksheet.Cell(currentRow, 21).Value = dt.Rows[i]["mei_qty"];
                    worksheet.Cell(currentRow, 22).Value = dt.Rows[i]["mei_harga"];
                    worksheet.Cell(currentRow, 23).Value = dt.Rows[i]["Mei"];

                    worksheet.Cell(currentRow, 24).Value = dt.Rows[i]["jun_qty"];
                    worksheet.Cell(currentRow, 25).Value = dt.Rows[i]["jun_harga"];
                    worksheet.Cell(currentRow, 26).Value = dt.Rows[i]["Jun"];

                    worksheet.Cell(currentRow, 27).Value = dt.Rows[i]["jul_qty"];
                    worksheet.Cell(currentRow, 28).Value = dt.Rows[i]["jul_harga"];
                    worksheet.Cell(currentRow, 29).Value = dt.Rows[i]["Jul"];

                    worksheet.Cell(currentRow, 30).Value = dt.Rows[i]["agt_qty"];
                    worksheet.Cell(currentRow, 31).Value = dt.Rows[i]["agt_harga"];
                    worksheet.Cell(currentRow, 32).Value = dt.Rows[i]["Ags"];

                    worksheet.Cell(currentRow, 33).Value = dt.Rows[i]["sep_qty"];
                    worksheet.Cell(currentRow, 34).Value = dt.Rows[i]["sep_harga"];
                    worksheet.Cell(currentRow, 35).Value = dt.Rows[i]["Sep"];

                    worksheet.Cell(currentRow, 36).Value = dt.Rows[i]["okt_qty"];
                    worksheet.Cell(currentRow, 37).Value = dt.Rows[i]["okt_harga"];
                    worksheet.Cell(currentRow, 38).Value = dt.Rows[i]["Okt"];

                    worksheet.Cell(currentRow, 39).Value = dt.Rows[i]["nov_qty"];
                    worksheet.Cell(currentRow, 40).Value = dt.Rows[i]["nov_harga"];
                    worksheet.Cell(currentRow, 41).Value = dt.Rows[i]["Nov"];

                    worksheet.Cell(currentRow, 42).Value = dt.Rows[i]["des_qty"];
                    worksheet.Cell(currentRow, 43).Value = dt.Rows[i]["des_harga"];
                    worksheet.Cell(currentRow, 44).Value = dt.Rows[i]["Des"];


                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }



        }
        
        [HttpPost]
        public ActionResult CreateDataToExcel_BudgetSummaryAccount(string tahun, string dept)
        {
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_BudgetSummaryAccount(dept, tahun, user);
            //dt.TableName = "Budget";
            string fileName = "Budget_Summary_Account_" + dept + ".xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                worksheet.Cell(currentRow, 7).Value = "Januari";
                worksheet.Cell(currentRow, 8).Value = "Februari";
                worksheet.Cell(currentRow, 9).Value = "Maret";
                worksheet.Cell(currentRow, 10).Value = "April";
                worksheet.Cell(currentRow, 11).Value = "Mei";
                worksheet.Cell(currentRow, 12).Value = "Juni";
                worksheet.Cell(currentRow, 13).Value = "Juli";
                worksheet.Cell(currentRow, 14).Value = "Agustus";
                worksheet.Cell(currentRow, 15).Value = "September";
                worksheet.Cell(currentRow, 16).Value = "Oktober";
                worksheet.Cell(currentRow, 17).Value = "November";
                worksheet.Cell(currentRow, 18).Value = "Desember";
                worksheet.Cell(currentRow, 19).Value = "Total";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Jan"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Feb"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["Mar"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["Apr"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Mei"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["Jun"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["Jul"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Agst"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["Sept"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["Okt"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Nov"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["Des"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["Subtotal"];

                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }


            //byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            //return File(fileByteArray, "application/vnd.ms-excel", fileName);


        }

        [HttpPost]
        public ActionResult CreateDataToExcel_BudgetSummaryAccount_All(string tahun, string dept)
        {
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_BudgetSummaryAccount_All(dept, tahun, user);
            //dt.TableName = "Budget";
            string fileName = "Budget_Summary_Account_All.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                worksheet.Cell(currentRow, 7).Value = "Januari";
                worksheet.Cell(currentRow, 8).Value = "Februari";
                worksheet.Cell(currentRow, 9).Value = "Maret";
                worksheet.Cell(currentRow, 10).Value = "April";
                worksheet.Cell(currentRow, 11).Value = "Mei";
                worksheet.Cell(currentRow, 12).Value = "Juni";
                worksheet.Cell(currentRow, 13).Value = "Juli";
                worksheet.Cell(currentRow, 14).Value = "Agustus";
                worksheet.Cell(currentRow, 15).Value = "September";
                worksheet.Cell(currentRow, 16).Value = "Oktober";
                worksheet.Cell(currentRow, 17).Value = "November";
                worksheet.Cell(currentRow, 18).Value = "Desember";
                worksheet.Cell(currentRow, 19).Value = "Total";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Jan"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Feb"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["Mar"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["Apr"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Mei"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["Jun"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["Jul"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Agst"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["Sept"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["Okt"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Nov"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["Des"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["Subtotal"];

                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }


            //byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            //return File(fileByteArray, "application/vnd.ms-excel", fileName);


        }

        public ActionResult CreateDataToExcel_BudgetDetail(string tahun, string dept,string account)
        {
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_BudgetDetail(dept, tahun, account);
            string fileName = "Budget_Detail_" + dept + ".xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                worksheet.Cell(currentRow, 7).Value = "Description";
                worksheet.Cell(currentRow, 8).Value = "Unit";


                worksheet.Cell(currentRow, 9).Value = "Jan Qty";
                worksheet.Cell(currentRow, 10).Value = "Jan Harga";
                worksheet.Cell(currentRow, 11).Value = "Jan Total";

                worksheet.Cell(currentRow, 12).Value = "Feb Qty";
                worksheet.Cell(currentRow, 13).Value = "Feb Harga";
                worksheet.Cell(currentRow, 14).Value = "Feb Total";

                worksheet.Cell(currentRow, 15).Value = "Mar Qty";
                worksheet.Cell(currentRow, 16).Value = "Mar Harga";
                worksheet.Cell(currentRow, 17).Value = "Mar Total";

                worksheet.Cell(currentRow, 18).Value = "Apr Qty";
                worksheet.Cell(currentRow, 19).Value = "Apr Harga";
                worksheet.Cell(currentRow, 20).Value = "Apr Total";

                worksheet.Cell(currentRow, 21).Value = "Mei Qty";
                worksheet.Cell(currentRow, 22).Value = "Mei Harga";
                worksheet.Cell(currentRow, 23).Value = "Mei Total";

                worksheet.Cell(currentRow, 24).Value = "Jun Qty";
                worksheet.Cell(currentRow, 25).Value = "Jun Harga";
                worksheet.Cell(currentRow, 26).Value = "Jun Total";

                worksheet.Cell(currentRow, 27).Value = "Jul Qty";
                worksheet.Cell(currentRow, 28).Value = "Jul Harga";
                worksheet.Cell(currentRow, 29).Value = "Jul Total";

                worksheet.Cell(currentRow, 30).Value = "Agus Qty";
                worksheet.Cell(currentRow, 31).Value = "Agus Harga";
                worksheet.Cell(currentRow, 32).Value = "Agus Total";

                worksheet.Cell(currentRow, 33).Value = "Sep Qty";
                worksheet.Cell(currentRow, 34).Value = "Sep Harga";
                worksheet.Cell(currentRow, 35).Value = "Sep Total";

                worksheet.Cell(currentRow, 36).Value = "Okt Qty";
                worksheet.Cell(currentRow, 37).Value = "Okt Harga";
                worksheet.Cell(currentRow, 38).Value = "Okt Total";

                worksheet.Cell(currentRow, 39).Value = "Nov Qty";
                worksheet.Cell(currentRow, 40).Value = "Nov Harga";
                worksheet.Cell(currentRow, 41).Value = "Nov Total";

                worksheet.Cell(currentRow, 42).Value = "Des Qty";
                worksheet.Cell(currentRow, 43).Value = "Des Harga";
                worksheet.Cell(currentRow, 44).Value = "Des Total";             


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i + 1;
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Keterangan"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Unit"];

                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["jan_qty"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["jan_harga"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Jan"];

                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["feb_qty"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["feb_harga"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Feb"];

                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["mar_qty"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["mar_harga"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Mar"];

                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["apr_qty"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["apr_harga"];
                    worksheet.Cell(currentRow, 20).Value = dt.Rows[i]["Apr"];

                    worksheet.Cell(currentRow, 21).Value = dt.Rows[i]["mei_qty"];
                    worksheet.Cell(currentRow, 22).Value = dt.Rows[i]["mei_harga"];
                    worksheet.Cell(currentRow, 23).Value = dt.Rows[i]["Mei"];
                
                    worksheet.Cell(currentRow, 24).Value = dt.Rows[i]["jun_qty"];
                    worksheet.Cell(currentRow, 25).Value = dt.Rows[i]["jun_harga"];
                    worksheet.Cell(currentRow, 26).Value = dt.Rows[i]["Jun"];

                    worksheet.Cell(currentRow, 27).Value = dt.Rows[i]["jul_qty"];
                    worksheet.Cell(currentRow, 28).Value = dt.Rows[i]["jul_harga"];
                    worksheet.Cell(currentRow, 29).Value = dt.Rows[i]["Jul"];

                    worksheet.Cell(currentRow, 30).Value = dt.Rows[i]["agt_qty"];
                    worksheet.Cell(currentRow, 31).Value = dt.Rows[i]["agt_harga"];
                    worksheet.Cell(currentRow, 32).Value = dt.Rows[i]["Ags"];

                    worksheet.Cell(currentRow, 33).Value = dt.Rows[i]["sep_qty"];
                    worksheet.Cell(currentRow, 34).Value = dt.Rows[i]["sep_harga"];
                    worksheet.Cell(currentRow, 35).Value = dt.Rows[i]["Sep"];

                    worksheet.Cell(currentRow, 36).Value = dt.Rows[i]["okt_qty"];
                    worksheet.Cell(currentRow, 37).Value = dt.Rows[i]["okt_harga"];
                    worksheet.Cell(currentRow, 38).Value = dt.Rows[i]["Okt"];

                    worksheet.Cell(currentRow, 39).Value = dt.Rows[i]["nov_qty"];
                    worksheet.Cell(currentRow, 40).Value = dt.Rows[i]["nov_harga"];
                    worksheet.Cell(currentRow, 41).Value = dt.Rows[i]["Nov"];

                    worksheet.Cell(currentRow, 42).Value = dt.Rows[i]["des_qty"];
                    worksheet.Cell(currentRow, 43).Value = dt.Rows[i]["des_harga"];
                    worksheet.Cell(currentRow, 44).Value = dt.Rows[i]["Des"];


                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
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

        //belum ke pake
        public ActionResult UploadExcelFirst(HttpPostedFileBase file)
        {
            string path = "";
            DataTable dt = new DataTable();
            //Checking file content length and Extension must be .xlsx  
            if (file != null && file.ContentLength > 0 && System.IO.Path.GetExtension(file.FileName).ToLower() == ".xlsx")
            {
                path = Path.Combine(Server.MapPath("~/temp"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

            }
            else
            {
                //If file extension of the uploaded file is different then .xlsx  
                ViewBag.Message = "Please select file with .xlsx extension!";
            }

            var data = path;
            return Json(new { redirectUrl = "/lihatbudget/" + data });


        }

        public ActionResult TestA(DataTable dt)
        {

            return View();

        }

        //[HttpGet]
        //public ActionResult UploadExcel()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase Formdata)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (Formdata != null && Formdata.ContentLength > 0 && System.IO.Path.GetExtension(Formdata.FileName).ToLower() == ".xlsx")
            {
                string path = Path.Combine(Server.MapPath("~/temp"), Path.GetFileName(Formdata.FileName));
                Formdata.SaveAs(path);
                using (XLWorkbook workbook = new XLWorkbook(path))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);
                    bool FirstRow = true;
                    //Range for reading the cells based on the last cell used.  
                    string readRange = "1:1";
                    foreach (IXLRow row in worksheet.RowsUsed())
                    {
                        //If Reading the First Row (used) then add them as column name  
                        if (FirstRow)
                        {
                            //Checking the Last cellused for column generation in datatable  
                            readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                cell.Value = cell.Value.ToString().Trim();
                                dt.Columns.Add((cell.Value.ToString()).Trim());
                            }
                            FirstRow = false;
                        }
                        else
                        {
                            //Adding a Row in datatable  
                            dt.Rows.Add();
                            int cellIndex = 0;
                            //Updating the values of datatable  
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                cell.Value = cell.Value.ToString().Trim();
                                dt.Rows[dt.Rows.Count - 1][cellIndex] = ((cell.Value).ToString()).Trim();
                                cellIndex++;
                            }
                        }
                    }



                    //If no data in Excel file  
                    if (FirstRow)
                    {
                        ViewBag.Message = "Empty Excel File!";
                    }


                }
            }
            else
            {
                //If file extension of the uploaded file is different then .xlsx  
                ViewBag.Message = "Please select file with .xlsx extension!";
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt1.Rows.Add(dt);
            }

            return View(dt1);


        }

        [HttpGet]
        public ActionResult Import(string tahun,string semester)
        {
            ViewData["ListBudgetOpen1"] = service.ListBudgetOpen("Open");
            DataTable dt = service.CreateExcell("xxx", tahun);
            return View(dt);
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            DataTable dt = new DataTable();
            if (file != null && file.ContentLength > 0 && (System.IO.Path.GetExtension(file.FileName).ToLower() == ".xlsx" || System.IO.Path.GetExtension(file.FileName).ToLower() == ".xls"))
            {
                string path = Path.Combine(Server.MapPath("~/temp"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                using (XLWorkbook workbook = new XLWorkbook(path))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);
                    bool FirstRow = true;
                    //Range for reading the cells based on the last cell used.  
                    string readRange = "1:1";
                    foreach (IXLRow row in worksheet.RowsUsed())
                    {
                        //If Reading the First Row (used) then add them as column name  
                        if (FirstRow)
                        {
                            //Checking the Last cellused for column generation in datatable  
                            readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            FirstRow = false;
                        }
                        else
                        {
                            //Adding a Row in datatable  
                            dt.Rows.Add();
                            int cellIndex = 0;
                            //Updating the values of datatable  
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                cellIndex++;
                            }
                        }
                    }



                    //If no data in Excel file  
                    //if (FirstRow)
                    //{
                    //    ViewBag.Message = "Empty Excel File!";
                    //}


                }
            }
            //else
            //{
            //    //If file extension of the uploaded file is different then .xlsx  
            //    ViewBag.Message = "Please select file with .xlsx extension!";
            //}

            ViewData["ListBudgetOpen1"] = service.ListBudgetOpen("Open");
            return View(dt);
        }

        [HttpPost]
        public JsonResult Simpan_Upload(BO_EBudget data)
        {
            try

            {
                
                if (data.OpenSemester.ToString()  == "1")
                {
                   
                    foreach (var Item in data.BD)
                    {
                        int hasil = service.CekBarang(Item.IdNo.ToString(), Item.Tahun.ToString());
                        if (hasil == 0)
                        {
                            service.SimpanUploadExcel(Item);
                        }
                        else
                        {
                            service.EditUploadExcel(Item);
                        }
                    }
                }
                else if (data.OpenSemester.ToString() == "2")
                {
                    foreach (var Item in data.BD)
                    {
                        int hasil = service.CekBarang(Item.IdNo.ToString(), Item.Tahun.ToString());
                        if (hasil == 0)
                        {
                            service.SimpanUploadExcel_S2(Item);
                        }
                        else
                        {
                            service.EditUploadExcel_S2(Item);
                        }
                    }
                }

                return Json(new { success = true, message = "Data Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
               
        public ActionResult BudgetDept()
        {
            List<ComboList> listYear = new List<ComboList>();
            //List<ListBudgetOpen> ListBudgetOpen = new List<ListBudgetOpen>();
            int yearEnd = DateTime.Now.Year + 1;
            for (int i = yearEnd; i >= 2017; i--)
            {
                listYear.Add(new ComboList
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }


            //ViewData["Filter"] = service.Filter(id, "", "");
            ViewData["ComboTahun"] = listYear;
            ViewBag.Title = "Budget Departemen";
            return View();
        }

        [HttpGet]
        public ActionResult Budgetdeptsummary(string id)
        {
            List<ComboList> listYear = new List<ComboList>();
            //List<ListBudgetOpen> ListBudgetOpen = new List<ListBudgetOpen>();
            
                listYear.Add(new ComboList
                {
                    Value = dHelper.DecryptURL(id),
                    Text = dHelper.DecryptURL(id)
                });
            ViewData["Tahun"] = listYear;
            ViewBag.Title = "Budget Departemen tahun - " + dHelper.DecryptURL(id) ;
            return View();
        }

        public ActionResult LoadBudgetDeptSummary(string Tahun)
        {
            var id = Tahun.ToString();
            return Json(new { redirectUrl = "/budgetdeptsummary/" + id });
        }

        public ActionResult GetDeptSummary([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string id)
        {
            Budgetakses.Dept = dHelper.CurrentLoginDeptID();
            Budgetakses.UserName = dHelper.CurrentLoginUserName();

            var query = service.GetSummaryBudgetDept(id, Budgetakses.Dept, Budgetakses.UserName).AsQueryable();
            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.DeptID.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.No,
                Module.DeptID,
                Module.DeptName,
                Module.SiteID,
                Module.Tahun,
                Module.Subtotal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }


        public ActionResult LihatBudgetPresentasi(string Tahun, string DeptId)
        {
            List<CurrentLogin> Userlogin = new List<CurrentLogin>();
            Userlogin.Add(new CurrentLogin
            {
                Dept = dHelper.CurrentLoginDeptID(),
                UserName = dHelper.CurrentLoginUserName()
            });
            ViewData["userlogin"] = Userlogin;

            ViewData["Filter"] = service.Filter(dHelper.DecryptURL(Tahun),dHelper.DecryptURL(DeptId), "");
            ViewBag.Title = "Budget Tahun  " + dHelper.DecryptURL(Tahun) + "  -  " + dHelper.DecryptURL(DeptId);
            return View();
        }

        public ActionResult GetBudgetPresentasi([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string tahun, string dept)
        {

            //var dept = Settings.Instance.Dept;
            //var User_ = Settings.Instance.UserName;
            Budgetakses.UserName = dHelper.CurrentLoginUserName();
            var query = service.GetBudgetPresentasi(tahun, dept, Budgetakses.UserName).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.AcctID.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.RowNumber,
                Module.No,
                Module.AcctID,
                Module.AcctName,
                Module.Tahun,
                Module.DeptID,
                Module.Jan,
                Module.Feb,
                Module.Mar,
                Module.Apr,
                Module.Mei,
                Module.Jun,
                Module.Jul,
                Module.Agst,
                Module.Sept,
                Module.Okt,
                Module.Nov,
                Module.Des,
                Module.Subtotal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateDataToExcel_ProfitLost(string tahun)
        {
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_ProfitLost(tahun);
            //dt.TableName = "Budget";
            string fileName = "Profitlost.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("Budget");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "No.Acct";
                worksheet.Cell(currentRow, 3).Value = "Nama Account";
                worksheet.Cell(currentRow, 4).Value = "Januari";
                worksheet.Cell(currentRow, 5).Value = "Februari";
                worksheet.Cell(currentRow, 6).Value = "Maret";
                worksheet.Cell(currentRow, 7).Value = "April";
                worksheet.Cell(currentRow, 8).Value = "Mei";
                worksheet.Cell(currentRow, 9).Value = "Juni";
                worksheet.Cell(currentRow, 10).Value = "Semester1";
                worksheet.Cell(currentRow, 11).Value = "Juli";
                worksheet.Cell(currentRow, 12).Value = "Agustus";
                worksheet.Cell(currentRow, 13).Value = "September";
                worksheet.Cell(currentRow, 14).Value = "Oktober";
                worksheet.Cell(currentRow, 15).Value = "November";
                worksheet.Cell(currentRow, 16).Value = "Desember";
                worksheet.Cell(currentRow, 17).Value = "Semester2";
                worksheet.Cell(currentRow, 18).Value = "Total";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["ID"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["AcctID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["Januari"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["Februari"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Maret"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["April"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Mei"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["Juni"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["Semester1"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Juli"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["Agustus"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["September"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Oktober"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["November"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["Desember"];                  
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Semester2"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["Total"];

                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }


            //byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            //return File(fileByteArray, "application/vnd.ms-excel", fileName);


        }

        public ActionResult testRollBack()
        {
            ViewBag.message = "OK";
            return View();
        }

        public ActionResult loadtestRollBack()
        {
            service.testRollback();
            return Json(new { redirectUrl = "/test" });
        }

        #region Budget vs Actual
        public ActionResult BudgetvsActualDept()
        {
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

            ViewData["ComboTahun"] = listYear;
            ViewBag.Title = "Budget vs Actual Dept";
            return View();
        }

        public ActionResult LihatBudgetvsActual(string id)
        {
            ViewData["Filter"] = service.Filter(id, "", "");
            ViewBag.Title = "Budget Tahun  " + id;
            return View();
        }

        public ActionResult LoadBudgetvsActualDeptSummary(string Tahun)
        {
            var id = Tahun.ToString();
            return Json(new { redirectUrl = "/budgetvsactualdeptsummary/" + id });
        }

        [HttpGet]
        public ActionResult BudgetvsActualdeptsummary(string id)
        {
            List<ComboList> listYear = new List<ComboList>();

            listYear.Add(new ComboList
            {
                Value = dHelper.DecryptURL(id),
                Text = dHelper.DecryptURL(id)
            });
            ViewData["Tahun"] = listYear;
            ViewBag.Title = "Budget vs Actual Departemen Tahun - " + dHelper.DecryptURL(id);
            return View();
        }

        public ActionResult GetActualDeptSummary([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string id)
        {
            Budgetakses.Dept = dHelper.CurrentLoginDeptID();
            Budgetakses.UserName = dHelper.CurrentLoginUserName();

            var query = service.GetSummaryBudgetvsActualDept(id, Budgetakses.Dept, Budgetakses.UserName).AsQueryable();
            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.DeptID.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.No,
                Module.DeptID,
                Module.DeptName,
                Module.SiteID,
                Module.Tahun,
                Module.BudgetSubtotal,
                Module.ActualSubtotal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LihatBudgetvsActualPresentasi(string Tahun, string DeptId)
        {
            List<CurrentLogin> Userlogin = new List<CurrentLogin>();
            Userlogin.Add(new CurrentLogin
            {
                Dept = dHelper.CurrentLoginDeptID(),
                UserName = dHelper.CurrentLoginUserName()
            });
            ViewData["userlogin"] = Userlogin;

            ViewData["Filter"] = service.Filter(dHelper.DecryptURL(Tahun), dHelper.DecryptURL(DeptId), "");
            ViewBag.Title = "Budget vs Actual Tahun  " + dHelper.DecryptURL(Tahun) + "  -  " + dHelper.DecryptURL(DeptId);
            return View();
        }

        public ActionResult GetBudgetvsActualPresentasi([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string tahun, string dept)
        {

            Budgetakses.UserName = dHelper.CurrentLoginUserName();
            var query = service.GetBudgetvsActualPresentasi(tahun, dept, Budgetakses.UserName).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.AcctID.ToLower().ToString().Contains(value.ToString().ToLower())
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
                Module.RowNumber,
                Module.No,
                Module.AcctID,
                Module.AcctName,
                Module.Tahun,
                Module.DeptID,
                Module.JanBudget,
                Module.FebBudget,
                Module.MarBudget,
                Module.AprBudget,
                Module.MeiBudget,
                Module.JunBudget,
                Module.JulBudget,
                Module.AgtBudget,
                Module.SepBudget,
                Module.OktBudget,
                Module.NovBudget,
                Module.DesBudget,
                Module.BudgetSubtotal,
                Module.JanActual,
                Module.FebActual,
                Module.MarActual,
                Module.AprActual,
                Module.MeiActual,
                Module.JunActual,
                Module.JulActual,
                Module.AgtActual,
                Module.SepActual,
                Module.OktActual,
                Module.NovActual,
                Module.DesActual,
                Module.ActualSubtotal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateDataToExcel_ActualSummaryDept(string tahun)
        {
            string Deptid = dHelper.CurrentLoginDeptID();
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_ActualSummaryDept(Deptid, tahun, user);
            string fileName = "BudgetvsActual_Summary_Dept.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook())  
            {
                IXLWorksheet worksheet = wb.Worksheets.Add("BudgetvsActual");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "DeptID";
                worksheet.Cell(currentRow, 3).Value = "Desc";
                worksheet.Cell(currentRow, 4).Value = "SiteID";
                worksheet.Cell(currentRow, 5).Value = "Tahun";
                worksheet.Cell(currentRow, 6).Value = "BudgetSubtotal";
                worksheet.Cell(currentRow, 7).Value = "ActualSubtotal";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["Desc"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["BudgetSubtotal"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["ActualSubtotal"];
                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }
        }

        [HttpPost]
        public ActionResult CreateDataToExcel_BudgetvsActualSummaryAccount(string tahun, string dept)
        {
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_BudgetvsActualSummaryAccount(dept, tahun, user);
            string fileName = "BudgetvsActual_Summary_Account_" + dept + ".xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook())
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("BudgetvsActual");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                worksheet.Cell(currentRow, 7).Value = "Jan Budget";
                worksheet.Cell(currentRow, 8).Value = "Jan Actual";
                worksheet.Cell(currentRow, 9).Value = "Feb Budget";
                worksheet.Cell(currentRow, 10).Value = "Feb Actual";
                worksheet.Cell(currentRow, 11).Value = "Mar Budget";
                worksheet.Cell(currentRow, 12).Value = "Mar Actual";
                worksheet.Cell(currentRow, 13).Value = "Apr Budget";
                worksheet.Cell(currentRow, 14).Value = "Apr Actual";
                worksheet.Cell(currentRow, 15).Value = "Mei Budget";
                worksheet.Cell(currentRow, 16).Value = "Mei Actual";
                worksheet.Cell(currentRow, 17).Value = "Jun Budget";
                worksheet.Cell(currentRow, 18).Value = "Jun Actual";
                worksheet.Cell(currentRow, 19).Value = "Jul Budget";
                worksheet.Cell(currentRow, 20).Value = "Jul Actual";
                worksheet.Cell(currentRow, 21).Value = "Agt Budget";
                worksheet.Cell(currentRow, 22).Value = "Agt Actual";
                worksheet.Cell(currentRow, 23).Value = "Sep Budget";
                worksheet.Cell(currentRow, 24).Value = "Sep Actual";
                worksheet.Cell(currentRow, 25).Value = "Okt Budget";
                worksheet.Cell(currentRow, 26).Value = "Okt Actual";
                worksheet.Cell(currentRow, 27).Value = "Nov Budget";
                worksheet.Cell(currentRow, 28).Value = "Nov Actual";
                worksheet.Cell(currentRow, 29).Value = "Des Budget";
                worksheet.Cell(currentRow, 30).Value = "Des Actual";
                worksheet.Cell(currentRow, 31).Value = "Total Budget";
                worksheet.Cell(currentRow, 32).Value = "Total Actual";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["Acct"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["JanBudget"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["JanActual"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["FebBudget"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["FebActual"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["MarBudget"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["MarActual"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["AprBudget"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["AprActual"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["MeiBudget"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["MeiActual"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["JunBudget"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["JunActual"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["JulBudget"];
                    worksheet.Cell(currentRow, 20).Value = dt.Rows[i]["JulActual"];
                    worksheet.Cell(currentRow, 21).Value = dt.Rows[i]["AgtBudget"];
                    worksheet.Cell(currentRow, 22).Value = dt.Rows[i]["AgtActual"];
                    worksheet.Cell(currentRow, 23).Value = dt.Rows[i]["SepBudget"];
                    worksheet.Cell(currentRow, 24).Value = dt.Rows[i]["SepActual"];
                    worksheet.Cell(currentRow, 25).Value = dt.Rows[i]["OktBudget"];
                    worksheet.Cell(currentRow, 26).Value = dt.Rows[i]["OktActual"];
                    worksheet.Cell(currentRow, 27).Value = dt.Rows[i]["NovBudget"];
                    worksheet.Cell(currentRow, 28).Value = dt.Rows[i]["NovActual"];
                    worksheet.Cell(currentRow, 29).Value = dt.Rows[i]["DesBudget"];
                    worksheet.Cell(currentRow, 30).Value = dt.Rows[i]["DesActual"];
                    worksheet.Cell(currentRow, 31).Value = dt.Rows[i]["BudgetSubtotal"];
                    worksheet.Cell(currentRow, 32).Value = dt.Rows[i]["ActualSubtotal"];

                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }
        }

        [HttpPost]
        public ActionResult CreateDataToExcel_BudgetvsActualSummaryAccount_All(string tahun, string dept)
        {
            string user = dHelper.CurrentLoginUserName();
            DataTable dt = service.CreateDataToExcel_BudgetvsActualSummaryAccount_All("ALL", tahun, user);
            string fileName = "BudgetvsActual_Summary_Account_All.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            using (XLWorkbook wb = new XLWorkbook()) 
            {

                IXLWorksheet worksheet = wb.Worksheets.Add("BudgetvsActual");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "AcctID";
                worksheet.Cell(currentRow, 3).Value = "AcctName";
                worksheet.Cell(currentRow, 4).Value = "DeptID";
                worksheet.Cell(currentRow, 5).Value = "SiteID";
                worksheet.Cell(currentRow, 6).Value = "Tahun";
                //worksheet.Cell(currentRow, 7).Value = "Januari";
                //worksheet.Cell(currentRow, 8).Value = "Februari";
                //worksheet.Cell(currentRow, 9).Value = "Maret";
                //worksheet.Cell(currentRow, 10).Value = "April";
                //worksheet.Cell(currentRow, 11).Value = "Mei";
                //worksheet.Cell(currentRow, 12).Value = "Juni";
                //worksheet.Cell(currentRow, 13).Value = "Juli";
                //worksheet.Cell(currentRow, 14).Value = "Agustus";
                //worksheet.Cell(currentRow, 15).Value = "September";
                //worksheet.Cell(currentRow, 16).Value = "Oktober";
                //worksheet.Cell(currentRow, 17).Value = "November";
                //worksheet.Cell(currentRow, 18).Value = "Desember";
                //worksheet.Cell(currentRow, 19).Value = "Total";
                worksheet.Cell(currentRow, 7).Value = "Jan Budget";
                worksheet.Cell(currentRow, 8).Value = "Jan Actual";
                worksheet.Cell(currentRow, 9).Value = "Feb Budget";
                worksheet.Cell(currentRow, 10).Value = "Feb Actual";
                worksheet.Cell(currentRow, 11).Value = "Mar Budget";
                worksheet.Cell(currentRow, 12).Value = "Mar Actual";
                worksheet.Cell(currentRow, 13).Value = "Apr Budget";
                worksheet.Cell(currentRow, 14).Value = "Apr Actual";
                worksheet.Cell(currentRow, 15).Value = "Mei Budget";
                worksheet.Cell(currentRow, 16).Value = "Mei Actual";
                worksheet.Cell(currentRow, 17).Value = "Jun Budget";
                worksheet.Cell(currentRow, 18).Value = "Jun Actual";
                worksheet.Cell(currentRow, 19).Value = "Jul Budget";
                worksheet.Cell(currentRow, 20).Value = "Jul Actual";
                worksheet.Cell(currentRow, 21).Value = "Agt Budget";
                worksheet.Cell(currentRow, 22).Value = "Agt Actual";
                worksheet.Cell(currentRow, 23).Value = "Sep Budget";
                worksheet.Cell(currentRow, 24).Value = "Sep Actual";
                worksheet.Cell(currentRow, 25).Value = "Okt Budget";
                worksheet.Cell(currentRow, 26).Value = "Okt Actual";
                worksheet.Cell(currentRow, 27).Value = "Nov Budget";
                worksheet.Cell(currentRow, 28).Value = "Nov Actual";
                worksheet.Cell(currentRow, 29).Value = "Des Budget";
                worksheet.Cell(currentRow, 30).Value = "Des Actual";
                worksheet.Cell(currentRow, 31).Value = "Total Budget";
                worksheet.Cell(currentRow, 32).Value = "Total Actual";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = dt.Rows[i]["No"];
                    worksheet.Cell(currentRow, 2).Value = dt.Rows[i]["Acct"];
                    worksheet.Cell(currentRow, 3).Value = dt.Rows[i]["AcctName"];
                    worksheet.Cell(currentRow, 4).Value = dt.Rows[i]["DeptID"];
                    worksheet.Cell(currentRow, 5).Value = dt.Rows[i]["SiteID"];
                    worksheet.Cell(currentRow, 6).Value = dt.Rows[i]["Tahun"];
                    //worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["Jan"];
                    //worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["Feb"];
                    //worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["Mar"];
                    //worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["Apr"];
                    //worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["Mei"];
                    //worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["Jun"];
                    //worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["Jul"];
                    //worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["Agst"];
                    //worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["Sept"];
                    //worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["Okt"];
                    //worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["Nov"];
                    //worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["Des"];
                    //worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["Subtotal"];
                    worksheet.Cell(currentRow, 7).Value = dt.Rows[i]["JanBudget"];
                    worksheet.Cell(currentRow, 8).Value = dt.Rows[i]["JanActual"];
                    worksheet.Cell(currentRow, 9).Value = dt.Rows[i]["FebBudget"];
                    worksheet.Cell(currentRow, 10).Value = dt.Rows[i]["FebActual"];
                    worksheet.Cell(currentRow, 11).Value = dt.Rows[i]["MarBudget"];
                    worksheet.Cell(currentRow, 12).Value = dt.Rows[i]["MarActual"];
                    worksheet.Cell(currentRow, 13).Value = dt.Rows[i]["AprBudget"];
                    worksheet.Cell(currentRow, 14).Value = dt.Rows[i]["AprActual"];
                    worksheet.Cell(currentRow, 15).Value = dt.Rows[i]["MeiBudget"];
                    worksheet.Cell(currentRow, 16).Value = dt.Rows[i]["MeiActual"];
                    worksheet.Cell(currentRow, 17).Value = dt.Rows[i]["JunBudget"];
                    worksheet.Cell(currentRow, 18).Value = dt.Rows[i]["JunActual"];
                    worksheet.Cell(currentRow, 19).Value = dt.Rows[i]["JulBudget"];
                    worksheet.Cell(currentRow, 20).Value = dt.Rows[i]["JulActual"];
                    worksheet.Cell(currentRow, 21).Value = dt.Rows[i]["AgtBudget"];
                    worksheet.Cell(currentRow, 22).Value = dt.Rows[i]["AgtActual"];
                    worksheet.Cell(currentRow, 23).Value = dt.Rows[i]["SepBudget"];
                    worksheet.Cell(currentRow, 24).Value = dt.Rows[i]["SepActual"];
                    worksheet.Cell(currentRow, 25).Value = dt.Rows[i]["OktBudget"];
                    worksheet.Cell(currentRow, 26).Value = dt.Rows[i]["OktActual"];
                    worksheet.Cell(currentRow, 27).Value = dt.Rows[i]["NovBudget"];
                    worksheet.Cell(currentRow, 28).Value = dt.Rows[i]["NovActual"];
                    worksheet.Cell(currentRow, 29).Value = dt.Rows[i]["DesBudget"];
                    worksheet.Cell(currentRow, 30).Value = dt.Rows[i]["DesActual"];
                    worksheet.Cell(currentRow, 31).Value = dt.Rows[i]["BudgetSubtotal"];
                    worksheet.Cell(currentRow, 32).Value = dt.Rows[i]["ActualSubtotal"];
                }
                worksheet.Columns().AdjustToContents();
                wb.SaveAs(fullPath);
                return Json(new { success = true, namafile = fileName });
            }
        }

        public ActionResult ShowLihatBudgetvsActualDetail(string AcctId, string Tahun, string DeptID)
        {
            List<CurrentLogin> Userlogin = new List<CurrentLogin>();
            Userlogin.Add(new CurrentLogin
            {
                Dept = dHelper.CurrentLoginDeptID(),
                UserName = dHelper.CurrentLoginUserName()
            });
            ViewData["userlogin"] = Userlogin;

            ViewData["ListBudgetOpen"] = service.ListBudgetOpen("Open");
            ViewData["FilterDetailBudget"] = service.FilterDetailBudget(dHelper.DecryptURL(Tahun), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(AcctId));
            GetBudgetDetail2(dHelper.DecryptURL(AcctId), dHelper.DecryptURL(DeptID), dHelper.DecryptURL(Tahun));
            ViewBag.Title = "Budget Tahun  " + dHelper.DecryptURL(Tahun) + "   -   " + dHelper.DecryptURL(DeptID) + "   -   " + dHelper.DecryptURL(AcctId);
            return View();
        }
        #endregion        
    }
}