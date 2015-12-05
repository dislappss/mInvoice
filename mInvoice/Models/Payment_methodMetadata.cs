using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [LocalizedDisplayName("payment_method_code")]
        public int code { get; set; }

        [Required]
        [LocalizedDisplayName("payment_method_name")]       
        public string name { get; set; }
    }

   

    
}