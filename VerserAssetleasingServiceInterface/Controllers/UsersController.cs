using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Authorize;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    [MyAuthorize]
    public class UsersController : Controller
    {       
        public ActionResult Index()
        {
            var _userroles=new UserRoleModel();
            _userroles.Usermodel = AdminHelperService.Users().Result;
            _userroles.Rolemodel = AdminHelperService.Roles().Result;
            _userroles.Companylist=CompanyServicehelper.CompanyList().Result;
            return View(_userroles);
        }
    }
}