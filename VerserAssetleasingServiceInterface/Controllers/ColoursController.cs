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
    public class ColoursController : Controller
    {
        // GET: Colours
        public ActionResult Index()
        {
            List<ColoursListViewModel> contractdata = new List<ColoursListViewModel>();

            contractdata = ColoursServicehelper.Projects().Result;

            return View(contractdata);
        }

        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<ColoursListViewModel> Companymodel = new List<ColoursListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = ColoursServicehelper.Projects().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ColoursList.xls");
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