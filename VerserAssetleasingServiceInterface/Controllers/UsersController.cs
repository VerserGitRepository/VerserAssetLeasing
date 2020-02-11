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

            _userroles.UserIDs =new SelectList(AdminHelperService.Users().Result);
   
            _userroles.Companylist = new SelectList(CompanyServicehelper.CompanyList().Result, "ID", "Value");
            return View(_userroles);
        }
        public ActionResult Add(UserRoleModel theModel)
        {
            if (theModel.ResourceIDs.Count > 0 && theModel != null)
            {
                var retuenedresponse = AdminHelperService.ChangeUserPermissions(theModel).Result;
            }          
            return View();
        }
        [HttpGet]
        public ActionResult GetRegristrationModal()
        {
            return PartialView("NewUserRegistration");
        }
        [HttpPost]
        public ActionResult NewUserRegistration()
        {
            return RedirectToAction("Index");
        }
    }
}