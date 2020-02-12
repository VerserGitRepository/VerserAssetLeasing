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
    public class RegisterEndUsersController : Controller
    {
        // GET: RegisterEndUsers
        public ActionResult Index()
        {
            return View();
        }
        // GET: Assets
        public ActionResult AddEnduser()
        {

            return PartialView();
        }
        public ActionResult UpdateEnduser()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult Index(EndUsersListViewModel EndUsersRegisterdata)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEndUser(EndUsersListViewModel theModel)
        {
            ReturnModel model = EndUsersServicehelper.AddEndUser(theModel).Result;
            TempData["StatusMessage"] = model.Message;
            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateEndUser(ContractsListViewModel ContractRegisterdata)
        {
            ReturnModel model = ContractsServicehelper.UpdateContract(ContractRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return RedirectToAction("Index", "Company");
        }
        [HttpGet]
        public ActionResult UpdateContract(string Id)
        {
            List<ContractsListViewModel> contractdata = new List<ContractsListViewModel>();

            contractdata = ContractsServicehelper.Projects().Result;
            contractdata = contractdata.Where(i => i.id == Convert.ToInt32(Id)).ToList();


            return PartialView(contractdata[0]);
        }
    }
}