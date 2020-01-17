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

            assetsdata = AssetsServicehelper.AssetsList().Result;

            return View(assetsdata);
        }

        [HttpGet]
        public ActionResult GetAssetsData(int Id)
        {
            List<AssetsListViewModel> companydata = new List<AssetsListViewModel>();

            companydata = AssetsServicehelper.GetAssetsData(Id).Result;

            return Json(companydata, JsonRequestBehavior.AllowGet);
        }
    }
}