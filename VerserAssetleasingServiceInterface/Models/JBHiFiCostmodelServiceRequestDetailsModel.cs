using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHiFiCostmodelServiceRequestDetailsModel
    {
        public JBHiFiCostmodelServiceRequestDetailsModel()
        {
            CostModelQuoteRequestModel = new JBHIFiCostModelQuoteRequests();
            ServiceItemsLists = new List<JBHIFiCostModelServiceItems>();
        }
        public  JBHIFiCostModelQuoteRequests CostModelQuoteRequestModel { get; set; }
        public List<JBHIFiCostModelServiceItems> ServiceItemsLists { get; set; }
        
    }
}