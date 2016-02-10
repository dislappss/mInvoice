using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(CurrencyMetadata))]
    public partial class Currency
    {
    }

    public class CurrencyMetadata
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string name { get; set; }

        [Required]
        [Display(Name = "code", ResourceType = typeof(Resource))]
        [MaxLength(3, ErrorMessageResourceName="max_lenght", ErrorMessageResourceType = typeof(Resource))]
        [MinLength(3, ErrorMessageResourceName = "max_lenght", ErrorMessageResourceType = typeof(Resource))]
        public string code { get; set; }

        

    }

   

    
}