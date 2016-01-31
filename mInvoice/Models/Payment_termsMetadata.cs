using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Payment_termsMetadata))]
    public partial class Payment_terms
    {
    }

    public class Payment_termsMetadata
    {
        //[ScaffoldColumn(false)]         
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("shortmark")]
        [Display(Name = "shortmark", ResourceType = typeof(Resource))]
        public string code { get; set; }

        [Required]
        // [LocalizedDisplayName("payment_method")]     
        [Display(Name = "payment_term", ResourceType = typeof(Resource))]
        public string description { get; set; }
    }

   

    
}