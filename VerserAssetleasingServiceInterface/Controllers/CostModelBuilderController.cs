using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;

namespace VerserAssetleasingServiceInterface.Controllers
{
    public class CostModelBuilderController : Controller
    {
       
        public ActionResult GenerateQuote()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var Quotesmodel = new JBHIFiCostModelQuoteRequests();
                //  Quotesmodel. = 

                Quotesmodel.PostQuoteRequestModelLIST = QuoteRequestHelperService.GetTQuotes().Result;
                Quotesmodel.CostModelServices = new SelectList(CostModelServicesHelpers.GetCostModelServices().Result, "ID", "Value");
                Quotesmodel.CostModelServicesCategories = new SelectList(CostModelServicesHelpers.GetCostModelServiceCategories().Result, "ID", "Value");
                return View(Quotesmodel);
            }
        }
        [HttpPost]
        public ActionResult CreateSalesForceOpportunity(SalesForceOpportunity opportunity)
        {
            string opportunityNumber = "";
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (CostModelServicesHelpers.CreateSalesForceOpportunity(opportunity, out opportunityNumber))
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
                    return Json("Salesforce Opportunity has been successfully created with Opportunity Number :" + opportunityNumber + "-Id:" + IsRequestCompleted.Id, JsonRequestBehavior.AllowGet);
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
        [HttpGet]
        public ActionResult AddSalesForceOpportunity()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return PartialView("CostModelServicePopup");
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
                var result = QuoteRequestHelperService.AddQuoteServiceItems(RequestQuoteModel).Result;
                return new JsonResult { Data = result.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }
    }
}