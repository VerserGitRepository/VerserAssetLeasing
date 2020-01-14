using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class CompanyListViewModel
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string suburb { get; set; }
        public string fax { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string web { get; set; }
        public string login { get; set; }
        public byte[] logo { get; set; }
        public string createdBy { get; set; }
        public Nullable<DateTime> serviceStartDate { get; set; }
        public Nullable<DateTime> serviceEndDate { get; set; }
        public Nullable<DateTime> created { get; set; }
    }
}