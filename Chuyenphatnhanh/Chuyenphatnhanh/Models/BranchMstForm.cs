using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class BranchMstForm : FormBase
    {
        public string BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string WARD_ID { get; set; }
        public string ADDRESS_DETAILS { get; set; }
        public Nullable<decimal> LATITUDE { get; set; }
        public Nullable<decimal> LONGITUDE { get; set; }

        public string Display_Address { get; set; }
    }
}