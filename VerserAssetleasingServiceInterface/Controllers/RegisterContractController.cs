﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class RegisterContractController : Controller
    {
        // GET: Assets
        public ActionResult AddContract()
        {

            return PartialView();
        }
        public ActionResult UpdateContract()
        {

            return PartialView();
        }
        // GET: RegisterContract
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ContractsListViewModel ContractRegisterdata)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddContract(ContractsListViewModel ContractRegisterdata)
        {
            ReturnModel model = ContractsServicehelper.AddContract(ContractRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateContract(ContractsListViewModel ContractRegisterdata)
        {
            ReturnModel model = ContractsServicehelper.UpdateContract(ContractRegisterdata).Result;
            TempData["StatusMessage"] = model.Message;
            return View("Index");
        }
    }
}