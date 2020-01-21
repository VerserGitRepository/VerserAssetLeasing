using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Authorize
{
    public class MyAuthorizeAttribute :AuthorizeAttribute
    {
         protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        if (httpContext.Session["Username"] == null)
            return false;
        else
            return true;
    }
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectResult("~/Login/Login");  //return RedirectToAction("Login", "Login");
    }
    }
}