using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using System.IO;
using System.Globalization;
using iTells.Models;

namespace iTells.Helper
{
    public class GeneralHelper
    {
        iTellsEntities db = new iTellsEntities();
        public static Boolean IsDemo = false;

        public class UserControl
        {
            public Boolean IsAdd { get; set; }
            public Boolean IsEdit { get; set; }
            public Boolean IsDelete { get; set; }
            public Boolean IsView { get; set; }
            public Boolean IsLogin { get; set; }
            public Boolean IsAdmin { get; set; }
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }        

        public string CurrentLoginUser()
        {
            if (HttpContext.Current.Request.Cookies["User"] != null)
                return HttpContext.Current.Request.Cookies["User"].Values["UserID"];
            else
                return "";
        }

        public string CurrentLoginUserName()
        {
            if (HttpContext.Current.Request.Cookies["User"] != null)
                return HttpContext.Current.Request.Cookies["User"].Values["UserName"];
            else
                return "";
        }

        public string CurrentLoginDeptID()
        {
            if (HttpContext.Current.Request.Cookies["User"] != null)
                return HttpContext.Current.Request.Cookies["User"].Values["DeptID"];
            else
                return "";
        }

        public void LogAction(String Message)
        {
            try
            {
                System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
                Guid UserID = new Guid(CurrentLoginUser());
                Models.tblUserLog lg = new Models.tblUserLog();
                lg.ID = Guid.NewGuid();
                lg.Message = Message;
                lg.MoreInfo = browser.Browser + " " + browser.Version;
                lg.LogTime = DateTime.Now;
                lg.ID = UserID;
                lg.IPAddress = GetVisitorIPAddress();
                db.tblUserLogs.Add(lg);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
        
        // get visitor ip address
        public string GetVisitorIPAddress()
        {
            string IPAdd = string.Empty;
            IPAdd = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return IPAdd;
        }

        // generate module url
        public string GetModuleSlug(String ModuleName, int? ParentModuleID)
        {
            string result = string.Empty;
            ModuleName = ModuleName.Replace(" ", "");
            if (String.IsNullOrEmpty(result))
                result = ModuleName;
            if (ParentModuleID > 0)
                result = GetModuleByID(ParentModuleID).ModuleName.Replace(" ", "") + "_" + result;            
            return RemoveIllegalCharacters(result);
        }

        //get role name
        public string GerRoleName(Guid? RoleID)
        {
            string str = "";
            var role = db.tblRoles.FirstOrDefault(s => s.ID == RoleID);
            if (role != null)
                str = role.Name;
            return str;
        }

        public Models.tblModule GetModuleByID(int? CategoryID)
        {
            var module = (from c in db.tblModules
                          where c.ID == CategoryID
                          select c).FirstOrDefault();
            return module;
        }

        private string RemoveExtraHyphen(string text)
        {
            if (text.Contains("__"))
            {
                text = text.Replace("__", "_");
                return RemoveExtraHyphen(text);
            }
            return text;
        }

        private string RemoveDiacritics(string text)
        {
            string Normalize = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= Normalize.Length - 1; i++)
            {
                char c = Normalize[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace("'", string.Empty);
            //text = text.Replace("_", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text.ToLower()).Replace("%", string.Empty);
        }

        public UserControl GetUserPermission(string pageName)
        {
            string[] pageSplit = pageName.Split('/');
            UserControl control = new UserControl();
            control.IsAdd = false;
            control.IsDelete = false;
            control.IsEdit = false;
            control.IsView = false;
            control.IsLogin = false;
            control.IsAdmin = false;

            var userID = CurrentLoginUser();
            if (userID == "")
                return control;
            else
                control.IsLogin = true;

            try
            {
                Guid UserID = new Guid(userID);
                var userInfo = db.S_User.FirstOrDefault(s => s.UserId == UserID);
                if (userInfo != null)
                {
                    var role = db.tblRoles.FirstOrDefault(s => s.ID == userInfo.RoleID && s.Name.ToLower() == "administrator");
                    if (role != null)
                    {
                        control.IsAdmin = true;
                        control.IsAdd = true;
                        control.IsDelete = true;
                        control.IsEdit = true;
                        control.IsView = true;
                        return control;
                    }
                    string moduleName = "";
                    if (pageSplit.Length > 1)
                        moduleName = pageSplit[pageSplit.Length - 1].ToLower();
                    var module = db.tblModules.FirstOrDefault(s => s.IsDeleted == false && s.ModuleName == moduleName);
                    if (module != null)
                    {
                        var roleModule = db.tblRolePermissions.FirstOrDefault(s => s.RoleID == userInfo.RoleID && s.ModuleID == module.ID);
                        if (roleModule != null)
                        {
                            if (roleModule.IsView == true)
                                control.IsView = true;
                            if (roleModule.IsAdd == true)
                                control.IsAdd = true;
                            if (roleModule.IsDeleted == true)
                                control.IsDelete = true;
                            if (roleModule.IsEdit == true)
                                control.IsEdit = true;
                            return control;
                        }
                    }
                }
                else
                {
                    control.IsAdd = true;
                    control.IsDelete = true;
                    control.IsEdit = true;
                    control.IsView = true;
                }
            }
            catch
            {
                control.IsLogin = false;
            }
            return control;
        }

        public List<MenuViewModel> GetMenuList(Guid? RoleID, Boolean IsAdmin)
        {
            var menuList = db.tblModules.AsEnumerable()
                .Where(s => s.IsActive == true && s.IsDeleted == false && s.ParentModuleID == 0).Select(s =>
                    new MenuViewModel
                    {
                        ID = s.ID,
                        ModuleName = s.ModuleName,
                        DisplayOrder = s.DisplayOrder,
                        ParentModuleID = s.ParentModuleID,
                        PageIcon = s.PageIcon,
                        PageURL = s.PageUrl,
                        PageSlug = s.PageSlug,
                    }).ToList();
            if (IsAdmin != true)
            {
                menuList = (from m in menuList
                    join m1 in db.tblRolePermissions on m.ID equals m1.ModuleID
                    where m1.IsView == true && m1.RoleID == RoleID
                    select m).ToList();
            }

            return menuList.OrderBy(s => s.DisplayOrder).ToList();
        }

        public List<SubMenuViewModel> GetSubMenuList(Guid? RoleID, int? ParentID,Boolean IsAdmin)
        {
            
            var menuList = db.tblModules.AsEnumerable().Where(s => s.IsActive == true && s.IsDeleted == false && s.ParentModuleID == ParentID).Select(s => new SubMenuViewModel
            {
                ID = s.ID,
                ModuleName = s.ModuleName,
                DisplayOrder = s.DisplayOrder,
                PageIcon = s.PageIcon,
                PageURL = s.PageUrl,
                PageSlug = s.PageSlug,
            }).ToList();
            if (IsAdmin != true)
            {
                menuList = (from m in menuList
                            join m1 in db.tblRolePermissions on m.ID equals m1.ModuleID
                            where m1.IsView == true && m1.RoleID == RoleID
                            select m).ToList();
            }
            return menuList.OrderBy(s => s.DisplayOrder).ToList();
        }

        public string UserStatus(string status)
        {
            status = status.ToLower();
            string lblStatus = "";
            if (status == "active")
                lblStatus = "<span class=\"label label-success\">Active</span>";
            else if (status == "inactive")
                lblStatus = "<span class=\"label label-danger\">Banned</span>";
            else if (status == "unconfirmed")
                lblStatus = "<span class=\"label label-warning\">Unconfirmed</span>";
            return lblStatus;
        }

        #region Role Permission
        public string CheckRolePermission(string type, int? ModuleID, Guid? RoleID)
        {
            string str = "";
            var role = db.tblRoles.FirstOrDefault(s => s.ID == RoleID && s.Name.ToLower() == "admin");
            if (role != null)
                return "checked";
            var permission = db.tblRolePermissions.Where(s => s.RoleID == RoleID && s.ModuleID == ModuleID).ToList();
            foreach (var item in permission)
            {
                if (type == "view")
                {
                    if (item.IsView == true)
                        str = "checked";
                }
                if (type == "create")
                {
                    if (item.IsAdd == true)
                        str = "checked";
                }
                if (type == "edit")
                {
                    if (item.IsEdit == true)
                        str = "checked";
                }
                if (type == "delete")
                {
                    if (item.IsDeleted == true)
                        str = "checked";
                }
                if (str != "")
                    return str;
            }
            return str;
        }
        #endregion

        #region ActivityLog List
        public List<ActivityViewModel> GetActivityLog(Guid UserID)
        {            
            var activity = (from a in db.tblUserLogs
                            join u in db.tblUsers on a.ID equals u.ID
                            select new ActivityViewModel {UserID=u.ID, UserName = u.UserName, Message = a.Message, LogTime = a.LogTime, IPAddress = a.IPAddress });
            if (UserID != Guid.Empty)                       
                activity = activity.Where(s => s.UserID == UserID);
            
            activity = activity.OrderBy(s => s.LogTime);
            return activity.ToList();
        }
        #endregion

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        public string DecryptURL(string cipherText)
        {
            if ( cipherText != null)
            {
                var cipherText_ = cipherText.Replace("-", "+");
                cipherText_ = cipherText_.Replace(" ", "/");

                var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
                var iv = Encoding.UTF8.GetBytes("8080808080808080");

                var encrypted = Convert.FromBase64String(cipherText_);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
                               
                return string.Format(decriptedFromJavascript);
            } else
            {
                return string.Format("");
            }          

        }
    }
}