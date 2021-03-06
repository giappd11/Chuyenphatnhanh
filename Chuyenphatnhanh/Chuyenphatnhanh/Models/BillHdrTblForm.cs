﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Content.Texts;
using Chuyenphatnhanh.Util;

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

        public string BRANCH_ID_CURRENT { get; set; }
        [Display(Name = "branch_temp", ResourceType = typeof(RGlobal))]
        public string BRANCH_ID_TEMP { get; set; }
        public string ADDRESS_TEMP { get; set; }
        public string WARD_ID_TEMP { get; set; }
        public string DISTRICT_ID_TEMP { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public string UID_CURRENT { get; set; }


        public List<BillTblForm> Bill { get; set; }

        
        [Display(Name = "AddressFrom", ResourceType = typeof(RGlobal))]
        public string AddressFrom { get; set; }
        [Display(Name = "AddressTo", ResourceType = typeof(RGlobal))]
        public string AddressTo { get; set; }
        [Display(Name = "AddressCurrent", ResourceType = typeof(RGlobal))]
        public string AddressCurrent { get; set; }

        [Display(Name = "Status", ResourceType = typeof(RGlobal))]
        public string statusString { get; set; }

    }
}