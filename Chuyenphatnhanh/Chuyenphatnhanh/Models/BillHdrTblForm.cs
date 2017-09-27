using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;

namespace Chuyenphatnhanh.Models
{
    public class BillHdrTblForm : FormBase
    {
        public string STATUS { get; set; }

        public string BILL_HDR_ID { get; set; }
        public string CUST_FROM_ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_Phone", ResourceType = typeof(RGlobal))]
        public string Cust_From_Phone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_Name_from", ResourceType = typeof(RGlobal))]
        public string Cust_From_Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS_FROM { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_District", ResourceType = typeof(RGlobal))]
        public string DISTRICT_ID_FROM { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_ward", ResourceType = typeof(RGlobal))]
        public string WARD_ID_FROM { get; set; }

        public string CUST_TO_ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_Phone", ResourceType = typeof(RGlobal))]
        public string Cust_To_Phone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_Name_To", ResourceType = typeof(RGlobal))]
        public string Cust_To_Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_Address", ResourceType = typeof(RGlobal))]
        public string ADDRESS_TO { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_District", ResourceType = typeof(RGlobal))]
        public string DISTRICT_ID_TO { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField",
        ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Cust_ward", ResourceType = typeof(RGlobal))]
        public string WARD_ID_TO { get; set; }

        public List<BillTblForm> Bill { get; set; }



    }
}