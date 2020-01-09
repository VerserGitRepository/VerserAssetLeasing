﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class RegisterEndUsersController : Controller
    {
        // GET: RegisterEndUsers
        public ActionResult Index()
        {
            return View();
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
    }
}