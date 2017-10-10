using Chuyenphatnhanh.Content.Texts;
using Chuyenphatnhanh.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class SecRoleMstForm : FormBase
    {
        public string SEC_ROLE_ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Action", ResourceType = typeof(RGlobal))]
        public string VALUE { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Role", ResourceType = typeof(RGlobal))]
        public string ROLE_ID { get; set; }

        public string Role_name { get; set; }
    }
}