using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class DeviceCategoriesListViewModel
    {
        public int id { get; set; }
        public string deviceCategoryName { get; set; }
        public string createdBy { get; set; }
        public Nullable<DateTime> created { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<DateTime> modified { get; set; }
    }
}