using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var _userroles=new List<UserRoleModel>();
            var _users = AdminHelperService.Users().Result;
            var _roles = AdminHelperService.Roles().Result;       

            return View(_users);
        }
    }
}