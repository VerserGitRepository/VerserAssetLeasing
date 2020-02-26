using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class EndUsersListViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName Is Mandatory")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Is Mandatory")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Employee Number Is Mandatory")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "Department Is Mandatory")]
        public string Department { get; set; }
        public string CostCode { get; set; }
        public string EndUserStatus { get; set; }
        [Required(ErrorMessage = "Start Date Is Mandatory")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CommencementDate { get; set; }
        [Required(ErrorMessage = "End Date Is Mandatory")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
        [Required(ErrorMessage = "EndUser_Company Is Mandatory")]
        public int EndUser_Company { get; set; }
        [Required(ErrorMessage = "EndUser_Site Is Mandatory")]
        public int? EndUser_Site { get; set; }
        public SelectList EnduserList { get; set; }
    }
}