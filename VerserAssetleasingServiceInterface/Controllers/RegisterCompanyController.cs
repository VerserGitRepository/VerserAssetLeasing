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
    public class RegisterCompanyController : Controller
    {
        // GET: RegisterCompany
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CompanyListViewModel CompanyEntryModel)
        {
            CompanyListViewModel model = new CompanyListViewModel();

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (CompanyEntryModel != null)
                {
                    var CompanyEntryRecord = new CompanyEntryModel()
                    {
                        companyName=CompanyEntryModel.companyName,

                    };

                    var returnstatus = CompanyServicehelper.CompanyAdd(CompanyEntryRecord);
                }
            }
            return View();
        }
    }
}