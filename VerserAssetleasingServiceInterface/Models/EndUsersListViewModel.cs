using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class EndUsersListViewModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string employeeNo { get; set; }
        public string department { get; set; }
        public string costCode { get; set; }
        public string endUserStatus { get; set; }
        public Nullable<DateTime> commencementDate { get; set; }
        public Nullable<DateTime> terminationDate { get; set; }
        public string userName { get; set; }
        public string createdBy { get; set; }
        public Nullable<DateTime> created { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<DateTime> modified { get; set; }
        public string endUser_Company { get; set; }
        public string endUser_Site { get; set; }
    }
}