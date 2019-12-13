using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class CarriersController : Controller
    {
        // GET: Carriers
        public ActionResult Index()
        {
            List<CarriersListViewModel> carriersdata = new List<CarriersListViewModel>();

            carriersdata = CarriersServicehelper.Projects().Result;

            return View(carriersdata);
        }
    }
}