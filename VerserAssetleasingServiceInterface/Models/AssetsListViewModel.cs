using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class AssetsListViewModel
    {
        public int id { get; set; }
        public string AssetID { get; set; }
        public string SerialNo { get; set; }
        public string Asset_Company { get; set; }
        public string IMEI { get; set; }
        public string SIMNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchasePrice { get; set; }
        public string DecomissionDate { get; set; }
        public string Condition { get; set; }
        public string BuyBackPrice { get; set; }
        public string AquisitionDate { get; set; }
        public string SPMD { get; set; }
        public string FirstCommissionDate { get; set; }
        public string LastCommissionDate { get; set; }
        public string LastRepairDate { get; set; }
        public string CreatedBy { get; set; }
        public string Created { get; set; }
        public string ModifiedBy { get; set; }
        public string Modified { get; set; }
        public string Asset_Contract { get; set; }
        public string Asset_EndUser { get; set; }
        public string Asset_LifecycleStatus { get; set; }
        public string Asset_InventoryItem { get; set; }
        public string Asset_OSVersion { get; set; }
    }
}