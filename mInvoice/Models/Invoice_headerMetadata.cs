using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mInvoice.Models
{
    [MetadataType(typeof(Invoice_headerMetadata))]
    public partial class Invoice_header
    {
    }

    public class Invoice_headerMetadata
    {
        //[ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [LocalizedDisplayName("invoice_no")]
        public string invoice_no { get; set; }

        [Required]
        [LocalizedDisplayName("order_date")]
        public DateTime order_date { get; set; }

        [Required]
        [LocalizedDisplayName("delivery_date")]
        public string delivery_date { get; set; }

        [Required]
        [LocalizedDisplayName("customer")]
        public int customers_id { get; set; }
      
        [LocalizedDisplayName("customer_reference")]
        public string customer_reference { get; set; }

        [Required]
        [LocalizedDisplayName("country_code")]
        public string countriesid { get; set; }

        [Required]
        [LocalizedDisplayName("zip")]
        public string zip { get; set; }

        [Required]
        [LocalizedDisplayName("city")]
        public string city { get; set; }

        [Required]
        [LocalizedDisplayName("street")]
        public string street { get; set; }
    }

   

    
}