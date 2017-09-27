using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Chuyenphatnhanh.Models
{
    public class BillTblForm : FormBase
    {
        public string BILL_ID { get; set; }
        public string BILL_HDR_ID { get; set; }
        public string BILL_STATUS { get; set; }
        public string COMMENT { get; set; }
        public Nullable<decimal> WEIGHT { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<System.DateTime> SEND_DATE { get; set; }
        public virtual BILL_HDR_TBL BILL_HDR_TBL { get; set; }
       
    }
}