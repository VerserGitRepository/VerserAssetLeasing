using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHIFiCostModelQuoteRequests
    {
        public List<JBHIFiCostModelQuoteRequests> QuoteList { get; set; }
        public List<JBHIFiCostModelServiceItems> QuoteServiceItems { get; set; }

        public List<PostQuoteRequestModel> PostQuoteRequestModelLIST { get; set; }        
        public JBHIFiCostModelQuoteRequests()
        {
            QuoteList = new List<JBHIFiCostModelQuoteRequests>();
            QuoteServiceItems = new List<JBHIFiCostModelServiceItems>();
            PostQuoteRequestModelLIST = new List<PostQuoteRequestModel>();          
        }
        public int Id { get; set; }
        public int opportunityNumber { get; set; }
        public string opportunityName { get; set; }
        public string CustomerName { get; set; }
        public DateTime startDate { get; set; }
        public string customerContactName { get; set; }
        public string salesforceOpportunityName { get; set; }
        public string siteAddress { get; set; }
        public string salesManager { get; set; }
        public string email { get; set; }
        public string approver { get; set; }
        public string verserBranch { get; set; }
        public string projectManager { get; set; }
        public decimal totaL_Incl_GST { get; set; }
        public decimal gsT_10 { get; set; }
        public decimal totaL_Excl_GST { get; set; }
        public SelectList CostModelServices { get; internal set; }
        public int CostModelServiceId { get; set; }
        public SelectList CostModelServicesCategories { get; internal set; }
        public int CostModelServiceCategoryId { get; set; }
        public string Status { get; set; }


    }
}