using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class CostModelBuilderController : Controller
    {
       
        public  ActionResult GenerateQuote()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
              // await CostModelServicesHelpers.CostModelMailNotificationRequestor(new JBHiFiCostmodelServiceRequestDetailsDto());
                var Quotesmodel = new JBHIFiCostModelQuoteRequests();
                //  Quotesmodel. = 

                Quotesmodel.PostQuoteRequestModelLIST = QuoteRequestHelperService.GetTQuotes().Result;
                Quotesmodel.CostModelServices = new SelectList(CostModelServicesHelpers.GetCostModelServices().Result, "ID", "Value");
                Quotesmodel.ProjectManagerList = new SelectList(ListItemHelperServices.ProjectManagerList().Result, "ID", "Value");
                Quotesmodel.SalesManagerList = new SelectList(ListItemHelperServices.SalesManagerList().Result, "ID", "Value");
                Quotesmodel.CostModelServicesCategories = new SelectList(CostModelServicesHelpers.GetCostModelServiceCategories().Result, "ID", "Value");               
                return View(Quotesmodel);
            }
        }
        [HttpPost]
        public ActionResult CreateSalesForceOpportunity(SalesForceOpportunity opportunity)
        {
            string opportunityNumber = "";
            string salesForceUniqueId = "";
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (CostModelServicesHelpers.CreateSalesForceOpportunity(opportunity, out opportunityNumber, out salesForceUniqueId))
                {

                    var RequestQuoteModel = new PostQuoteRequestModel();
                    RequestQuoteModel.ProjectManager = "Arman";
                    RequestQuoteModel.SalesManager = "Danny";
                    RequestQuoteModel.VerserBranch = "Sydney";
                    RequestQuoteModel.CustomerName = opportunity.Customer;
                    RequestQuoteModel.CustomerContactName = opportunity.CustomerContactName;
                    RequestQuoteModel.OpportunityNumber = Convert.ToInt32(opportunityNumber);
                    RequestQuoteModel.OpportunityName = opportunity.OpportunityName;
                    RequestQuoteModel.SiteAddress = opportunity.SiteAddress;
                    RequestQuoteModel.StartDate = opportunity.StartDate;
                    RequestQuoteModel.Email = opportunity.Email;

                    var IsRequestCompleted = QuoteRequestHelperService.PostQuoteRequest(RequestQuoteModel).Result;
                    return Json("Salesforce Opportunity has been successfully created with Opportunity Number :" + opportunityNumber + " - Id:" + IsRequestCompleted.Id + "-salesForceUniqueId:" + salesForceUniqueId, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return new JsonResult { Data = "An error occurred while processing the request.", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }

        }

        [HttpPost]
        public ActionResult RequestServiceQuote(PostQuoteRequestModel RequestQuoteModel)
        {
            RequestQuoteModel.ProjectManager = "Arman";
            RequestQuoteModel.CustomerName = "JB HI FI APEX";
            RequestQuoteModel.SalesManager = "Danny";
            RequestQuoteModel.VerserBranch = "Sydney";
            RequestQuoteModel.CustomerContactName = "Basan";
            RequestQuoteModel.OpportunityNumber = 208784;
            RequestQuoteModel.OpportunityName = "Reuest of Virtual server setup";
            RequestQuoteModel.SiteAddress = "91 B bridge rd westmead NSW";
            RequestQuoteModel.StartDate = DateTime.Now;
            var IsRequestCompleted = QuoteRequestHelperService.PostQuoteRequest(RequestQuoteModel);

            //RequestQuoteModel.QuoteServiceItems.Add(new JBHIFiCostModelServiceItems { FK_JBHIFiCostModelServiceID =1,Quantity=10 });
            //RequestQuoteModel.QuoteServiceItems.Add(new JBHIFiCostModelServiceItems { FK_JBHIFiCostModelServiceID = 2, Quantity = 10 });
            //RequestQuoteModel.QuoteServiceItems.Add(new JBHIFiCostModelServiceItems { FK_JBHIFiCostModelServiceID = 3, Quantity = 10 });
            //;

            return View();
        }
        [HttpGet]
        public ActionResult GetCostModelServices()
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = new CostServiceModel();
                model.CostModelServices = new SelectList(CostModelServicesHelpers.GetCostModelServices().Result, "ID", "Value");
                model.CostModelServicesCategories = new SelectList(CostModelServicesHelpers.GetCostModelServiceCategories().Result, "ID", "Value");
                return PartialView("CostModelServicePopup", model);

            }
        }

        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {

            List<JBHIFiCostModelQuoteRequests> QuoteModel = new List<JBHIFiCostModelQuoteRequests>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                string CurrentDate;
                DateTime saveNow = DateTime.Now;
                CurrentDate = saveNow.Date.ToShortDateString();
                string reportContent ="";
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Assets/CostModelNewQuote.xls");
                Byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                using (StreamWriter outfile = new StreamWriter(filePath, true))
                {
                    outfile.WriteLine(reportContent);
                }

                System.IO.FileInfo file = new System.IO.FileInfo(filePath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            return RedirectToAction("GenerateQuote", "CostModelBuilder");
        }
        [HttpGet]
        public ActionResult CostModelOppDetails(int id)
        {

            var AllRecords = new JBHiFiCostmodelServiceRequestDetailsModel();
            var  ReturnAllRecords = QuoteRequestHelperService.JBHiFiCostmodelServiceRequestDetails(id);
            if (ReturnAllRecords.Result != null)
            {
                AllRecords = ReturnAllRecords.Result;
            }
            if (AllRecords.ServiceItemsLists.Count == 0)
            {
                JBHIFiCostModelServiceItems item = new JBHIFiCostModelServiceItems();
                AllRecords.ServiceItemsLists.Add(item);
            }
            AllRecords.CostModelQuoteRequestModel.CostModelServices = new SelectList(CostModelServicesHelpers.GetCostModelServices().Result, "ID", "Value");
            return PartialView("CostModelOppDetails", AllRecords);
        }

        [HttpGet]
        public ActionResult AddSalesForceOpportunity()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                SalesForceOpportunity model = new SalesForceOpportunity();
                model.JBHIFiCostModelQuoteRequest = new JBHIFiCostModelQuoteRequests();
                model.JBHIFiCostModelQuoteRequest.ProjectManagerList = new SelectList(ListItemHelperServices.ProjectManagerList().Result, "ID", "Value");
                model.JBHIFiCostModelQuoteRequest.SalesManagerList = new SelectList(ListItemHelperServices.SalesManagerList().Result, "ID", "Value");
                List<ListItemViewModel> accountNames = new List<ListItemViewModel>();
                List<ListItemViewModel> oppNames = new List<ListItemViewModel>();
                //oppNames = CostModelServicesHelpers.GetAccountAndCustomerNames(out accountNames);
                //model.AccountNames = new SelectList(accountNames, "ID", "Value");
                //model.OpportunityNames = new SelectList(oppNames, "ID", "Value");
                return PartialView("CostModelServicePopup",model);
            }
        }
        [HttpGet]
        public ActionResult GetTotalPrice(int value1, int value2)
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var result = CostModelServicesHelpers.GetPrice(value1, value2).Result;
                return new JsonResult { Data = result.TotalPrice, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }


        }
        [HttpPost]
        public ActionResult SubmitQuote(List<JBHIFiCostModelServiceItems> RequestQuoteModel)
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                JBHIFiCostModelServiceItemsSummary summary = new JBHIFiCostModelServiceItemsSummary();
                summary.TOTAL_Excl_GST = 0;
                summary.TOTAL_Incl_GST = 0;
                summary.GST_10 = 0;
                summary.JBHIFiCMServiceItems = RequestQuoteModel;

                foreach (JBHIFiCostModelServiceItems item in RequestQuoteModel)
                {
                    item.GST_10 = ((Convert.ToDecimal(0.1)) * item.TotalPrice);
                    item.TOTAL_Excl_GST =  item.TotalPrice;
                    item.TOTAL_Incl_GST = item.GST_10 + item.TOTAL_Excl_GST;
                    summary.Summary += item.Summary + Environment.NewLine;
                    summary.GST_10 += item.GST_10;
                    summary.TOTAL_Incl_GST += item.TOTAL_Incl_GST;
                    summary.TOTAL_Excl_GST += item.TOTAL_Excl_GST;
                    summary.ServiceQuoteRequestId = item.FK_JBHIFIQuoteRequestID;
                    summary.TotalPrice = item.TotalPrice;
                    summary.SalesForceUniqueId = item.SalesForceUniqueId;

                }

                var result = QuoteRequestHelperService.AddQuoteServiceItems(summary).Result;
                return new JsonResult { Data = result.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpPost]
        public ActionResult SubmitChangedQuote(List<JBHIFiCostModelServiceItems> RequestQuoteModel)
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                JBHIFiCostModelServiceItemsSummary summary = new JBHIFiCostModelServiceItemsSummary();
                summary.TOTAL_Excl_GST = 0;
                summary.TOTAL_Incl_GST = 0;
                summary.GST_10 = 0;
                summary.JBHIFiCMServiceItems = RequestQuoteModel;

                foreach (JBHIFiCostModelServiceItems item in RequestQuoteModel)
                {
                    item.GST_10 = ((Convert.ToDecimal(0.1)) * item.TotalPrice);
                    item.TOTAL_Excl_GST = item.TotalPrice;
                    item.TOTAL_Incl_GST = item.GST_10 + item.TOTAL_Excl_GST;
                    summary.Summary += item.Summary + Environment.NewLine;
                    summary.GST_10 += item.GST_10;
                    summary.TOTAL_Incl_GST += item.TOTAL_Incl_GST;
                    summary.TOTAL_Excl_GST += item.TOTAL_Excl_GST;
                    summary.ServiceQuoteRequestId = item.FK_JBHIFIQuoteRequestID;
                    summary.TotalPrice = item.TotalPrice;
                    summary.SalesForceUniqueId = item.SalesForceUniqueId;

                }

                var result = QuoteRequestHelperService.SubmitChangedQuote(summary).Result;
               
                return new JsonResult { Data = result.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpPost]
        public ActionResult ExportToExcel(JBHiFiCostmodelServiceRequestDetailsModel RequestQuoteModel)
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ExcelHelper.GenerateExcelCostModel(RequestQuoteModel);

                


                //GridView gv = new GridView();
                //gv.DataSource = null;
                //gv.DataBind();
                //Response.ClearContent();
                //Response.Buffer = true;
                //Response.AddHeader("content-disposition", "attachment; filename=OpportunityList.xls");
                //Response.ContentType = "application/ms-excel";
                //Response.Charset = "";
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                //gv.RenderControl(htw);
                //Response.Output.Write(sw.ToString());
                //Response.Flush();
                //Response.End();
                return new JsonResult { Data = "Exported", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               


            }
        }
        [HttpGet]
        public ActionResult GetQuotesList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var Quotesmodel = new JBHIFiCostModelQuoteRequests();
                //  Quotesmodel. = 

                List<PostQuoteRequestModel> PostQuoteRequestModelLIST = QuoteRequestHelperService.GetTQuotes().Result;                             

                return Json(PostQuoteRequestModelLIST, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public ActionResult ExportToExcel(JBHiFiCostmodelServiceRequestDetailsDto CostModelMailViewModel)
        //{
        //    if (Session["Username"] == null)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //    else
        //    {
        //        var Quotesmodel = new JBHIFiCostModelQuoteRequests();
        //        //  Quotesmodel. = 
        //        var result = CostModelServicesHelpers.CostModelMailNotification(CostModelMailViewModel).Result;
        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    }
        //}
    }
}