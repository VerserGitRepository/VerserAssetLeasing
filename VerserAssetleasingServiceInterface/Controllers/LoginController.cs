using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceHelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult login(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                Session["Username"] = null;
                Session["ErrorMessage"] = "Username Or Password Is Empty!";
                return View("Login");
            }
            LoginModel user = new LoginModel { UserName = login.UserName, Password = login.Password };
            Task<LoginModel> userReturn = LoginService.Login(user);
            if (userReturn.Result.IsLoggedIn == true)
            {
                Session["Username"] = login.UserName;     
                Session["ErrorMessage"] = null;
                return RedirectToAction("Index", "Company");
            }
            else
            {
                Session["FullUserName"] = null;
                Session["ErrorMessage"] = "Invalid UserName Or Password";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
           
            Session.Clear();
            
            return View("Login");
        }

    }
}