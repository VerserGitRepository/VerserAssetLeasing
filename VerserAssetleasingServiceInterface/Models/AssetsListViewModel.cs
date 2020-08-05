using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class AssetsListViewModel
    {
        public int Id { get; set; }
        public string AssetID { get; set; }
        public string SerialNo { get; set; }
        public int Asset_Company { get; set; }
        public string IMEI { get; set; }
        public string SIMNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DecomissionDate { get; set; }
        public string Condition { get; set; }
        public decimal? BuyBackPrice { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> AquisitionDate { get; set; }
        public string SPMD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> FirstCommissionDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LastCommissionDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LastRepairDate { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTimeOffset> Created { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTimeOffset> Modified { get; set; }
        public int? Asset_Contract { get; set; }
        public int? Asset_EndUser { get; set; }
        public int Asset_LifecycleStatus { get; set; }
        [Required(ErrorMessage = "Inventory Item is Required")]
        public int Asset_InventoryItem { get; set; }
        public int? Asset_OSVersion { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SSN { get; set; }
        public string DeviceName { get; set; }
        public string CustomerName { get; set; }
        public string ConnectTechTicket { get; set; }
        public string SalesForce { get; set; }
        public string Status { get; set; }
    }
}