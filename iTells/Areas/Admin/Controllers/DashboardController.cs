using iTells.Helper;
using iTells.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace iTells.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private iTellsEntities db = new iTellsEntities();
        private GeneralHelper dHelper = new GeneralHelper();

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            try
            {
                string Url = Request.RawUrl.ToString();
                var control = dHelper.GetUserPermission(Url);
                if (control.IsLogin == false)
                {
                    return Redirect("/account/login");
                }

                if (control.IsView == false)
                {
                    return Redirect("/admin/notaccess");
                }

                int TotalUser = 0, CurrentMonth = 0, ActiveUser = 0, InActiveUser = 0;
                // Total Member
                var userMast = db.tblUsers.Where(s => s.IsDeleted == false);
                if (userMast.Count() > 0)
                {
                    TotalUser = userMast.Count();
                }

                // Current Month Register Users
                var currentMonth = userMast.Where(s => s.InsertDate.Value.Month == DateTime.Now.Month).Count();
                if (currentMonth > 0)
                {
                    CurrentMonth = currentMonth;
                }

                // Unconfirmed Users
                var activeUser = userMast.Where(s => s.IsActive == true).Count();
                if (activeUser > 0)
                {
                    ActiveUser = activeUser;
                }

                // Banned Users
                var bannedUser = userMast.Where(s => s.IsActive == false).Count();
                if (bannedUser > 0)
                {
                    InActiveUser = bannedUser;
                }

                ViewBag.TotalUser = TotalUser;
                ViewBag.CurrentMonth = CurrentMonth;
                ViewBag.ActiveUser = ActiveUser;
                ViewBag.InActiveUser = InActiveUser;

                ViewBag.IsAdmin = control.IsAdmin;
                PopulateChart();
                PopulateLatestUser();

                
            }
            catch
            {
            }

            return View();
        }

        private void PopulateLatestUser()
        {
            var userInfo = db.tblUsers.Where(s => s.IsDeleted == false).OrderByDescending(s => s.InsertDate).Take(5);
            ViewBag.UserList = userInfo;
        }

        #region Dashboard Chart

        private void PopulateChart()
        {
            string MonthName = "", saleData = "";
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime reportDate = (DateTime.Now.AddMonths(0 - 11 + i));
                    MonthName += "\"" + ToMonthName(reportDate) + "\",";

                    var Data = db.tblUsers.Where(s =>
                        s.InsertDate.Value.Month == reportDate.Month && s.InsertDate.Value.Year == reportDate.Year);
                    if (Data.Count() > 0)
                    {
                        saleData += Data.Count().ToString() + ",";
                    }
                    else
                    {
                        saleData += "0,";
                    }
                }
            }
            catch { }
            ViewBag.MonthName = MonthName.Remove(MonthName.Length - 1);
            ViewBag.Data = saleData.Remove(saleData.Length - 1);
        }

        #endregion Dashboard Chart

        #region Header

        //Get Header
        public ActionResult GetHeader()
        {
            try
            {
                var permission = dHelper.GetUserPermission("");
                ViewBag.IsAdmin = permission.IsAdmin;

                ViewBag.UserID = dHelper.CurrentLoginUser();
                var setting = db.tblSettings.FirstOrDefault();
                if (setting != null)
                {
                    ViewBag.Name = setting.Name;
                }

                var userID = new Guid(dHelper.CurrentLoginUser());
                var userInfo = db.S_User.FirstOrDefault(s => s.UserId == userID);
                if (userInfo != null)
                {
                    ViewBag.UserName = userInfo.Username;
                    //ViewBag.PhotoPath = userInfo.PhotoPath;
                    ViewBag.RegisterDate = userInfo.EntryDate.Value.ToString("dd-MMM-yyyy");
                }
            }
            catch
            { }
            return PartialView("_Header");
        }

        #endregion Header

        public string ToMonthName(DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        #region Sidebar Menu

        public ActionResult SidebarMenu()
        {
            try
            {
                var userID = new Guid(dHelper.CurrentLoginUser());
                var permission = dHelper.GetUserPermission("");
                ViewBag.IsAdmin = permission.IsAdmin;
                var userInfo = db.S_User.FirstOrDefault(s => s.UserId == userID);
                if (userInfo != null)
                {
                    //ViewBag.PhotoPath = userInfo.PhotoPath;
                    string str = "";
                    var menuList = dHelper.GetMenuList(userInfo.RoleID, permission.IsAdmin);
                    if (menuList.Count() > 0)
                    {
                        foreach (var menu in menuList)
                        {
                            var subMenu = dHelper.GetSubMenuList(userInfo.RoleID, menu.ID, permission.IsAdmin);
                            if (subMenu.Count() > 0)
                            {
                                str += string.Format(
                                    "<li class=\"treeview\" id=\"{0}\"><a href=\"#\"><i class=\"fa {1}\"></i><span>{2}</span><i class=\"fa fa-angle-left pull-right\"></i></a>",
                                    menu.PageSlug, menu.PageIcon, menu.ModuleName);

                                str += "<ul class=\"treeview-menu\">";
                                foreach (var sub in subMenu)
                                {
                                    str += string.Format("<li id=\"{0}\"><a href=\"{1}\"><i class=\"fa {2}\"></i> {3}</a></li>",
                                        sub.PageSlug, sub.PageURL, sub.PageIcon, sub.ModuleName);
                                }

                                str += "</ul></li>";
                            }
                            else
                            {
                                str +=
                                    string.Format(
                                        "<li id=\"{0}\"><a href=\"{1}\"><i class=\"fa {2}\"></i> <span>{3}</span></a></li>",
                                        menu.PageSlug, menu.PageURL, menu.PageIcon, menu.ModuleName);
                            }
                        }
                    }
                    ViewBag.MenuList = str;
                    return PartialView("_Menu", null);
                }
                return PartialView("_Menu", null);
            }
            catch (Exception ex)
            {
                string Error = ex.Message.ToString();
                return PartialView("_Menu", null);
            }
        }

        #endregion Sidebar Menu
    }
}