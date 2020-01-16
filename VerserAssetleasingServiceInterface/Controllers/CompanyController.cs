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
       int CompanyID = 100000;
        public ActionResult Index()
        {
            CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
            var model = new CompanyAndSiteListViewModel();
            model.CompanyListViewModel = new List<CompanyListViewModel>();
            model.CompanySitesListViewModel = new List<CompanySitesListViewModel>();
            model.CompanyListViewModel = CompanyServicehelper.Projects(CompanyID).Result;
            model.CompanySitesListViewModel = CompanyServicehelper.CompanySites(CompanyID).Result;
       // model.CompanySitesListViewModel = CompanyServicehelper.CompanySites(7).Result;
            model.AssetsListViewModel = new List<AssetsListViewModel>();
            model.EndUsersListViewModel = new List<EndUsersListViewModel>();
            model.ContractsListViewModel = new List<ContractsListViewModel>();
            model.AssetsListViewModel = CompanyServicehelper.Assets().Result;
            model.EndUsersListViewModel = CompanyServicehelper.EndUsers().Result;
            model.ContractsListViewModel = CompanyServicehelper.Contracts().Result;          
            return View(model);
        }

        [HttpGet]
        public ActionResult GetCompanyData(int Id)
        {
            List<CompanySitesListViewModel> companydata = new List<CompanySitesListViewModel>();

            companydata = CompanyServicehelper.GetCompanyData(Id).Result;

            return Json(companydata, JsonRequestBehavior.AllowGet);
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
                Companymodel = CompanyServicehelper.Projects(CompanyID).Result;
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
        [HttpPost]
        public ActionResult UpdateCompanyData(CompanyListViewModel theModel)
        {
            var result = CompanyServicehelper.UpdateCompany(theModel);
            return Json(result);
        }
    }
}