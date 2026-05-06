using ActionServices.Surat;
using DataTables.Mvc;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data;
using ActionServices;
using BusinessObjects.Surat;
using System.IO;
using BusinessObjects;
using Microsoft.Ajax.Utilities;

namespace iTells.Areas.User.Controllers
{
    public class SuratKeluarController : Controller
    {
        private ISuratKeluarService service { get; set; }
        private IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();
        public SuratKeluarController() : this(new SuratKeluarService(), new ComboListService()) { }

        public SuratKeluarController(ISuratKeluarService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }

        // GET: User/SuratKeluar
        public ActionResult Index()
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            List<ComboList> listDept = comboListService.GetDept();

            List<ComboList> AksesDept = new List<ComboList>();
            
            AksesDept.Add(new ComboList { Value = "ALL", Text = "ALL" });
            foreach (ComboList item in listDept)
            {
                AksesDept.Add(new ComboList { Value = item.Value.Substring(item.Value.Length - 3, 3), Text = item.Value.Substring(item.Value.Length - 3, 3) });
            }

            ViewData["Kategori"] = comboListService.GetKategori();            
            ViewData["Dept"] = AksesDept.DistinctBy(x => x.Value).ToList();
            ViewData["AksesDept"] = AksesDept.DistinctBy(x => x.Value).ToList();

            ViewBag.DeptId = dHelper.CurrentLoginDeptID();

            return View();
        }

        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            var dept = dHelper.CurrentLoginDeptID();

            var query = service.Get(dept).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.NoSurat.ToLower().ToString().Contains(value.ToString().ToLower())
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
                res.Tanggal,
                res.NoSurat,
                res.Pengirim,
                res.Dept,
                res.Tujuan,
                res.Kategori,
                res.Perihal
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(SuratKeluarModel model)
        {
            bool isSave = false;

            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            if (model == null)
            {
                return Json(new { success = isSave, message = "Model kosong " }, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                string path = Server.MapPath("~/Upload/E-Letter/Surat Keluar/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                HttpPostedFileBase postedFile = Request.Files[0];

                string fileName = postedFile.FileName;

                string newFileName = string.Concat(Path.GetFileNameWithoutExtension(fileName)
                               , DateTime.Now.ToString("_yyyyMMddHHmmss")
                               , Path.GetExtension(fileName)
                               );

                model.FileName = newFileName;

                postedFile.SaveAs(path + newFileName);
            }

            model.Username = dHelper.CurrentLoginUserName();

            int hasil = service.Add(model);
            if (hasil != -1)
            {
                isSave = true;
                return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = isSave, message = "Simpan Data Gagal" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        public ActionResult Update(SuratKeluarModel model)
        {
            bool isSave = false;

            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            if (model == null)
            {
                return Json(new { success = isSave, message = "Model kosong " }, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                string path = Server.MapPath("~/Upload/E-Letter/Surat Masuk/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                HttpPostedFileBase postedFile = Request.Files[0];

                string fileName = postedFile.FileName;

                string newFileName = string.Concat(Path.GetFileNameWithoutExtension(fileName)
                               , DateTime.Now.ToString("_yyyyMMddHHmmss")
                               , Path.GetExtension(fileName)
                               );

                model.FileName = newFileName;

                postedFile.SaveAs(path + newFileName);
            }

            model.Username = dHelper.CurrentLoginUserName();

            int hasil = service.Edit(model);
            if (hasil != -1)
            {
                isSave = true;
                return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = isSave, message = "Simpan Data Gagal" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            bool isSave = false;

            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            int hasil = service.Delete(Id);

            if (hasil != -1)
            {
                isSave = true;
                return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = isSave, message = "Simpan Data Gagal" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Get(int Id)
        {
            var result = service.GetRow(Id);

            if (result != null)
            {
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetKategori(string IdKategori)
        {
            var result = service.GetKategori(IdKategori);

            if (result != null)
            {
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }

        [HttpGet]
        public ActionResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/Upload/E-Letter/Surat Keluar/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }

        [HttpGet]
        public ActionResult GetNoSurat()
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            string dept = dHelper.CurrentLoginDeptID();

            string result = service.GetNoSuratAuto(dept);

            if (result != null)
            {
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }
    }
}
