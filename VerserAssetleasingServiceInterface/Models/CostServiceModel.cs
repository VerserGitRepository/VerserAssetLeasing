using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class CostServiceModel
    {
                 
        public SelectList CostModelServices { get; internal set; }
        public int CostModelServiceId { get; set; }
        public SelectList CostModelServicesCategories { get; internal set; }
        public int CostModelServiceCategoryId { get; set; }
    }
}