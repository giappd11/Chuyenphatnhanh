using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class WardMstForm : FormBase
    {
        public string WARD_ID { get; set; }
        public string WARD_NAME { get; set; }
        public string DISTRICT_ID { get; set; }
        public string DISTRICT_NANE { get; set; }
    }
}