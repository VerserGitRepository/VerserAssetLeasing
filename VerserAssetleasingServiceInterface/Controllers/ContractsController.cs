using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class ContractsController : Controller
    {
        // GET: Contracts
        public ActionResult Index()
        {
            List<ContractsListViewModel> contractdata = new List<ContractsListViewModel>();

            contractdata = ContractsServicehelper.Projects().Result;

            return View(contractdata);
        }
    }
}