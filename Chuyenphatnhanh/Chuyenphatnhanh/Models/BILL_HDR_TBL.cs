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
    
    public partial class BILL_HDR_TBL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BILL_HDR_TBL()
        {
            this.BILL_TBL = new HashSet<BILL_TBL>();
        }
    
        public Nullable<bool> DELETE_FLAG { get; set; }
        public string REG_USER_NAME { get; set; }
        public string MOD_USER_NAME { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public Nullable<System.DateTime> MOD_DATE { get; set; }
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
    
        public virtual CUST_MST CUST_MST { get; set; }
        public virtual CUST_MST CUST_MST1 { get; set; }
        public virtual WARD_MST WARD_MST { get; set; }
        public virtual WARD_MST WARD_MST1 { get; set; }
        public virtual WARD_MST WARD_MST2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILL_TBL> BILL_TBL { get; set; }
    }
}
