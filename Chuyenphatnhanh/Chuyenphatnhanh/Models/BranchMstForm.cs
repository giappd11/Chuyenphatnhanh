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
        [Display(Name = "Name", ResourceType = typeof(RGlobal))]
        public string BRANCH_NAME { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS { get; set; }
        public string WARD_ID { get; set; }
        public string ADDRESS_DETAILS { get; set; }
        public Nullable<decimal> LATITUDE { get; set; }
        public Nullable<decimal> LONGITUDE { get; set; }

        public string DISTRICT_ID { get; set; }
        public string Display_Address { get; set; }
    }
}