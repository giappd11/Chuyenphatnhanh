using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;

namespace Chuyenphatnhanh.Models
{
    public class WardMstForm : FormBase
    {
        public string WARD_ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_ward", ResourceType = typeof(RGlobal))]
        public string WARD_NAME { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_District", ResourceType = typeof(RGlobal))]
        public string DISTRICT_ID { get; set; }

        public string DISTRICT_NAME { get; set; }
    }
}