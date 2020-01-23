using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class UserRoleModelLite
    {
       
        public int CompanyId { get; set; }
        public bool IsAdmin { get; set; }
        public bool CanEdit { get; set; }
        public string UserName { get; set; }
       
    }
}