using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(CountriesMetadata))]
    public partial class Countries
    {
    }

    public class CountriesMetadata
    {
        //[ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("country_code")]
        [Display(Name = "country_code", ResourceType = typeof(Resource))]
        public int code { get; set; }

        [Required]
        // [LocalizedDisplayName("country_name")]      
        [Display(Name = "country_name", ResourceType = typeof(Resource))]
        public string name { get; set; }

        [Required]
        // [LocalizedDisplayName("active")]
        [Display(Name = "active", ResourceType = typeof(Resource))]
        public string active { get; set; }
    }

   

    
}