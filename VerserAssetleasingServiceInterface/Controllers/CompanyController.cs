using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            CompanyAndSiteListViewModel model = new CompanyAndSiteListViewModel();
            model.CompanyListViewModel = new List<CompanyListViewModel>();
            model.CompanySitesListViewModel = new List<CompanySitesListViewModel>();

            model.CompanyListViewModel = CompanyServicehelper.Projects().Result;
            model.CompanySitesListViewModel = CompanyServicehelper.GetCompanies().Result;


            return View(model);
        }

        [HttpGet]
        public ActionResult GetCompanyData(int Id)
        {
            List<CompanySitesListViewModel> companydata = new List<CompanySitesListViewModel>();

            companydata = CompanyServicehelper.GetCompanyData(Id).Result;

            return Json(companydata, JsonRequestBehavior.AllowGet);
        }       

    }
}