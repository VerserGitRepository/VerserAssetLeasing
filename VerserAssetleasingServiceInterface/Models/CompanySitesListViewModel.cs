using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class CompanySitesListViewModel
    {
        public int id { get; set; }
        public string siteName { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string suburb { get; set; }       
        public string state { get; set; }
        public string postCode { get; set; }
        public string site_Company { get; set; }
       
    }
}