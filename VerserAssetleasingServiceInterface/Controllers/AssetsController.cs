using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class AssetsController : Controller
    {
        // GET: Assets
        public ActionResult Index()
        {
            List<AssetsListViewModel> assetsdata = new List<AssetsListViewModel>();

            assetsdata = AssetsServicehelper.Projects().Result;

            return View(assetsdata);
        }
    }
}