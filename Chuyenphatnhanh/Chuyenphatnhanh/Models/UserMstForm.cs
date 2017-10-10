using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chuyenphatnhanh.Util;
using Resource = Chuyenphatnhanh.Content.Texts;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;

namespace Chuyenphatnhanh.Models
{
    public class UserMstForm : FormBase
    {
        private  string userName = Resource.RGlobal.UserName +"";

        public string USER_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "user_name", ResourceType = typeof(RGlobal))]
        public string USER_NAME { get; set; }
         
        [Display(Name = "PassWord", ResourceType = typeof(RGlobal))]
        public string PASSWORD { get; set; }
        public Nullable<int> NUMBER_LOGIN_FAIL { get; set; }
        public Nullable<System.DateTime> LAST_CHANGE_PASS { get; set; }
        public string OLD_PASSWORD { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Phone", ResourceType = typeof(RGlobal))]
        public string PHONE { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_ward", ResourceType = typeof(RGlobal))]
        public string WARD_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_District", ResourceType = typeof(RGlobal))]
        public string DISTRICT_ID { get; set; }
        public UserConfigMstForm Config { get; set; }
    }
}