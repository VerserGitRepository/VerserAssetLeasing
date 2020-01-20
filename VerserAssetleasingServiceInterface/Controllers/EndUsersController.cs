using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
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
        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<EndUsersListViewModel> Companymodel = new List<EndUsersListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = EndUsersServicehelper.Projects().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=EndUserList.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index", "Company");
        }
    }
}