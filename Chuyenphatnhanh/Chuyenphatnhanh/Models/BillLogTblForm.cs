using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class BillLogTblForm
    {
         
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public string MOD_USER_NAME { get; set; } 
        public Nullable<System.DateTime> TRANSACTION_DATE { get; set; }
        public string TRANSACTION_ID { get; set; }
        public string TRANSACTION_UID { get; set; } 
        public string STATUS { get; set; }         
        public string BRANCH_ID_CURRENT { get; set; }
        public string BRANCH_ID_TEMP { get; set; }
        public string UID_CURRENT { get; set; }

        public string USERNAME_CURRENT { get; set; }
        public string BRANCH_NAME_CURRENT { get; set;}
        public string BRANCH_NAME_TEMP { get; set; }

    }
}