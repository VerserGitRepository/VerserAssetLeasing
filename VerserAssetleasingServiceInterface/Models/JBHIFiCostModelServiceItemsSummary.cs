using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHIFiCostModelServiceItemsSummary
    {
        public List<JBHIFiCostModelServiceItems> JBHIFiCMServiceItems { get; set; }
        public int ServiceQuoteRequestId { get; set; }
        public string Summary { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TOTAL_Excl_GST { get; set; }
        public decimal? TOTAL_Incl_GST { get; set; }
        public decimal? GST_10 { get; set; }
        public string OpportunityId { get; set; }
        public string SalesForceUniqueId { get; set; }
    }
}