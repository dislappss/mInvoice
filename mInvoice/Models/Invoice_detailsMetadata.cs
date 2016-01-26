using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Invoice_detailsMetadata))]
    public partial class Invoice_details { }

    public partial class Invoice_detailsMetadata
    {
        public int Id { get; set; }

         // [LocalizedDisplayName("invoice_no")]
        [Display(Name = "invoice_no", ResourceType = typeof(Resource))]
         [Editable(false)]
        public int invoice_header_id { get; set; }

        // [LocalizedDisplayName("article")]
        [Display(Name = "article", ResourceType = typeof(Resource))]
        public int article_id { get; set; }

        // [LocalizedDisplayName("tax_rate")]
        [Display(Name = "tax_rate", ResourceType = typeof(Resource))]
        public int tax_rate_id { get; set; }

        [Display(Name = "quantity_units_id", ResourceType = typeof(Resource))]
        public int quantity_units_id { get; set; }

        // [LocalizedDisplayName("description")]
        [Display(Name = "description", ResourceType = typeof(Resource))]
        public string description { get; set; }

        // [LocalizedDisplayName("quantity")]
        [Display(Name = "quantity", ResourceType = typeof(Resource))]
        public decimal quantity { get; set; }

        // [LocalizedDisplayName("quantity_2")]
        [Display(Name = "quantity_2", ResourceType = typeof(Resource))]
        public Nullable<decimal> quantity_2 { get; set; }

        // [LocalizedDisplayName("quantity_3")]
        [Display(Name = "quantity_3", ResourceType = typeof(Resource))]
        public Nullable<decimal> quantity_3 { get; set; }

        // [LocalizedDisplayName("price_netto")]
        [Display(Name = "price_netto", ResourceType = typeof(Resource))]
        public decimal price_netto { get; set; }

        // [LocalizedDisplayName("discount")]
        [Display(Name = "discount", ResourceType = typeof(Resource))]
        public Nullable<decimal> discount { get; set; }
    }
}