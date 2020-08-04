using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class PostQuoteRequestModel
    {
        public int Id { get; set; }
        public int OpportunityNumber { get; set; }
        public string OpportunityName { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime StartDate { get; set; }
        public string CustomerContactName { get; set; }
        public string SalesforceOpportunityName { get; set; }
        public string SiteAddress { get; set; }
        public string SalesManager { get; set; }
        public string Approver { get; set; }
        public string VerserBranch { get; set; }
        public string ProjectManager { get; set; }
        public decimal TOTAL_Incl_GST { get; set; }
        public decimal GST_10 { get; set; }
        public decimal TOTAL_Excl_GST { get; set; }
        public string OpportunityVersion { get; set; }
        public string RequestStatus { get; set; }
    }
}