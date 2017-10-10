using Chuyenphatnhanh.Content.Texts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class RoleMstForm : FormBase
    {
        public string ROLE_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Name", ResourceType = typeof(RGlobal))]
        public string TYPE_ROLE { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Description", ResourceType = typeof(RGlobal))]
        public string DESCRIPTION { get; set; }
    }
}