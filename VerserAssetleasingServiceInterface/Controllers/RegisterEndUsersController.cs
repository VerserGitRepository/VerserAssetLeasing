using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;

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
    }
}