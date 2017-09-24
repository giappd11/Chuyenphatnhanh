using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Chuyenphatnhanh.Models
{
    public class FormBase
    {
        public string MessageError { get; set; }
        public string MessageSuccess { get; set; }
        public string Mode { get; set; }

        public Nullable<bool> DELETE_FLAG { get; set; }
        public System.DateTime REG_DATE { get; set; }
        public System.DateTime MOD_DATE { get; set; }
        public string REG_USER_NAME { get; set; }
        public string MOD_USER_NAME { get; set; }
    }
}