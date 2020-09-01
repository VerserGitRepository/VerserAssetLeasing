using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerserAssetleasingServiceInterface.Authorize;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;
using VerserAssetleasingServiceInterface.Utils;

namespace VerserAssetleasingServiceInterface.Controllers
{
    [MyAuthorize]
    public class CompanyController : Controller
    {
        private static string folder = HostingEnvironment.MapPath("~/");
        private static string blancco4filePath = "";
        private static string blancco5filePath = "";

        string _user = string.Empty;
        [OutputCache(CacheProfile = "OneHour", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            string _user=Session["Username"].ToString();
          ///  int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());

            var model = new CompanyAndSiteListViewModel();
            model.CompanyListViewModel = new List<CompanyListViewModel>();
            model.CompanySitesListViewModel = new List<CompanySitesListViewModel>();
            model.CompanyListViewModel = CompanyServicehelper.Projects(_user).Result;
            model.CompanySitesListViewModel = CompanyServicehelper.CompanySites(_user).Result;      
            model.AssetsListViewModel = new List<JBHiFiAssetsModel>();
            model.EndUsersListViewModel = new List<EndUsersListViewModel>();
            model.ContractsListViewModel = new List<ContractsListViewModel>();
            //Changes required on API
            model.AssetsListViewModel = AssetsServicehelper.GetAssetsData(_user).Result;
            model.EndUsersListViewModel = CompanyServicehelper.EndUsersByCompany(_user).Result;
            model.ContractsListViewModel = CompanyServicehelper.ContractsByCompany(_user).Result;          

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
                Companymodel = CompanyServicehelper.Projects(_user).Result;
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
        [HttpGet]
        public ActionResult SSNDetails(string SSN)
        {            
            CompanyAndSiteListViewModel model = new CompanyAndSiteListViewModel();
            model.CompanySitesListViewModel = new List<CompanySitesListViewModel>();
            return PartialView("SSNDetails", model);
        }
        [HttpGet]
        public ActionResult AssetPartialDiv(int Id)
        {
            string _user = Session["Username"].ToString();
            ///  int CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());

            var model = new CompanyAndSiteListViewModel();
           
            model.AssetsListViewModel = new List<JBHiFiAssetsModel>();
           
            model.AssetsListViewModel = AssetsServicehelper.GetAssetsData(Id.ToString()).Result;
            return PartialView("AssetPartialDiv",model);
        }
        [HttpGet]
        public ActionResult DownloadReport(string ssn)
        {
            blancco4filePath = Path.Combine(folder, @"web-service-request-export-report4.xml");
            blancco5filePath = Path.Combine(folder, @"web-service-request-export-report5.xml");
            
            PDFReportExporter exporter = new PDFReportExporter( blancco4filePath, ssn);
            byte[] buffer = exporter.GetReportXML();
            //Response.Buffer = true;
            //Response.Charset = "";

            //Response.AppendHeader("Content-Disposition", "attachment; filename=SSNPDF.pdf"); 


            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //Response.ContentType = "application/pdf";

            //Response.BinaryWrite(buffer);

            //Response.Flush();

            //Response.End();

            string fileName = "testFile.pdf";

            byte[] pdfasBytes = buffer;   // Here the fileBytes are already encoded (Encrypt) value. Just convert from string to byte
            Response.Clear();
            MemoryStream ms = new MemoryStream(pdfasBytes);
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.Flush();
            Response.End();
            return null;



        }
    
    }
}