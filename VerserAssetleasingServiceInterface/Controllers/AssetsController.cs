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
    public class AssetsController : Controller
    {
        // GET: Assets
        public ActionResult Index()
        {
            List<AssetsListViewModel> assetsdata = new List<AssetsListViewModel>();

            assetsdata = AssetsServicehelper.AssetsList().Result;

            return View(assetsdata);
        }

        [HttpGet]
        public ActionResult GetAssetsData(int Id)
        {
            List<AssetsListViewModel> companydata = new List<AssetsListViewModel>();

            companydata = AssetsServicehelper.GetAssetsData(Id).Result;

            return Json(companydata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<AssetsListViewModel> Companymodel = new List<AssetsListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = AssetsServicehelper.AssetsList().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=AssetList.xls");
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