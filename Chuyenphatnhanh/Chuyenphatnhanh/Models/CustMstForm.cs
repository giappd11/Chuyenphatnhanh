using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;

namespace Chuyenphatnhanh.Models
{
    public class CustMstForm : FormBase
    {
        public string CUST_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "CustName", ResourceType = typeof(RGlobal))]
        public string CUST_NAME { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Phone", ResourceType = typeof(RGlobal))]
        public string PHONE { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS { get; set; }        
        
    }
}