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
    }
}