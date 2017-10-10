using System;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;
using PdfRpt.Core.Contracts;
using PdfRpt.DataAnnotations;

namespace Chuyenphatnhanh.Models
{
    public class BillTblForm : FormBase
    {
        public string BILL_ID { get; set; }
        public string BILL_HDR_ID { get; set; }
        public string BILL_STATUS { get; set; }
        
         
        public string COMMENT { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Name", ResourceType = typeof(RGlobal))]
        public string NAME { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Weight", ResourceType = typeof(RGlobal))]
        public decimal WEIGHT { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Price", ResourceType = typeof(RGlobal))]
        public Nullable<decimal> PRICE { get; set; } 
        [Display(Name = "Amount", ResourceType = typeof(RGlobal))]

        public Nullable<decimal> AMOUNT { get; set; }
         
        [Display(Name = "SendDate", ResourceType = typeof(RGlobal))]
        public Nullable<System.DateTime> SEND_DATE { get; set; }

        public int Position { get; set; }


       
    }
}