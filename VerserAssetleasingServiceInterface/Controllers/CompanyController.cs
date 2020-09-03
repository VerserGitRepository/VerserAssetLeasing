
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
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
        string VDBlanccoPDFReports = ConfigurationManager.AppSettings["VDBlanccoPDFReports"];        
       // string folder = ConfigurationManager.AppSettings["LocationBlanccoPDFReports"];
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

            var model = new SSNModel();           
            return PartialView("AssetPartialDiv",model);
        }
        //[HttpPost]
        //public JsonResult DownloadReport(string SSNNumber)
        //{            
        //    blancco4filePath = Path.Combine(folder, @"web-service-request-export-report4.xml");
        //    blancco5filePath = Path.Combine(folder, @"web-service-request-export-report5.xml");
            
        //    PDFReportExporter exporter = new PDFReportExporter( blancco4filePath, SSNNumber);
        //    exporter.GetReportXML();
        //    return Json(new { fileName = VDBlanccoPDFReports + SSNNumber + ".pdf", errorMessage = "" });
        //}
        [HttpGet]
        //Action Filter, it will auto delete the file after download, 
       
        public ActionResult Download(string file)
        {
            //get the temp folder and file path in server
            string fullPath =  file;
            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
           
            return File(fullPath, "application/pdf", "BlanccoDeviceCertificateReport.pdf");

        }

        [HttpPost]
        public JsonResult GenerateReport(string[] SSNNumber)
        {
            string tempPath = Path.Combine(folder, "TempFile.xml");
            string filePath = Path.Combine(folder, @"web-service-request-export-report4.xml");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            XmlWriter xmlWriter = XmlWriter.Create(filePath);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("request");
            xmlWriter.WriteStartElement("export-report");
            xmlWriter.WriteStartElement("report");
            xmlWriter.WriteAttributeString("mode", "original");
            xmlWriter.WriteEndElement();
            foreach (string ssn in SSNNumber)
            {
                xmlWriter.WriteStartElement("search");
                xmlWriter.WriteAttributeString("path", "user_data.fields.SSN");

                xmlWriter.WriteAttributeString("value", ssn);

                xmlWriter.WriteAttributeString("operator", "eq");
                xmlWriter.WriteAttributeString("datatype", "string");
                xmlWriter.WriteAttributeString("conjunction", "false");
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteStartElement("pdf-options");
            xmlWriter.WriteAttributeString("pdfLanguage", "en_US");
            xmlWriter.WriteAttributeString("embedBarcode", "false");
            xmlWriter.WriteAttributeString("type", "albus_standard");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            PDFReportExporter exporter = new PDFReportExporter(filePath, SSNNumber.ToString());
            exporter.GetReportXML();
            return Json(new { fileName = VDBlanccoPDFReports + "BlanccoDeviceCertificateReport.pdf", errorMessage = "" });
        }
    }
}