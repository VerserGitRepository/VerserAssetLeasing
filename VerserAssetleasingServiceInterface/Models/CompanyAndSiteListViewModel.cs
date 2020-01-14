using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class CompanyAndSiteListViewModel
    {
        public List<CompanyListViewModel> CompanyListViewModel { get; set; }
        public List<CompanySitesListViewModel> CompanySitesListViewModel { get; set; }

    }
}