using Chuyenphatnhanh.Content.Texts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class DistrictMstForm : FormBase
    {
        public string DISTRICT_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_District", ResourceType = typeof(RGlobal))]
        public string DISTRICT_NAME { get; set; }
    }
}