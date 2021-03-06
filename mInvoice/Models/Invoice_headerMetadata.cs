﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Invoice_headerMetadata))]
    public partial class Invoice_header
    {
        public bool IsPaid { get; set; }
    }

    public class Invoice_headerMetadata
    {
        //[ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "invoice_no", ResourceType = typeof(Resource))]
        public string invoice_no { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "order_date", ResourceType = typeof(Resource))]
        [Required]
        public DateTime order_date   {   get;  set;   }

        [Display(Name = "delivery_date", ResourceType = typeof(Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime delivery_date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "paid_at", ResourceType = typeof(Resource))]
        public Nullable<DateTime> paid_at { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "due_date", ResourceType = typeof(Resource))]
        public Nullable<DateTime> due_date { get; set; }


        [Display(Name = "customer", ResourceType = typeof(Resource))]
        public int customers_id { get; set; }

        [Display(Name = "customer_reference", ResourceType = typeof(Resource))]
        public string customer_reference { get; set; }

        [Required]
        [Display(Name = "countriesid", ResourceType = typeof(Resource))]
        public int countriesid { get; set; }

        [Required]
        [Display(Name = "zip", ResourceType = typeof(Resource))]
        public string zip { get; set; }

        [Required]
        [Display(Name = "city", ResourceType = typeof(Resource))]
        public string city { get; set; }

        [Required]
        [Display(Name = "street", ResourceType = typeof(Resource))]
        public string street { get; set; }

        [Display(Name = "quantity_2_column_name", ResourceType = typeof(Resource))]
        public string quantity_2_column_name { get; set; }

        [Display(Name = "quantity_3_column_name", ResourceType = typeof(Resource))]
        public string quantity_3_column_name { get; set; }

        [Range(0, 100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "err_range")]
        [Display(Name = "discount", ResourceType = typeof(Resource))]
        public Nullable<decimal> discount { get; set; }

        [Display(Name = "payment_terms", ResourceType = typeof(Resource))]
        public Nullable<decimal> payment_terms_id { get; set; }

        [Display(Name = "delivery_terms", ResourceType = typeof(Resource))]
        public Nullable<decimal> delivery_terms_id { get; set; }
       
        [Display(Name = "currency_id", ResourceType = typeof(Resource))]
         public int currency_id { get; set; }

        [Display(Name = "tax_rate", ResourceType = typeof(Resource))]
        public Nullable<decimal> tax_rate_id { get; set; }
        
    }
}

   

    
