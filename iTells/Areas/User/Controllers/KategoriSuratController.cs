using ActionServices.Surat;
using DataTables.Mvc;
using iTells.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using BusinessObjects.Surat;
using System.IO;
using DataObjects.Helpers;

namespace iTells.Areas.User.Controllers
{
    public class KategoriSuratController : Controller
    {
        IKategoriSuratService service { get; set; }
        GeneralHelper dHelper = new GeneralHelper();
        public KategoriSuratController() : this(new KategoriSuratService()) { }

        public KategoriSuratController(IKategoriSuratService service)
        {
            this.service = service;
        }

        // GET: User/KategoriSurat
        public ActionResult Index()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            return View();
        }


        public ActionResult GetKategori()
        {
            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }
            ViewData["ListSuratKategori"] = service.GetKategori();
            return View();
        }

        [HttpPost]
        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {

            var query = service.Get().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Kategori.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "cat_name asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(res => new
            {
                res.Id,
                res.IdKategori,
                res.Kategori,
                res.Aktif
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(KategoriSuratModel model)
        {
            bool isSave = false;

            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            if (model == null)
            {
                return Json(new { success = isSave, message = "Model kosong " }, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                string path = Server.MapPath("~/Upload/E-Letter/Kategori Surat/");
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
        public ActionResult Update(KategoriSuratModel model)
        {
            bool isSave = false;

            if (dHelper.CurrentLoginUserName() == null)
            {
                return Redirect("/account/login");
            }

            if (model == null)
            {
                return Json(new { success = isSave, message = "Model kosong " }, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                string path = Server.MapPath("~/Upload/E-Letter/Kategori Surat/");
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

            if (dHelper.CurrentLoginUserName() == null)
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
                ViewBag.FileName = result.FileName;
                ViewData["FileName"] = result.FileName; ;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }

        [HttpGet]
        public ActionResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/Upload/E-Letter/Kategori Surat/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}