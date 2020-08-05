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
    public class RegisterAssetsController : Controller
    {
        // GET: Assets
        public ActionResult AddAssets()
        {

            return PartialView();
        }
        public ActionResult UpdateAssets(string AssetId,string companyId)
        {
           var assetsdata = new List<JBHiFiAssetsModel>();
           
            assetsdata = AssetsServicehelper.GetAssetsData(companyId).Result;
            var model = assetsdata.Where(item => item.AssetID == AssetId).FirstOrDefault();
            return PartialView(model);
        }
        // GET: RegisterAssets
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Company");
        }
        [HttpPost]
        public ActionResult Index(AssetsListViewModel AssetsRegisterdata)
        {
            ReturnModel model = AssetsServicehelper.AddAsset(AssetsRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        public ActionResult UpdateAsset(AssetsListViewModel AssetsRegisterdata)
        {
            ReturnModel model = AssetsServicehelper.UpdateAsset(AssetsRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return RedirectToAction("Index", "Company");
        }
    }
}