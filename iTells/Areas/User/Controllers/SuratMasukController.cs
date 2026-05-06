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
    public class SuratMasukController : Controller
    {
        private ISuratMasukService service { get; set; }
        private IComboListService comboListService { get; set; }
        GeneralHelper dHelper = new GeneralHelper();

        public SuratMasukController() : this(new SuratMasukService(), new ComboListService()) { }

        public SuratMasukController(ISuratMasukService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }

        //GET: User/ELetter
        public ActionResult Index()
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            List<ComboList> listDept = comboListService.GetDept();

            List<ComboList> Dept = new List<ComboList>();

            foreach (ComboList item in listDept)
            {
                Dept.Add(new ComboList { Value = item.Value.Substring(item.Value.Length - 3, 3), Text = item.Value.Substring(item.Value.Length - 3, 3) });
            }

            ViewData["Kategori"] = comboListService.GetKategori();
            ViewData["Dept"] = Dept.DistinctBy(x => x.Value).ToList();

            ViewBag.DeptId = dHelper.CurrentLoginDeptID();

            return View();
        }

        public ActionResult SuratMasukSecurity()
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
            {
                return Redirect("/account/login");
            }

            List<ComboList> listDept = comboListService.GetDept();

            List<ComboList> Dept = new List<ComboList>();

            foreach (ComboList item in listDept)
            {
                Dept.Add(new ComboList { Value = item.Value.Substring(item.Value.Length - 3, 3), Text = item.Value.Substring(item.Value.Length - 3, 3) });
            }

            ViewData["Kategori"] = comboListService.GetKategori();
            ViewData["Dept"] = Dept.DistinctBy(x => x.Value).ToList();
            ViewBag.UserName = dHelper.CurrentLoginUserName();

            return View();
        }
        
        [HttpPost]
        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int flag, string status)
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            var dept = dHelper.CurrentLoginDeptID();

            var query = service.Get(dept, flag, status).AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            //Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.NoUrut.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                            p.Dari.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                            p.Tujuan.ToLower().ToString().Contains(value.ToString().ToLower())
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
            query = query.OrderBy("Id desc");

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(res => new
            {
                res.Id,
                res.NoUrut,
                res.NoSurat,
                res.Tanggal,
                res.Dari,
                res.Pengirim,
                res.Tujuan,
                res.Dept,
                res.Security,
                res.TglAmbil,
                res.Penerima,
                res.DeptTerima,
                res.Perihal,
                res.Kategori,
                res.Jenis,
                res.Media,
                res.Akses,
                res.Rahasia
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(SuratMasukModel model)
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

                string newFileName =string.Concat(Path.GetFileNameWithoutExtension(fileName)
                               , DateTime.Now.ToString("_yyyyMMddHHmmss")
                               , Path.GetExtension(fileName)
                               );

                model.FileName = newFileName;

                postedFile.SaveAs(path + newFileName);
            }

            model.Username = dHelper.CurrentLoginUserName();
            string dept = dHelper.CurrentLoginDeptID();

            int hasil = service.Add(model, dept);
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
        public ActionResult Update(SuratMasukModel model)
        {
            bool isSave = false;

            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
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

            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()))
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
        public ActionResult GetNoUrut(int Flag)
        {
            if (String.IsNullOrEmpty(dHelper.CurrentLoginUserName()) || String.IsNullOrEmpty(dHelper.CurrentLoginDeptID()))
            {
                return Redirect("/account/login");
            }

            string dept = dHelper.CurrentLoginDeptID();

            string result = service.GetNoUrutAuto(dept, Flag);

            if (result != null)
            {
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }

        [HttpGet]
        public ActionResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/Upload/E-Letter/Surat Masuk/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
