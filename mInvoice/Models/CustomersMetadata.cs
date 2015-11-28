using System.ComponentModel.DataAnnotations;

namespace mInvoice.Models
{
    [MetadataType(typeof(CustomerseMetadata))]
    public partial class Customers
    {
    }

    public class CustomerseMetadata
    {
    
        [Editable(false)]
        public int Id { get; set; }

        [Required]
        [LocalizedDisplayName("customer")]
        public string customer_name { get; set; }

        [Required]
        [LocalizedDisplayName("clientsysid")]
        public int clientsysid { get; set; }

        [Required]
        [LocalizedDisplayName("email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }

        //[ScaffoldColumn(false)]
        //[Editable(false)]
        //[System.Web.Mvc.HiddenInput(DisplayValue = false)]
        //public byte[] Version { get; set; }

        //[ScaffoldColumn(false)]
        //[Editable(false)]
        //[System.Web.Mvc.HiddenInput(DisplayValue = false)]
        //public System.DateTimeOffset CreatedAt { get; set; }

        //[ScaffoldColumn(false)]
        //[Editable(false)]
        //[System.Web.Mvc.HiddenInput(DisplayValue = false)]
        //public Nullable<System.DateTimeOffset> UpdatedAt { get; set; }

        //[ScaffoldColumn(false)]
        //[Editable(false)]
        //[System.Web.Mvc.HiddenInput(DisplayValue = false)]
        //public bool Deleted { get; set; }
    }

   

    
}