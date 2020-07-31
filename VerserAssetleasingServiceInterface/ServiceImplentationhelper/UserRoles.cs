using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VerserAssetleasingServiceInterface.ServiceHelper;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class UserRoles
    {
        public static bool UserAdminAccess()
        {
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "Administrator"
            || r.Value == "Accounts" || r.Value == "WarehouseManager" || r.Value == "ProjectmanagerAdmin").FirstOrDefault() != null ? true : false;
        }
    }
}