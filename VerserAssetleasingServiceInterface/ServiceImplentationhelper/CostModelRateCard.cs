using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class CostModelRateCard
    {
        public int ID { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}