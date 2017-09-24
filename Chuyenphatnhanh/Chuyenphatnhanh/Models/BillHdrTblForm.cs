using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class BillHdrTblForm : FormBase
    {
        public string BILL_HDR_ID { get; set; }
        public string CUST_FROM_ID { get; set; }
        public string CUST_TO_ID { get; set; }
        public string STATUS { get; set; }
        public string WARD_ID_FROM { get; set; }
        public string ADDRESS_FROM { get; set; }
        public string WARD_ID_TO { get; set; }
        public string ADDRESS_TO { get; set; }
        public string WARD_ID_CURRENT { get; set; }
        public string ADDRESS_CURRENT { get; set; }
        public string BRANCH_ID_CURRENT { get; set; }
        public List<BillTblForm> bill { get; set; }

    }
}