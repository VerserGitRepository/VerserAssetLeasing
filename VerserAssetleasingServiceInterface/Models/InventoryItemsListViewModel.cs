using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class InventoryItemsListViewModel
    {
        public int id { get; set; }
        public byte[] image { get; set; }
        public string inventoryItemID { get; set; }
        public string inventoryItemName { get; set; }
        public string model { get; set; }
        public string warrantyPeriod { get; set; }
        public string maintenancePeriod { get; set; }
        public string supplierLeadTime { get; set; }
        public string inventoryItem_Colour { get; set; }
        public string inventoryItem_DeviceCategory { get; set; }
        public string inventoryItem_FormFactor { get; set; }
        public string inventoryItem_OEM { get; set; }
        public string inventoryItem_OperatingSystem { get; set; }
        public string createdBy { get; set; }
        public Nullable<DateTime> created { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<DateTime> modified { get; set; }
    }
}