using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;
namespace VerserAssetleasingServiceInterface.Controllers
{
    public class DeviceCategoriesController : Controller
    {
        // GET: DeviceCategories
        public ActionResult Index()
        {
            List<DeviceCategoriesListViewModel> devicecategoriesdata = new List<DeviceCategoriesListViewModel>();

            devicecategoriesdata = DeviceCategoriesServicehelper.Projects().Result;

            return View(devicecategoriesdata);
        }
    }
}