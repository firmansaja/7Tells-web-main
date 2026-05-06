using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTells.Models;
using System.Web.Security;
using iTells.Helper;
using ActionServices;
using DataObjects.Helpers;

namespace iTells.Controllers
{

    public class AccountController : Controller
    {
        iTellsEntities db = new iTellsEntities();
        GeneralHelper dHelper = new GeneralHelper();
        MailHelper dMailHelper = new MailHelper();
        IUserService service { get; set; }
        public AccountController(): this(new UserService()) { }
        public AccountController(IUserService service) { this.service = service; }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        //[AllowAnonymous]
        public ActionResult Login()
        {
            var setting = db.tblSettings.FirstOrDefault();
            if (setting != null)
            {
                HttpCookie cookie1 = new HttpCookie("User");
                cookie1.Values.Add("LogoPath", setting.LogoPath);
                cookie1.Values.Add("FeviconPath", setting.FeviconPath);
                ViewBag.SiteLogo = setting.LogoPath;
                ViewBag.Name = setting.Name;
                Response.Cookies.Add(cookie1);
            }
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        public ActionResult Login(LoginViewModel objLogin)
        {
            HttpCookie cookie1 = new HttpCookie("User");
            //var password = dHelper.Encrypt(objLogin.Password);
            //var userLogin = db.tblUsers.FirstOrDefault(s =>
            //    s.Email == objLogin.Email || s.UserName == objLogin.Email && s.Password == password);
            var UserLogin = service.GetUser(objLogin.Email, objLogin.Password);
            //var Users = service.GetUsers();
            if (UserLogin != null)
            {
                cookie1.Values.Add("UserID", UserLogin.UserId.ToString());
                cookie1.Values.Add("UserName", UserLogin.Username.ToString());
                cookie1.Values.Add("DeptID", UserLogin.UserGroupCode.ToString());
                cookie1.Expires = DateTime.Now.AddHours(3);
                Response.Cookies.Add(cookie1);
                Settings.Instance.UserId = UserLogin.UserId;
                Settings.Instance.Site = UserLogin.Site;
                Settings.Instance.UserName = UserLogin.Username;
                Settings.Instance.Dept = UserLogin.UserGroupCode;
                //UserLogin.LastLoginDate = DateTime.Now;
                var a = Settings.Instance.UserId;
                dHelper.LogAction(UserLogin.Username + " successfully login.");
                //return RedirectToAction("dashboard", "Index");
                return Redirect("~/admin/dashboard");
            }
            else
            {
                ViewBag.Message = "<div class=\"alert alert-danger fade in\">Invalid Username and password.</div>.";
                return View();
            }
        }

        public ActionResult ForgotPassword()
        {
            var setting = db.tblSettings.FirstOrDefault();
            if (setting != null)
            {
                ViewBag.SiteLogo = setting.LogoPath;
                ViewBag.Name = setting.Name;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(LoginViewModel objLogin)
        {
            var userLogin = db.tblUsers.FirstOrDefault(s => s.Email == objLogin.Email || s.UserName == objLogin.Email);
            if (userLogin != null)
            {
                var returnValue = dMailHelper.SendMail(userLogin.Email, "Subject", "Body");
                if (returnValue == "Successful")
                    ViewBag.Message =
                        "<div class=\"alert alert-success fade in\">Password sent to your email address.</div>.";
                else
                    ViewBag.Message =
                        "<div class=\"alert alert-danger fade in\">" + returnValue + "</div>.";
                return View();
            }
            else
            {
                ViewBag.Message = "<div class=\"alert alert-danger fade in\">Invalid Username and password.</div>.";
                return View();
            }
        }

        //Logout user 
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            Response.Cookies["User"].Expires = DateTime.MinValue;
            Response.Cookies.Clear();
            HttpCookie cookie1 = new HttpCookie("User");
            cookie1.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie1);
            return RedirectToAction("Login", "Account");
        }
    }
}