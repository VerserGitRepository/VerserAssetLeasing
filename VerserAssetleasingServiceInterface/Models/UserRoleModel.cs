using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class UserRoleModel
    {
        public List<string> Usermodel { get; set; }
        public SelectList UserIDs { get; set; }
        public List<RolesModel> Rolemodel { get; set; }
        public SelectList Companylist { get; set; }
        public List<string> ResourceIDs { get; set; }
        public List<int?> CompanyIDs { get; set; }
        public List<ProjectLoginsModel> ProjectLoginUserList { get; set; }        
        public bool IsAdmin { get; set; }
        public bool CanRemove { get; set; }
        public bool CanEdit { get; set; }
        public string UserName { get; set; }
        public virtual JBHIFiCostModelQuoteRequests JBHIFiCostModelQuoteRequests { get; set; }
        public List<PostQuoteRequestModel> PostQuoteRequestModelLIST { get; set; }
        public SelectList ProjectManagerList { get; internal set; }
        public SelectList SalesManagerList { get; internal set; }
    }
}