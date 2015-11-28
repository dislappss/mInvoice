using System.ComponentModel.DataAnnotations;

namespace mInvoice.Models
{
    [MetadataType(typeof(ClientsMetadata))]
    public partial class Clients
    {
    }

    public class ClientsMetadata
    {
        [ScaffoldColumn(false)]
        [LocalizedDisplayName("clientsysid")]
        public int Id { get; set; }

        [Required]
        [LocalizedDisplayName("clientsysid")]
        public string clientname { get; set; }

        [Required]
        [LocalizedDisplayName("email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }
    }

   

    
}