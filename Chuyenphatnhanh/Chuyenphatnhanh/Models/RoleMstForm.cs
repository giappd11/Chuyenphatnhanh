using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class RoleMstForm : FormBase
    {
        public string ROLE_ID { get; set; }
        public string TYPE_ROLE { get; set; }
        public string DESCRIPTION { get; set; }
    }
}