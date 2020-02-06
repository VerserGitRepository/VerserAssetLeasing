using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class UserRoleModelLite
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int CompanyID { get; set; }
        public bool CanWrite { get; set; }
        public bool ISAdmin { get; set; }
    }
}