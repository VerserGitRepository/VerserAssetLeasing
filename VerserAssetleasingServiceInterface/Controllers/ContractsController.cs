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
    public class ContractsController : Controller
    {
        // GET: Contracts
        public ActionResult Index()
        {
            List<ContractsListViewModel> contractdata = new List<ContractsListViewModel>();

            contractdata = ContractsServicehelper.Projects().Result;

            return View(contractdata);
        }

        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<ContractsListViewModel> Companymodel = new List<ContractsListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = ContractsServicehelper.Projects().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ContractsList.xls");
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
        [HttpGet]
        public ActionResult GetContractsData(int CompanyId)
        {
            List<ContractsListViewModel> contractdata = new List<ContractsListViewModel>();

            contractdata = ContractsServicehelper.Projects().Result;
            contractdata = contractdata.Where(i => i.contract_Company == Convert.ToString(CompanyId)).ToList();

            return Json(contractdata, JsonRequestBehavior.AllowGet);
        }
    }
}