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
    
    public partial class TARIFF_MST
    {
        public Nullable<bool> DELETE_FLAG { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public Nullable<System.DateTime> MOD_DATE { get; set; }
        public string REG_USER_NAME { get; set; }
        public string MOD_USER_NAME { get; set; }
        public string TARIFF_ID { get; set; }
        public Nullable<decimal> WEIGHT_FROM { get; set; }
        public Nullable<decimal> WEIGHT_TO { get; set; }
        public Nullable<decimal> DISTANCE_FROM { get; set; }
        public Nullable<decimal> DISTANCE_TO { get; set; }
        public decimal PRICE { get; set; }
        public string LOCATION_SPICAL { get; set; }
        public string COMMENT { get; set; }
    }
}
