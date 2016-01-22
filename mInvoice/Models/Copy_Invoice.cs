using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    public class Copy_Invoice
    {
        public int invoice_header_id { get; set; }

        [Display(Name = "quantity_2_column_name", ResourceType = typeof(Resource))]
        public string quantity_2_column_name { get; set; }

        [Display(Name = "quantity_3_column_name", ResourceType = typeof(Resource))]
        public string quantity_3_column_name { get; set; }

        [Display(Name = "quantity_2", ResourceType = typeof(Resource))]
        public decimal quantity_2 { get; set; }

        [Display(Name = "quantity_2", ResourceType = typeof(Resource))]
        public decimal quantity_3 { get; set; }
    }
}