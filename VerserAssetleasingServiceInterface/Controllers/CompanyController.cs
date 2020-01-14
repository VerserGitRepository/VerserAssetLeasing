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
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            List<CompanyListViewModel> companydata = new List<CompanyListViewModel>();

            companydata = CompanyServicehelper.Projects().Result;

            return View(companydata);
        }

        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<CompanyListViewModel> Companymodel = new List<CompanyListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = CompanyServicehelper.Projects().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=CompanyList.xls");
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