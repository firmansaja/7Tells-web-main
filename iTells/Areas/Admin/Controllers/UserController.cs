using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTells.Models;
using iTells.Helper;
using System.IO;
using ActionServices;
using DataTables.Mvc;
using System.Linq.Dynamic;
using BusinessObjects;
namespace iTells.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        iTellsEntities db=new iTellsEntities();
        GeneralHelper dHelper=new GeneralHelper();
        Dictionary<string, object> res = new Dictionary<string, object>();

        IUserService service { get; set; }
        IComboListService comboListService { get; set; }


        public UserController() : this(new UserService(), new ComboListService()) { }
        public UserController(IUserService service, IComboListService comboListService)
        {
            this.service = service;
            this.comboListService = comboListService;
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission(Url);
            if (control.IsLogin == false)
                return Redirect("/account/login");
            if (control.IsView == false)
                return Redirect("/admin/notaccess");

            ViewBag.IsAdd = control.IsAdd;
            if (Session["Add"] != null)
                ViewBag.Message = "<div class=\"alert alert-success fade in\">successfully insert user.</div>";
            else if (Session["Update"] != null)
                ViewBag.Message = "<div class=\"alert alert-success fade in\">successfully update user.</div>";
            Session.RemoveAll();         
            return View();
        }

        #region Populate UserList
        public ActionResult PopulateUser(string Status)
        {
            var control = dHelper.GetUserPermission("");
            ViewBag.IsEdit = control.IsEdit;
            ViewBag.IsDelete = control.IsDelete;
            var userList = db.tblUsers.Where(s => s.IsDeleted == false).ToList();
            if (Status == "Active")
                userList = userList.Where(s => s.IsActive == true).ToList();
            else if (Status == "Banned")
                userList = userList.Where(s => s.IsActive == false).ToList();
            else if (Status == "NewUser")
                userList = userList.Where(s => s.InsertDate.Value.Month == DateTime.Now.Month).ToList();
            return PartialView("_List", userList);
        }
        #endregion

        #region Create & Update User
        public ActionResult Create()
        {
            PopulateDropdown();
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission(Url);
            if (control.IsLogin == false)
                return Redirect("/account/login");
            if (control.IsView == false)
                return Redirect("/admin/notaccess");

            return View();
        }

        public void PopulateDropdown()
        {
            var country = db.tblCountries.Where(s => s.IsDeleted == false).OrderBy(s => s.Name);
            ViewBag.CountryList = new SelectList(country, "ID", "Name");

            var role= db.tblRoles.Where(s => s.IsDeleted == false).OrderBy(s => s.Name);
            ViewBag.RoleList = new SelectList(role, "ID", "Name");
            
            List<SelectListItem> listGender = new List<SelectListItem>();
            listGender.Add(new SelectListItem { Text = "Male", Value = "Male", Selected = true });
            listGender.Add(new SelectListItem { Text = "FeMale", Value = "FeMale" });
            ViewBag.GenderList = new SelectList(listGender, "Value", "Text");
        }                
        
        public JsonResult AddUser(UserViewModel objUser, HttpPostedFileBase Photo)
        {
            PopulateDropdown();
            try
            {
                tblUser user;
                if (objUser.ID == null)
                {
                    user = new tblUser();
                    user.PhotoPath = "/Content/assets/dist/img/avatar5.png";
                }
                else
                {
                    user = db.tblUsers.FirstOrDefault(s => s.ID == objUser.ID);
                }
                if (user != null)
                {
                    if (GeneralHelper.IsDemo == true)
                    {
                        if (user.IsDefault == true)
                        {
                            Session["edit"] = "Edit";
                            res["status"] = "2";
                            res["message"] = "<div class=\"alert alert-danger fade in\">Edit is disabled in default user.</div>";
                            res["url"]= (Request.UrlReferrer.AbsoluteUri);
                            return Json(res, JsonRequestBehavior.AllowGet);
                        }
                    }
                    user.FirstName = objUser.FirstName;
                    user.LastName = objUser.LastName;                    
                    user.IsActive = true;
                    user.Phone = objUser.Phone;
                    user.Address = objUser.Address;
                    user.Gender = objUser.Gender;
                    user.CountryID = Convert.ToInt32(objUser.CountryID);
                    if (objUser.BirthDate != null)
                        user.DateOfBirth =Convert.ToDateTime(objUser.BirthDate);
                    user.Email = objUser.Email;                    
                    user.Password = dHelper.Encrypt(objUser.Password);
                    user.IsDefault = false;
                    user.IsDeleted = false;
                    user.FacebookLink = objUser.FacebookLink;
                    user.TwitterLink = objUser.TwitterLink;
                    user.LinkedInLink = objUser.LinkedInLink;
                    user.GoogleLink = objUser.GoogleLink;
                    user.SkypeID = objUser.SkypeID;
                    if (Photo != null)
                    {
                        string _FileName = Guid.NewGuid().ToString();
                        string _ext = Path.GetExtension(Photo.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Images/Profile/"), _FileName + _ext);

                        if (System.IO.File.Exists(Server.MapPath(user.PhotoPath)))
                            System.IO.File.Delete(Server.MapPath(user.PhotoPath));

                        Photo.SaveAs(_path);
                        user.PhotoPath = "/Images/Profile/" + _FileName + _ext;
                    }
                    string desc = "";
                    if (objUser.ID == null)
                    {
                        user.ID = Guid.NewGuid();
                        user.RoleID = objUser.RoleID;
                        user.UserName = objUser.UserName;
                        user.FailedPasswordCount = 0;
                        user.LastLoginDate = DateTime.Now;
                        user.InsertDate = DateTime.Now;
                        user.LastModifiedBy = new Guid(dHelper.CurrentLoginUser());
                        user.IsLockedOut = false;
                        db.tblUsers.Add(user);
                        desc = ("Insert User : " + user.UserName);
                        Session["Add"] = "add";
                    }
                    else
                    {
                        desc = ("Update User : " + user.UserName);
                        Session["Update"] = "update";
                    }
                    db.SaveChanges();
                    dHelper.LogAction(desc);
                    res["status"] = "1";
                    //Response.Redirect("/admin/user");
                    res["url"] = "/admin/user";
                }
            }
            catch
            {
                res["status"] = "0";
            }
            
            return Json(res,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string ID)
        {
            PopulateDropdown();
            if (Session["edit"] != null)
            {
                ViewBag.Message = "<div class=\"alert alert-danger fade in\">Edit is disabled in default user.</div>";
                Session.RemoveAll();
            }
            Guid UserID = new Guid(ID);
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission(Url);
            if (control.IsEdit == false)
            {
                if (dHelper.Decrypt(ID) != dHelper.CurrentLoginUser())
                    return Redirect("/admin/notaccess");                
            }
            ViewBag.IsAdmin = control.IsAdmin;
            var userInfo = db.tblUsers.FirstOrDefault(s => s.ID == UserID);
            if (userInfo != null)
            {
                UserViewModel objUser = new UserViewModel();
                objUser.ID = userInfo.ID;
                objUser.FirstName = userInfo.FirstName;
                objUser.LastName = userInfo.LastName;
                objUser.BirthDate = userInfo.DateOfBirth == null ? null : userInfo.DateOfBirth.Value.ToString("dd-MMM-yyyy");
                objUser.Gender = userInfo.Gender;
                objUser.CountryID = userInfo.CountryID;
                objUser.Phone = userInfo.Phone;
                objUser.Email = userInfo.Email;
                objUser.Address = userInfo.Address;
                objUser.RoleID = userInfo.RoleID;
                objUser.FacebookLink = userInfo.FacebookLink;
                objUser.TwitterLink = userInfo.TwitterLink;
                objUser.GoogleLink = userInfo.GoogleLink;
                objUser.LinkedInLink = userInfo.LinkedInLink;
                objUser.SkypeID = userInfo.SkypeID;
                objUser.UserName = userInfo.UserName;
                objUser.Password =dHelper.Decrypt(userInfo.Password);
                objUser.cPassword =dHelper.Decrypt(userInfo.Password);
                ViewBag.PhotoPath = userInfo.PhotoPath;
                ViewBag.Edit = "1";
                return View(objUser);
            }
            
            return View();
        }
        #endregion

        #region Delete User
        public JsonResult Delete(Guid ID)
        {
            var userInfo = db.tblUsers.FirstOrDefault(s => s.ID == ID);
            if (userInfo != null)
            {
                if (GeneralHelper.IsDemo == true)
                {
                    if (userInfo.IsDefault == true)
                    {
                        res["success"] = 3;
                        res["message"] = "<div class=\"alert alert-danger fade in\">delete is disabled in default user.</div>";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
                userInfo.IsDeleted = true;
                userInfo.LastModified = DateTime.Now;
                userInfo.LastModifiedBy = new Guid(dHelper.CurrentLoginUser());
                db.SaveChanges();

                string Description = string.Format("Delete User [Name: {0}]", userInfo.FirstName + " " + userInfo.LastName);
                dHelper.LogAction(Description);
                res["success"] = 0;
                res["message"] = "<div class=\"alert alert-success fade in\">successfully delete user.</div>";
                string Query = Request.UrlReferrer.Query;
                if (Query == "")
                    res["url"] = "/admin/user/populateuser";
                else
                    res["url"] = "/admin/user/populateuser" + Query;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Update User Status
        public JsonResult UpdateStatus(Guid Id)
        {
            var userInfo = db.tblUsers.FirstOrDefault(s => s.ID == Id);
            if (userInfo != null)
            {
                if (GeneralHelper.IsDemo == true)
                {
                    if (userInfo.IsDefault == true)
                    {
                        res["success"] = 3;
                        res["message"] = "<div class=\"alert alert-danger fade in\">status change is disabled in default user.</div>";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
                if (userInfo.IsActive == true)
                {
                    res["success"] = 0;
                    userInfo.IsActive = false;

                    string Description = string.Format("Update User Status [Name: {0}] (InActive)", userInfo.FirstName + " " + userInfo.LastName);
                    dHelper.LogAction(Description);
                }
                else
                {
                    res["success"] = 1;
                    userInfo.IsActive = true;

                    string Description = string.Format("Edit User [Name: {0}] (Active)", userInfo.FirstName + " " + userInfo.LastName);
                    dHelper.LogAction(Description);
                }
                userInfo.LastModified = DateTime.Now;
                userInfo.LastModifiedBy =new Guid(dHelper.CurrentLoginUser());
                db.SaveChanges();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region User Profile
        public new ActionResult Profile(Guid ID)
        {
            string Url = Request.RawUrl.ToString();
            var control = dHelper.GetUserPermission("");
            if (control.IsLogin == false)
                return Redirect("/account/login");

            var userInfo = db.tblUsers.FirstOrDefault(s => s.ID == ID);
            if (userInfo != null)
            {
                var role = db.tblRoles.FirstOrDefault(s => s.ID == userInfo.RoleID);
                if (role != null)
                    ViewBag.RoleName = role.Name;
                ViewBag.PhotoPath = userInfo.PhotoPath;
                return View(userInfo);
            }
            return View();
        }
        #endregion

        #region User ActivityLog
        public ActionResult GetActivityLog()
        {
            var activity = dHelper.GetActivityLog(new Guid(dHelper.CurrentLoginUser()));
            return PartialView("_ActivityLog", activity);
        }
        #endregion

        #region "S_USER"
        public ActionResult IndexUser()
        {
            ViewData["Role"] = comboListService.GetRole();
            return View();
        }

        
        [HttpPost]
        public ActionResult GetUsers([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {

            var query = service.GetUsers().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Username.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                        p.Site.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                        p.UserGroupCode.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                        p.Email.ToLower().ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "Username asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(User => new
            {
                User.No,
                User.Username,
                User.UserGroupCode,
                User.Site,
                //User.UserId,
                //User.RoleID,
                User.Email
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetUserById(string Username)
        {
            //Guid _IdUser = new Guid(Username);
            var result = service.GetUserById(Username);

            if (result != null)
            {
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }
        public ActionResult EditUser(BusinessObjects.User model)
        {
            bool isSave = false;
            Guid UserId = Guid.NewGuid();
            model.UserId = UserId;
            int hasil = service.Update(model);
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
        #endregion
    }
}