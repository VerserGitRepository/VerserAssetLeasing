using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class EndUsersListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNo { get; set; }
        public string Department { get; set; }
        public string CostCode { get; set; }
        public string EndUserStatus { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CommencementDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> TerminationDate { get; set; }
        public string UserName { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTimeOffset> Created { get; set; }
        public string ModifiedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTimeOffset> Modified { get; set; }
        public int EndUser_Company { get; set; }
        public int? EndUser_Site { get; set; }
    }
}