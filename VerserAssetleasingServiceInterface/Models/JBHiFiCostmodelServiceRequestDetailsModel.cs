using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHiFiCostmodelServiceRequestDetailsModel
    {
        public virtual JBHIFiCostModelQuoteRequests CostModelQuoteRequestModel { get; set; }
        public List<JBHIFiCostModelServiceItems> ServiceItemsLists { get; set; }
        public JBHiFiCostmodelServiceRequestDetailsModel()
        {
            CostModelQuoteRequestModel = new JBHIFiCostModelQuoteRequests();
            ServiceItemsLists = new List<JBHIFiCostModelServiceItems>();
        }
    }
}