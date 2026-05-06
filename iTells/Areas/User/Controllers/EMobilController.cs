using ActionServices;
using BusinessObjects;
using DataTables.Mvc;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DataObjects.Helpers;

namespace iTells.Areas.User.Controllers
{
    public class EMobilController : Controller
    {
        MobilAksesUser mobilakses { get; set; }
        IEMobilService service { get; set; }
        IComboListService comboListService { get; set; }
        //ApproveHistory apvHistory { get; set; }

        GeneralHelper dHelper = new GeneralHelper();

        public EMobilController() : this(new MobilAksesUser(), new EMobilService(), new ComboListService()) { }

        public EMobilController(MobilAksesUser mobilakses, IEMobilService service, IComboListService comboListService)
        {
            this.mobilakses = mobilakses;
            this.service = service;
            this.comboListService = comboListService;
            //this.apvHistory = apvHistory;
        }

        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            MobilUserAkses("FrmReqKendaraan");

            return View();
        }

        public void MobilUserAkses(string Frm)
        {
            mobilakses = service.GetAkses(dHelper.CurrentLoginUserName(), Frm);
            ViewData["AksesUser"] = mobilakses;
        }

        [HttpPost]
        public ActionResult GetReqMobil([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string status)
        {
            MobilUserAkses("FrmReqKendaraan");

            var query = service.GetReqMobil(mobilakses.UserName, status).AsQueryable();

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
            
            var data = query.Select(_Req => new
            {
                _Req.NoTrans,
                _Req.Tanggal,
                _Req.Dept,
                _Req.Level1,
                _Req.Level2,
                _Req.Level3,
                _Req.CurrentLevel,
                _Req.Approved,
                _Req.Comment
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewReqMobil(string NoTrans)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string noTransDecrypt = dHelper.DecryptURL(NoTrans);

            MobilUserAkses("FrmReqKendaraan");
            GetReqHeader(noTransDecrypt);
            GetReqDetail(noTransDecrypt);

            return View();
        }

        private void GetReqHeader(string NoTrans)
        {          
            EMobilReq req = new EMobilReq();
            req = service.GetReqHeaderByID(NoTrans);
            ViewData["ReqHeader"] = req;
        }

        private void GetReqDetail(string NoTrans)
        {
            List<EMobilReqDetail> reqDetail = new List<EMobilReqDetail>();
            reqDetail = service.GetReqDetailByID(NoTrans);
            ViewData["ListReqDetail"] = reqDetail;
        }

        [HttpPost]
        public ActionResult ApproveReq(EMobilReq MobilReq)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                MobilUserAkses("FrmReqKendaraan");

                ApproveHistory apvHistory = new ApproveHistory()
                {
                    UserName = mobilakses.UserName,
                    MenuCode = "FrmReqKendaraan",
                    DeptID = mobilakses.Dept,
                    NoTransaksi = MobilReq.NoTrans,
                    LevelApproved = mobilakses.Level,
                    StatusApproved = MobilReq.Approved,
                    ApprovedBy = mobilakses.UserName,
                    IsActive = 1
                };

                int hasil = service.ApproveReq(MobilReq, apvHistory);

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
        public ActionResult RejectReq(EMobilReq MobilReq)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                MobilUserAkses("FrmReqKendaraan");

                ApproveHistory apvHistory = new ApproveHistory()
                {
                    UserName = mobilakses.UserName,
                    MenuCode = "FrmReqKendaraan",
                    DeptID = mobilakses.Dept,
                    NoTransaksi = MobilReq.NoTrans,
                    LevelApproved = mobilakses.Level,
                    StatusApproved = MobilReq.Approved,
                    ApprovedBy = mobilakses.UserName,
                    IsActive = 1
                };

                int hasil = service.ApproveReq(MobilReq, apvHistory);

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

        public ActionResult CekMobil()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetCekMobil([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string Status)
        {
            MobilUserAkses("FrmCheckKendaraan");

            var query = service.GetCekMobil(mobilakses == null ? 0 : mobilakses.Level, Status).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.NoSuratJalan.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "NoSuratJalan asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(_Cek => new
            {
                _Cek.NoSuratJalan ,
                _Cek.Tanggal ,
                _Cek.StatusMobil,
                _Cek.NoMobil,
                _Cek.NamaSupir,
                _Cek.Catatan ,
                _Cek.BerangkatTgl ,
                _Cek.KMAwal ,
                _Cek.KembaliTgl ,
                _Cek.KMAkhir ,
                _Cek.Status ,
                _Cek.Approved ,
                _Cek.Comment ,
                _Cek.CurrLvlApv
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCekMobil(string NoSuratJalan)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string noTransDecrypt = dHelper.DecryptURL(NoSuratJalan);

            MobilUserAkses("FrmCheckKendaraan");
            GetCekHeader(noTransDecrypt);
            GetCekDetail(noTransDecrypt);

            return View();
        }

        private void GetCekHeader(string NoSuratJalan)
        {
            EMobilCek cek = new EMobilCek();
            cek = service.GetCekHeaderByID(NoSuratJalan);
            ViewData["CekHeader"] = cek;
        }

        private void GetCekDetail(string NoSuratJalan)
        {
            List<EMobilCekDetail> cekDetail = new List<EMobilCekDetail>();
            cekDetail = service.GetCekDetailByID(NoSuratJalan);
            ViewData["ListCekDetail"] = cekDetail;
        }

        [HttpPost]
        public ActionResult ApproveCek(EMobilCek MobilCek)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                MobilUserAkses("FrmCheckKendaraan");

                ApproveHistory apvHistory = new ApproveHistory()
                {
                    UserName = mobilakses.UserName,
                    MenuCode = "FrmCheckKendaraan",
                    DeptID = mobilakses.Dept,
                    NoTransaksi = MobilCek.NoSuratJalan,
                    LevelApproved = mobilakses.Level,
                    StatusApproved = MobilCek.Approved,
                    ApprovedBy = mobilakses.UserName,
                    IsActive = 1
                };

                int hasil = service.ApproveCek(MobilCek, apvHistory);

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
        public ActionResult RejectCek(EMobilCek MobilCek)
        {
            try
            {
                bool isSave = false;
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                MobilUserAkses("FrmCheckKendaraan");

                ApproveHistory apvHistory = new ApproveHistory()
                {
                    UserName = mobilakses.UserName,
                    MenuCode = "FrmCheckKendaraan",
                    DeptID = mobilakses.Dept,
                    NoTransaksi = MobilCek.NoSuratJalan,
                    LevelApproved = mobilakses.Level,
                    StatusApproved = MobilCek.Approved,
                    ApprovedBy = mobilakses.UserName,
                    IsActive = 1
                };

                int hasil = service.ApproveCek(MobilCek, apvHistory);

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

        public ActionResult ViewReportMbl(String NoSuratJalan)
        {
            this.HttpContext.Session["NoSuratJalan"] = NoSuratJalan;
            
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}