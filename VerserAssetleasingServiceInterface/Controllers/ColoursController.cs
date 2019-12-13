using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class ColoursController : Controller
    {
        // GET: Colours
        public ActionResult Index()
        {
            List<ColoursListViewModel> contractdata = new List<ColoursListViewModel>();

            contractdata = ColoursServicehelper.Projects().Result;

            return View(contractdata);
        }
    }
}