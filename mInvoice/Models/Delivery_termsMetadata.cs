using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Delivery_termsMetadata))]
    public partial class Delivery_terms
    {
    }

    public class Delivery_termsMetadata
    {
        //[ScaffoldColumn(false)]         
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("shortmark")]
        [Display(Name = "shortmark", ResourceType = typeof(Resource))]
        public string code { get; set; }

        [Required]
        // [LocalizedDisplayName("payment_method")]     
        [Display(Name = "delivery_term", ResourceType = typeof(Resource))]
        public string description { get; set; }
    }

   

    
}