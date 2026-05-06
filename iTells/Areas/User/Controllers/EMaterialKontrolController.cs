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
//using DataObjects.Helpers;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace iTells.Areas.User.Controllers
{
    public class EMaterialKontrolController : Controller
    {
        // GET: User/EMaterialKontrol
        GeneralHelper dHelper = new GeneralHelper();
        IMaterialKontrolService service { get; set; }
        //public PRController() : this(new PRAksesUser(), new PRService(), new ComboListService()) { }
        public EMaterialKontrolController() : this( new MaterialKontrolService()) { }

        public EMaterialKontrolController(IMaterialKontrolService service)
        {
            //this.prakses = prakses;
            this.service = service;
            //this.comboListService = comboListService;
        }
        public ActionResult Index()
        {

            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            List<ComboList> listYear = new List<ComboList>();
            //List<ListBudgetOpen> ListBudgetOpen = new List<ListBudgetOpen>();
            var id = "2022";
            listYear.Add(new ComboList
            {
                Value = dHelper.DecryptURL(id),
                Text = dHelper.DecryptURL(id)
            });

            ViewData["Tahun"] = listYear;
            //List<BO_MaterialKontrol> ListMaterialStok = new List<BO_MaterialKontrol>();
            //ListMaterialStok = service.GetStokMaterial(dHelper.DecryptURL(id), "");
            //ViewData["StokMaterial"] = ListMaterialStok;
            ViewBag.Title = "Kontrol Stok - " + dHelper.DecryptURL(id);

            return View();
        }

        public ActionResult loadStokMaterial(string Tahun, string account)
        {
            var id = Tahun.ToString();
            //var _account = _account.ToString();
            return Json(new { redirectUrl = "/StokMaterial/" + id + "/account/" + account });
        }


        public ActionResult StokMaterial(string id,string account,int bulan)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            List<ComboList> listYear = new List<ComboList>();
            //List<ListBudgetOpen> ListBudgetOpen = new List<ListBudgetOpen>();

            listYear.Add(new ComboList
            {
                //Value = dHelper.DecryptURL(id),
                //Text = dHelper.DecryptURL(id)
                Value = (id),
                Text = (id)
            });

            //int bulan = Convert.ToInt16((DateTime.Now.Month).ToString());
            string DescBulan = "";

            if (bulan == 1)
            {
                DescBulan = "Januari";
            }
            else if (bulan == 2)
            {
                DescBulan = "Februari";
            }
            else if (bulan == 3)
            {
                DescBulan = "Maret";
            }
            else if (bulan == 4)
            {
                DescBulan = "April";
            }
            else if (bulan == 5)
            {
                DescBulan = "Mei";
            }
            else if (bulan == 6)
            {
                DescBulan = "Juni";
            }
            else if (bulan == 7)
            {
                DescBulan = "Juli";
            }
            else if (bulan == 8)
            {
                DescBulan = "Agustus";
            }
            else if (bulan == 9)
            {
                DescBulan = "September";
            }
            else if (bulan == 10)
            {
                DescBulan = "Oktober";
            }
            else if (bulan == 11)
            {
                DescBulan = "November";
            }
            else if (bulan == 12)
            {
                DescBulan = "Desember";
            }


            string Ket = "";
            //Ket = dHelper.DecryptURL(account);
            Ket = account;

            if (Ket == "11620")
            {
                ViewBag.Title = "Stok Material Plastik - " + id;
            }
            else if (Ket == "11621")
            {
                ViewBag.Title = "Stok Material Painting - " + id;
            }
            else
            {
                ViewBag.Title = "Stok Supplies Parts - " + id;
            }


            ViewData["Tahun"] = listYear;
            ViewBag.Account = account;
            ViewBag.kolom = bulan;
            //GetData1(dHelper.DecryptURL(id), dHelper.DecryptURL(account),bulan, DescBulan);
            GetData1(id, account,bulan, DescBulan);

            return View();
        }

        public ActionResult GetData1(string id, string account,int bulan,string bulan2)

        {
            try

            {
                List<BO_MaterialKontrol> ListMaterialStok = new List<BO_MaterialKontrol>();
                ListMaterialStok = service.GetStokMaterial(id, account, bulan);
                ViewData["Januari"] = ListMaterialStok;
                ViewBag.Bulan = bulan2;
                ViewBag.Kolom = bulan;
                return PartialView("Partial/Partial1") ;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult GetDataByAccountlist(string Year)

        {
            try

            {
                List<BO_MaterialKontrol_ByAccount> ListMaterialStok_ByAccount = new List<BO_MaterialKontrol_ByAccount>();
                ListMaterialStok_ByAccount = service.GetStokMaterial_ByAccountList(Year);
                ViewData["DataByAccount"] = ListMaterialStok_ByAccount;
                ViewBag.Year = Year;
                //ViewBag.Kolom = bulan;
                return PartialView("Partial/Partial10");

            }
            catch (Exception)
            {

                throw;
            }

        }



        public ActionResult loadStokMaterialByAccount(string Tahun)
        {
            var id = Tahun.ToString();
            return Json(new { redirectUrl = "/StokMaterialByAccount1/" + id  });
        }

        public ActionResult StokMaterialByAccount(string id)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            List<ComboList> listYear = new List<ComboList>();
            //List<ListBudgetOpen> ListBudgetOpen = new List<ListBudgetOpen>();

            listYear.Add(new ComboList
            {
                Value = dHelper.DecryptURL(id),
                Text = dHelper.DecryptURL(id)
            });

            ViewData["Tahun"] = listYear;
           // List<BO_MaterialKontrol> ListMaterialStok = new List<BO_MaterialKontrol>();
           // ListMaterialStok = service.GetStokMaterialByAccount(dHelper.DecryptURL(id), "");
            //ViewData["StokMaterialByAccount"] = ListMaterialStok;
            ViewBag.Title = "Kontrol Stok - " + dHelper.DecryptURL(id);
            ViewBag.Year = dHelper.DecryptURL(id);

            return View();
        }


        public ActionResult StokMaterialByAccount1(string id)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            //List<ComboList> listYear = new List<ComboList>();

            //listYear.Add(new ComboList
            //{
            //    Value = dHelper.DecryptURL(id),
            //    Text = dHelper.DecryptURL(id)
            //});

           // ViewData["Tahun"] = listYear;
            // List<BO_MaterialKontrol> ListMaterialStok = new List<BO_MaterialKontrol>();
            // ListMaterialStok = service.GetStokMaterialByAccount(dHelper.DecryptURL(id), "");
            //ViewData["StokMaterialByAccount"] = ListMaterialStok;
            ViewBag.Title = "Kontrol Stok - " + dHelper.DecryptURL(id);
            //ViewBag.Year = dHelper.DecryptURL(id);
            GetDataByAccountlist(dHelper.DecryptURL(id));
            return View();
        }


        public ActionResult ViewStokByID(string Year, string Month, string invtid, string Acct,string site)
        {
            //List<PRBudgetDetail> ListPRBudgetDetail = new List<PRBudgetDetail>();
            //ListPRBudgetDetail = service.GetPRBudgetDetail(Year, Month, Sub, Acct);
            //ViewData["ListPRBudgetDetail"] = ListPRBudgetDetail;
            //ViewBag.Month = Month;
            //if (ListPRBudgetDetail.Count != 0)
            //{
            //    ViewBag.Sub = ListPRBudgetDetail[0].SubAccount.ToString() + " - " + ListPRBudgetDetail[0].SubAccountDesc.ToString();
            //    ViewBag.Account = ListPRBudgetDetail[0].Account.ToString() + " - " + ListPRBudgetDetail[0].AccountDesc.ToString();
            //}
            return View();
        }

        public ActionResult FIFO()
        {
            ViewBag.Title = "REPORT FIFO";
            return View();
        }

        public ActionResult GetFifo([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BO_Fifo_Excell Filter)
        {
            string CurrentYear = DateTime.Now.Year.ToString();

            var query = service.GetFifo(Filter).AsQueryable();
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
                _Cek.InvtName,
                _Cek.Qty,
                _Cek.Satuan
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult ExportExcel(BO_Fifo_Excell filter)
        {
            var DeptID = "";
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            var fileName = "";
           if (filter.Fifo == "IN")
            {
                fileName = "Report_Fifo_IN.xlsx";
            }
           else if (filter.Fifo == "OUT")
            {
                fileName = "Report_Fifo_OUT.xlsx";
            }

             

            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);

            List<BO_Fifo> ListExportToExcel = new List<BO_Fifo>();
            ListExportToExcel = service.GetFifo_DownloadExcel(filter);

            //if (DeptID == "PUR" || DeptID == "PVM")
            //{
            //    ListExport = service.GetFifo(filter);
            //}
            //else
            //{
            //    filter.UserName = dHelper.CurrentLoginUserName();
            //    ListExport = service.GetModelPRHistoryExport(filter);
            //}

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "InvtID";
                    worksheet.Cell(currentRow, 2).Value = "Description";
                    worksheet.Cell(currentRow, 3).Value = "Account";
                    worksheet.Cell(currentRow, 4).Value = "Site";
                    worksheet.Cell(currentRow, 5).Value = "Qty";
                    worksheet.Cell(currentRow, 6).Value = "Satuan";

                    worksheet.Range("A1:T1").Style.Font.Bold = true;
                    foreach (var FIFO in ListExportToExcel)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = FIFO.InvtID;
                        worksheet.Cell(currentRow, 2).Value = FIFO.InvtName;
                        worksheet.Cell(currentRow, 3).Value = FIFO.Account;
                        worksheet.Cell(currentRow, 4).Value = FIFO.Site;
                        worksheet.Cell(currentRow, 5).Value = FIFO.Qty;
                        worksheet.Cell(currentRow, 6).Value = FIFO.Satuan;
                    }
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(fullPath);
                    return Json(new { success = true ,filename = fileName });
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
        
        public ActionResult LoadStokFifo()
        {
            DateTime tgl = DateTime.Now;
            GetDataStokFifo(tgl);
            return View();
        }

        public ActionResult GetDataStokFifo(DateTime tgl)

        {
            var site = "";
            var siteDesc = "";
            var site2 = dHelper.CurrentLoginDeptID();
            try
            
            {
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                if (dHelper.CurrentLoginDeptID() == null)
                {
                    return Redirect("/account/login");
                }

                if (dHelper.CurrentLoginDeptID().Substring(0, 1) == "1")
                {
                    site = "TSC1";
                    siteDesc = "TANGERANG";
                }
                else
                {
                    site = "TSC3";
                    siteDesc = "CIKARANG";
                }
                List<BO_Fifo_Stok> ListMaterialStok = new List<BO_Fifo_Stok>();
                ListMaterialStok = service.GetStokFifo_auto(tgl, site);
                ViewData["DataStok"] = ListMaterialStok;
                ViewBag.tgl = tgl.ToString("dd-MMMM-yyyy").ToUpper();
                ViewBag.site = dHelper.CurrentLoginDeptID().Substring(0, 1);
                ViewBag.siteDesc = siteDesc;
                return PartialView("Partial/PartialFIFO");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult LoadModal_Stok(string invtid, string site)
        {
            var site2 = dHelper.CurrentLoginDeptID();
            try

            {


                //if (dHelper.CurrentLoginDeptID().Substring(0, 1) == "1")
                //{
                //    site = "TSC1";
                //}
                //else
                //{
                //    site = "TSC3";
                //}
                List<BO_Fifo_Stok> ListStok = new List<BO_Fifo_Stok>();
                ListStok = service.GetStokDetail(invtid, site);
                ViewData["DataStok"] = ListStok;
                return PartialView("Partial/PartialModal");

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public ActionResult LoadHasilProduksi()
        {
            DateTime tgl = DateTime.Now;
            GetDataStokFifo(tgl);
            return View();
        }
        
        public ActionResult LPH()
        {
            ViewBag.Title = "LAPORAN HASIL PRODUKSI";
            DateTime tgl = DateTime.Now;
            GetDataLPH(tgl, "All", "All", 15);
            return View();
        }

        public ActionResult GetDataLPH(DateTime tgl, string area, string noMesin, int perSlide )
        {
            var site = "TSC1";
            var siteDesc = "TANGERANG";
            //var site2 = dHelper.CurrentLoginDeptID();

            try
            {
                //if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
                //{
                //    return Redirect("/account/login");
                //}

                //if (string.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
                //{
                //    return Redirect("/account/login");
                //}

                //if (dHelper.CurrentLoginDeptID().Substring(0, 1) == "1")
                //{
                //    site = "TSC1";
                //    siteDesc = "TANGERANG";
                //}
                //else
                //{
                //    site = "TSC3";
                //    siteDesc = "CIKARANG";
                //}

                //Get data mesin
                List<BO_NoMesin> ListDataMesin = new List<BO_NoMesin>();
                ListDataMesin = service.GetNoMesin();
                ViewData["DataMesin"] = ListDataMesin;

                List<BO_HasilProduksi> ListLPH = new List<BO_HasilProduksi>();
                ListLPH = service.GetLPH_auto(tgl, site, area, noMesin);
                ViewData["DataLPH"] = ListLPH;
                ViewBag.PerSlide = perSlide;
                ViewBag.tgl = tgl.ToString("dd-MMMM-yyyy").ToUpper();
                //ViewBag.site = dHelper.CurrentLoginDeptID().Substring(0, 1);
                ViewBag.siteDesc = siteDesc;
                return PartialView("Partial/PartialLPH");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult LPHMesin()
        {
            List<BO_NoMesin> ListDataMesin = new List<BO_NoMesin>();
            ListDataMesin = service.GetNoMesin();
            ViewData["DataMesin"] = ListDataMesin;

            return View();
        }

        public ActionResult LPHMesinDetail(string NoMesin)
        {
            ViewBag.Title = "LAPORAN HASIL PRODUKSI MESIN";
            DateTime tgl = DateTime.Now;
            GetLPHMesin(tgl, NoMesin, 15);
            return View();
        }

        public ActionResult GetLPHMesin(DateTime Tgl, string NoMesin, int perSlide)
        {
            try
            {
                var siteDesc = "TANGERANG";

                //if (string.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
                //{
                //    return Redirect("/account/login");
                //}

                //if (string.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
                //{
                //    return Redirect("/account/login");
                //}

                //if (dHelper.CurrentLoginDeptID().Substring(0, 1) == "1")
                //{
                //    siteDesc = "TANGERANG";
                //}
                //else
                //{
                //    siteDesc = "CIKARANG";
                //}

                List<BO_HasilProduksiMesin> ListLPHMesin = new List<BO_HasilProduksiMesin>();
                ListLPHMesin = service.GetLPHByMesin(Tgl, NoMesin);
                ViewData["DataLPHMesin"] = ListLPHMesin;
                ViewBag.PerSlide = perSlide;
                ViewBag.tgl = Tgl.ToString("dd-MMMM-yyyy").ToUpper();
                //ViewBag.site = dHelper.CurrentLoginDeptID().Substring(0, 1);
                ViewBag.siteDesc = siteDesc;
                ViewBag.NoMesin = NoMesin;

                return PartialView("Partial/_GetLPHMesin");
            } catch (Exception)
            {
                throw;
            }
            
        }
    }
}