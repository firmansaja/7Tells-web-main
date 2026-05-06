using ActionServices;
using DataTables.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using iTells.Helper;
using System.Text;
using BusinessObjects.PPAP;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Web;

namespace iTells.Areas.User.Controllers
{  
    public class PPAPController : Controller
    {
        List<PPAPAksesDeptModel> aksesDept { get; set; }
        IPPAPService service { get; set; }
        IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public PPAPController() : this(new PPAPService(), new ComboListService(), new List<PPAPAksesDeptModel>()) { }
        public PPAPController(IPPAPService service, IComboListService comboListService, List<PPAPAksesDeptModel> aksesDept)
        {
            this.service = service;
            this.comboListService = comboListService;
            this.aksesDept = aksesDept;
        }

        // GET: User/PPAP
        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            return View();
        }

        public void AksesDept()
        {
            aksesDept = service.GetPPAPAksesDept(dHelper.CurrentLoginUserName());
            ViewBag.Dept = aksesDept.FirstOrDefault().Dept;
            ViewBag.Level = aksesDept.FirstOrDefault().Level;
            ViewData["AksesDept"] = aksesDept;
        }

        [HttpPost]
        public ActionResult GetPPAP([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            var query = service.GetPPAP().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Site.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                            p.Status.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                            p.CustID.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                            p.ProjectName.ToLower().ToString().Contains(value.ToString().ToLower())
                                    );
            }

            //query = query.Where(p => p.Status.ToLower().ToString().Contains(status.ToString().ToLower())
            //                        );

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

