using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class InventoryItemsController : Controller
    {
        // GET: InventoryItems
        public ActionResult Index()
        {
            List<InventoryItemsListViewModel> inventoryitemsdata = new List<InventoryItemsListViewModel>();

            inventoryitemsdata = InventoryItemsServicehelper.Projects().Result;

            return View(inventoryitemsdata);
        }
    }
}