using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Payment_methodMetadata))]
    public partial class Payment_method
    {
    }

    public class Payment_methodMetadata
    {
        //[ScaffoldColumn(false)]         
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("shortmark")]
        [Display(Name = "shortmark", ResourceType = typeof(Resource))]
        public string code { get; set; }

        [Required]
        // [LocalizedDisplayName("payment_method")]     
        [Display(Name = "payment_method", ResourceType = typeof(Resource))]
        public string name { get; set; }
    }

   

    
}