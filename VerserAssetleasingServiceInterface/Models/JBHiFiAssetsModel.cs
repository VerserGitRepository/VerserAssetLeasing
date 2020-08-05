using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class JBHiFiAssetsModel
    {
        public int Id { get; set; }
        public string AssetID { get; set; }
        public string SSN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string Barcode { get; set; }
        public string ClientPO { get; set; }
        public string ClientRef { get; set; }
        public string AssetTag { get; set; }
        public string ClientAssetTag { get; set; }
        public int? ItemTypeId { get; set; }
        public int? LoadId { get; set; }
        public int ProjectId { get; set; }
        public int? WarehouseId { get; set; }
        public int? LocationId { get; set; }
        public int AssetStatusId { get; set; }
        public int? DispatchId { get; set; }
        public string DeviceName { get; set; }
        public string CustomerName { get; set; }
        public string ConnectTechTicket { get; set; }
        public int? SalesForceOpportunity { get; set; }
        public string Status { get; set; }
        public string IMEI { get; set; }
        public string SIM { get; set; }
        public string Condition { get; set; }
        public int? Asset_Contract { get; set; }
        public int? Asset_EndUser { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        public string Connote { get; set; }
        public string JobNo { get; set; }
    }
}