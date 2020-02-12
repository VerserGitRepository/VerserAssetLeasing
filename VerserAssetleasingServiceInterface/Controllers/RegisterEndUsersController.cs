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
        [HttpGet]
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
        public ActionResult UpdateEndUser(EndUsersListViewModel ContractRegisterdata)
        {
            ReturnModel model = EndUsersServicehelper.UpdateEnduser(ContractRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return RedirectToAction("Index", "Company");
        }
        [HttpGet]
        public ActionResult UpdateEndUser(string Id)
        {
            List<EndUsersListViewModel> contractdata = new List<EndUsersListViewModel>();

            contractdata = EndUsersServicehelper.EndUsersList().Result;
            contractdata = contractdata.Where(i => i.Id == Convert.ToInt32(Id)).ToList();


            return PartialView(contractdata[0]);
        }
    }
}