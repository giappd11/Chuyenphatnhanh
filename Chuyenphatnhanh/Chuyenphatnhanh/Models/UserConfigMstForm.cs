using Chuyenphatnhanh.Content.Texts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class UserConfigMstForm : FormBase
    {
        public string USER_ID { get; set; }
        public string User_name { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Branch", ResourceType = typeof(RGlobal))]
        public string BRANCH_ID { get; set; }
        public string Branch_Name { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Role", ResourceType = typeof(RGlobal))]
        public string ROLE_ID { get; set; }
        public string Role_Name { get; set; }
    }
}