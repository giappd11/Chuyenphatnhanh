//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chuyenphatnhanh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BILL_LOG_TBL
    {
        public Nullable<bool> DELETE_FLAG { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public string REG_USER_NAME { get; set; }
        public Nullable<System.DateTime> MOD_DATE { get; set; }
        public string MOD_USER_NAME { get; set; }
        public string BILL_LOG_ID { get; set; }
        public Nullable<System.DateTime> TRANSACTION_DATE { get; set; }
        public string TRANSACTION_ID { get; set; }
        public string CUST_FROM_ID { get; set; }
        public string CUST_TO_ID { get; set; }
        public string STATUS { get; set; }
        public string COUNTRY_FROM { get; set; }
        public string PROVINCE_FROM { get; set; }
        public string DISTRICT_FROM { get; set; }
        public string ADDRESS_FROM { get; set; }
        public string COUNTRY_TO { get; set; }
        public string PROVINCE_TO { get; set; }
        public string DISTRICT_TO { get; set; }
        public string ADDRESS_TO { get; set; }
        public string COUNTRY_CURRENT { get; set; }
        public string PROVINCE_CURRENT { get; set; }
        public string DISTRICT_CURRENT { get; set; }
        public string ADDRESS_CURRENT { get; set; }
    
        public virtual BILL_HDR_TBL BILL_HDR_TBL { get; set; }
        public virtual CUST_MST CUST_MST { get; set; }
        public virtual CUST_MST CUST_MST1 { get; set; }
    }
}
