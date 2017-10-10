using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;
namespace Chuyenphatnhanh.Models
{
    public class BranchMstForm : FormBase
    {
        public string BRANCH_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Branch_name", ResourceType = typeof(RGlobal))]
        public string BRANCH_NAME { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Branch_name", ResourceType = typeof(RGlobal))]
        public string WARD_ID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS_DETAILS { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "LATITUDE", ResourceType = typeof(RGlobal))]
        public Nullable<decimal> LATITUDE { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "LONGITUDE", ResourceType = typeof(RGlobal))]
        public Nullable<decimal> LONGITUDE { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_District", ResourceType = typeof(RGlobal))]
        public string DISTRICT_ID { get; set; }
        public string Display_Address { get; set; }
    }
}