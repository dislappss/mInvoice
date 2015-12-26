using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mInvoice.Models
{
    [MetadataType(typeof(Invoice_detailsMetadata))]
    public partial class Invoice_details { }

    public partial class Invoice_detailsMetadata
    {
        public int Id { get; set; }

         [LocalizedDisplayName("invoice_no")]
         [Editable(false)]
        public int invoice_header_id { get; set; }

        [LocalizedDisplayName("article")]
        public int article_id { get; set; }

        [LocalizedDisplayName("tax_rate")]
        public int tax_rate_id { get; set; }

        [LocalizedDisplayName("description")]
        public string description { get; set; }

        [LocalizedDisplayName("quantity")]
        public decimal quantity { get; set; }

        [LocalizedDisplayName("quantity_2")]
        public Nullable<decimal> quantity_2 { get; set; }

        [LocalizedDisplayName("quantity_3")]
        public Nullable<decimal> quantity_3 { get; set; }

        [LocalizedDisplayName("price_netto")]
        public decimal price_netto { get; set; }

        [LocalizedDisplayName("discount")]
        public Nullable<decimal> discount { get; set; }
    }
}