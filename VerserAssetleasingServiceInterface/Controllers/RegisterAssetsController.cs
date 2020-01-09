using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class RegisterAssetsController : Controller
    {
        // GET: RegisterAssets
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AssetsListViewModel AssetsRegisterdata)
        {
            ReturnModel model = AssetsServicehelper.AddAsset(AssetsRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return View("Index");
        }
    }
}