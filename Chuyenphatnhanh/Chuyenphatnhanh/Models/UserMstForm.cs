using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class UserMstForm : FormBase
    {
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<int> NUMBER_LOGIN_FAIL { get; set; }
        public Nullable<System.DateTime> LAST_CHANGE_PASS { get; set; }
        public string OLD_PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public UserConfigMstForm Config { get; set; }
    }
}