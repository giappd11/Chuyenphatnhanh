using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class UserConfigMstForm : FormBase
    {
        public string USER_ID { get; set; }
        public string BRANCH_ID { get; set; }
        public string ROLE_ID { get; set; }
    }
}