using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerserAssetleasingServiceInterface.Authorize;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    [MyAuthorize]
    public class CarriersController : Controller
    {
        // GET: Carriers
        public ActionResult Index()
        {
            List<CarriersListViewModel> carriersdata = new List<CarriersListViewModel>();

            carriersdata = CarriersServicehelper.Projects().Result;

            return View(carriersdata);
        }

        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<CarriersListViewModel> Companymodel = new List<CarriersListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = CarriersServicehelper.Projects().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=CarrierList.xls");
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