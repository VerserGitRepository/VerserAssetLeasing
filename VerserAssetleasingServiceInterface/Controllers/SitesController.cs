using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class SitesController : Controller
    {
        // GET: Assets
        public ActionResult AddSite()
        {
           
            return PartialView();
        }
        public ActionResult UpdateSite()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult AddSiteInformation(CompanySitesListViewModel theModel)
        {
            var returnResult = SitesServicehelper.AddSiteInformation(theModel);
            return RedirectToAction("Index", "Company");
        }
        [HttpPost]
        public ActionResult UpdateSiteInformation(CompanySitesListViewModel theModel)
        {
            var returnResult = SitesServicehelper.UpdateSiteInformation(theModel);
            return RedirectToAction("Index", "Company");
        }
    }
}