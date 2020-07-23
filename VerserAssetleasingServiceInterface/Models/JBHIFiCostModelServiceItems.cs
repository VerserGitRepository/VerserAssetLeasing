using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHIFiCostModelServiceItems
    {
        public int Id { get; set; }
        public int FK_JBHIFIQuoteRequestID { get; set; }
        public int FK_JBHIFiCostModelServiceID { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TOTAL_Excl_GST { get; set; }
        public decimal? TOTAL_Incl_GST { get; set; }
        public decimal? GST_10 { get; set; }
    }
}