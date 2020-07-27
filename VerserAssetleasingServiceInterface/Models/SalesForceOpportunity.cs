using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class SalesForceOpportunity
    {
        public string OpportunityNumber { get; set; }
        [Required(ErrorMessage = "Opportunity Name Is Mandatory")]
        public string OpportunityName { get; set; }
        [Required(ErrorMessage = "Email Is Mandatory")]
        public string Email { get; set; }
        [Required(ErrorMessage = "StartDate Is Mandatory")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "SiteAddress Is Mandatory")]
        public string SiteAddress { get; set; }
        [Required(ErrorMessage = "Customer Is Mandatory")]
        public string Customer { get; set; }
        public string Approver { get; set; }
        public string ProjectManager { get; set; }
        public string SalesManager { get; set; }
        [Required(ErrorMessage = "Customer Name Is Mandatory")]
        public string CustomerContactName { get; set; }
        public string VerserBranch { get; set; }
        public string Status { get; set; }
        public JBHIFiCostModelQuoteRequests JBHIFiCostModelQuoteRequest { get; set; }
    }
}