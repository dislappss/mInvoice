using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [LocalizedDisplayName("country_code")]
        public int code { get; set; }

        [Required]
        [LocalizedDisplayName("country_name")]       
        public string name { get; set; }

        [Required]
        [LocalizedDisplayName("active")]
        public string active { get; set; }
    }

   

    
}