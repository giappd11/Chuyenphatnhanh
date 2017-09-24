using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class TariffMstForm : FormBase
    {
        public string TARIFF_ID { get; set; }
        public Nullable<decimal> WEIGHT_FROM { get; set; }
        public Nullable<decimal> WEIGHT_TO { get; set; }
        public Nullable<decimal> DISTANCE_FROM { get; set; }
        public Nullable<decimal> DISTANCE_TO { get; set; }
        public Nullable<decimal> PRICE { get; set; }
    }
}