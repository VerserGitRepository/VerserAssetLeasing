﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class EndUsersController : Controller
    {
        // GET: EndUsers
        public ActionResult Index()
        {
            List<EndUsersListViewModel> enduserdata = new List<EndUsersListViewModel>();

            enduserdata = EndUsersServicehelper.Projects().Result;

            return View(enduserdata);
        }
    }
}