            //query = query.OrderBy(orderByString == string.Empty ? "Id desc" : orderByString);
            //query = query.OrderBy("Id desc");

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(res => new
            {
                res.NoPPAP,
                res.IdStatus,
                res.Status,
                res.Site,
                res.CustID,
                res.ProjectName,
                res.ConfirmationDate,
                res.MassproductionDate,
                res.OverallJudgment,
                res.IsEdit
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewPPAP(string noppap, string idstatus)
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            string PPAPDecrypt = dHelper.DecryptURL(noppap);
            string IdStatusDecrypt = dHelper.DecryptURL(idstatus);

            AksesDept();
            GetPPAPHeader(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPDetail(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPAttendance(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPApprovalDept(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPTooling(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPQuality(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPPreparation(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPDocument(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPSupplier(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPPacking(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPComparation(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPPainting(PPAPDecrypt, IdStatusDecrypt);
            GetPPAPAssembly(PPAPDecrypt, IdStatusDecrypt);

            return View();
        }

        private void GetPPAPHeader(string noPPAP, string idStatus)
        {
            PPAPHeaderModel ppapHeader = new PPAPHeaderModel();
            ppapHeader = service.GetPPAPById(noPPAP, idStatus);
            ViewData["PPAPHeader"] = ppapHeader;
        }

        private void GetPPAPDetail(string noPPAP, string idStatus)
        {
            List<PPAPDetailModel> ppapDetail = new List<PPAPDetailModel>();
            ppapDetail = service.GetPPAPDetailById(noPPAP, idStatus);
            ViewData["PPAPDetail"] = ppapDetail;
        }

        private void GetPPAPAttendance(string noPPAP, string idStatus)
        {
            List<PPAPAttendanceModel> ppapAttendance = new List<PPAPAttendanceModel>();
            ppapAttendance = service.GetPPAPAttendanceById(noPPAP, idStatus);
            ViewData["PPAPAttendance"] = ppapAttendance;
        }

        private void GetPPAPApprovalDept(string noPPAP, string idStatus)
        {
            List<PPAPApprovalDeptModel> ppapApprovalDept = new List<PPAPApprovalDeptModel>();
            ppapApprovalDept = service.GetPPAPApprovalDeptById(noPPAP, idStatus);
            ViewData["PPAPApprovalDept"] = ppapApprovalDept;
        }

        private void GetPPAPTooling(string noPPAP, string idStatus)
        {
            List<PPAPToolingModel> ppapTooling = new List<PPAPToolingModel>();
            ppapTooling = service.GetPPAPToolingById(noPPAP, idStatus);
            ViewData["PPAPTooling"] = ppapTooling;
        }

        private void GetPPAPQuality(string noPPAP, string idStatus)
        {
            List<PPAPQualityModel> ppapQuality = new List<PPAPQualityModel>();
            ppapQuality = service.GetPPAPQualityById(noPPAP, idStatus);
            ViewData["PPAPQuality"] = ppapQuality;
        }

        private void GetPPAPPreparation(string noPPAP, string idStatus)
        {
            List<PPAPPreparationModel> ppapPreparation = new List<PPAPPreparationModel>();
            ppapPreparation = service.GetPPAPPreparationById(noPPAP, idStatus);
            ViewData["PPAPPreparation"] = ppapPreparation;
        }

        private void GetPPAPDocument(string noPPAP, string idStatus)
        {
            List<PPAPDocumentModel> ppapDocument = new List<PPAPDocumentModel>();
            ppapDocument = service.GetPPAPDocumentById(noPPAP, idStatus);
            ViewData["PPAPDocument"] = ppapDocument;
        }

        private void GetPPAPSupplier(string noPPAP, string idStatus)
        {
            List<PPAPSupplierModel> ppapSupplier = new List<PPAPSupplierModel>();
            ppapSupplier = service.GetPPAPSupplierById(noPPAP, idStatus);
            ViewData["PPAPSupplier"] = ppapSupplier;
        }

        private void GetPPAPPacking(string noPPAP, string idStatus)
        {
            List<PPAPPackingModel> ppapPacking = new List<PPAPPackingModel>();
            ppapPacking = service.GetPPAPPackingById(noPPAP, idStatus);
            ViewData["PPAPPacking"] = ppapPacking;
        }

        private void GetPPAPComparation(string noPPAP, string idStatus)
        {
            List<PPAPComparationModel> ppapComparation = new List<PPAPComparationModel>();
            ppapComparation = service.GetPPAPComparationById(noPPAP, idStatus);
            ViewData["PPAPComparation"] = ppapComparation;
        }

        private void GetPPAPPainting(string noPPAP, string idStatus)
        {
            List<PPAPPaintingModel> ppapPainting = new List<PPAPPaintingModel>();
            ppapPainting = service.GetPPAPPaintingById(noPPAP, idStatus);
            ViewData["PPAPPainting"] = ppapPainting;
        }

        private void GetPPAPAssembly(string noPPAP, string idStatus)
        {
            List<PPAPAssemblyModel> ppapAssembly = new List<PPAPAssemblyModel>();
            ppapAssembly = service.GetPPAPAssemblyById(noPPAP, idStatus);
            var result = ppapAssembly
                    .Where(x => x.Seq == 1)
                    .ToList();
            ViewData["PPAPAssembly"] = result;
        }

        public ActionResult GetDocumentDetail(string noPPAP, string idStatus, string code)
        {
            try
            {
                List<PPAPDocumentDetailModel> listDocumentDetail = new List<PPAPDocumentDetailModel>();
                listDocumentDetail = service.GetPPAPDocumentDetailByItem(noPPAP, idStatus, code);
                
                if (listDocumentDetail.Count == 0)
                {
                    return Json(new { success = false, message = "Data tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }
                
                return Json(new { success = true, message = "No. Seq ditemukan", data = listDocumentDetail }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetPaintingDetail(string noPPAP, string idStatus, string code)
        {
            try
            {
                List<PPAPPaintingDetailModel> listPaintingDetail = new List<PPAPPaintingDetailModel>();
                listPaintingDetail = service.GetPPAPPaintingDetailByItem(noPPAP, idStatus, code);

                if (listPaintingDetail.Count == 0)
                {
                    return Json(new { success = false, message = "Data tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, message = "No. Seq ditemukan", data = listPaintingDetail }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetAssemblyDetail(string noPPAP, string idStatus, string code)
        {
            try
            {
                List<PPAPAssemblyModel> listAssembly = new List<PPAPAssemblyModel>();
                listAssembly = service.GetPPAPAssemblyById(noPPAP, idStatus);

                var result = listAssembly
                    .Where(x => x.Code == code)
                    .ToList();

                if (result.Count == 0)
                {
                    return Json(new { success = false, message = "Data tidak ditemukan!" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, message = "No. Seq ditemukan", data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult GetToolingStatus([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string noppap, int idstatus)
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            var query = service.GetPPAPToolingById(noppap, idstatus.ToString()).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.PartName.ToLower().ToString().Contains(value.ToString().ToLower())
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

            //query = query.OrderBy(orderByString == string.Empty ? "Id desc" : orderByString);
            //query = query.OrderBy("Id desc");

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(res => new
            {
                res.NoPPAP,
                res.IdStatus,
                res.Code,
                res.PartNumber,
                res.PartName,
                res.Data2D,
                res.DataRNECI,
                res.DataGrain,
                res.MoldFabricationLocation,
                res.MoldProblem
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateApprovalDept(PPAPApprovalDeptModel data)
        {
            try
            {
                bool isSave = false;                
                if (dHelper.CurrentLoginUserName() == null)
                {
                    return Redirect("/account/login");
                }

                data.Username = dHelper.CurrentLoginUserName();
                int hasil = service.UpdateApprovalDept(data);
                if (hasil != -1)
                {
                    isSave = true;
                    return Json(new { success = isSave, message = "Comment Berhasil Disimpan!" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = isSave, message = "Comment Gagal Disimpan" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DownloadDocument(string noPPAP, int idStatus, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return HttpNotFound("Invalid file name");

            fileName = Path.GetFileName(fileName);

            string basePath = ConfigurationManager.AppSettings["PPAPFilePath"];
            string filePath = Path.Combine(basePath, noPPAP, idStatus.ToString(), fileName);

            if (!System.IO.File.Exists(filePath))
                return HttpNotFound("File not found");

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(
                fileBytes,
                MimeMapping.GetMimeMapping(fileName),
                fileName
            );
        }

        public ActionResult ViewReportPPAP(string noPPAP, int idStatus)
        {
            this.HttpContext.Session["NoPPAP"] = noPPAP;
            this.HttpContext.Session["IdStatus"] = idStatus;

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}