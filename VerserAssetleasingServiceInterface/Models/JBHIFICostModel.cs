using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHIFICostModel
    {
        
            //JBHiFICostModelServices table Name
            public int Id { get; set; }
            public string ServiceDescription { get; set; }
            public decimal Priceperunit { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public string CostCategory { get; set; }
            public decimal CostPerUnit_Asset { get; set; }
            public decimal TravelCostPerUnit { get; set; }
            public decimal Onsite_WarehouseLabourCostPerUnit { get; set; }
            public decimal Freight_Variable_PartnerCostPerUnit { get; set; }
            public decimal PMPerUnit { get; set; }
            public decimal LOADEDTechnicianHourlyRate { get; set; }
            public decimal BaseTechnicianHourlyRate { get; set; }
            public decimal TravelCostHoursPerUnit { get; set; }
            public decimal OnsiteorWarehouseLabourCostHoursperunit { get; set; }
            public decimal Freight_Variable_PartnerCostPerUnit2 { get; set; }
            public decimal PMCostHoursPerUnit { get; set; }
            public decimal TotalCost { get; set; }
            public decimal ProfitPerUnit_Asset { get; set; }
            public decimal TotalProfit { get; set; }
            public string ActualMarginAfterOverhead { get; set; }
            public int FK_OpportunityID { get; set; }
            public int FK_ServiceActivityID { get; set; }

        
    }
}