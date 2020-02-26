using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class ContractsListViewModel
    {
        public int id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> startDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> endDate { get; set; }
        public string serviceNo { get; set; }
        public Boolean isCancelled { get; set; }
        public string createdBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> created { get; set; }
        public string modifiedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> modified { get; set; }
        public string contract_Company { get; set; }
        public string contract_EndUser { get; set; }
        public string contract_Plan { get; set; }
        public SelectList CompanyList { get; set; }
        public SelectList ServiceList { get; set; }
        public SelectList EnduserList { get; set; }
        public SelectList PlanList { get; set; }
    }
}