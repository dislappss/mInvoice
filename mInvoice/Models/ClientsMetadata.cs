using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(ClientsMetadata))]
    public partial class Clients
    {
    }

    public class ClientsMetadata
    {
        //[ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("client")]
        [Display(Name = "client", ResourceType = typeof(Resource))]
        public int clientname { get; set; }

        [Required]
        // [LocalizedDisplayName("email")]
        [Display(Name = "email", ResourceType = typeof(Resource))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }

        // [LocalizedDisplayName("owner")]
        [Display(Name = "owner", ResourceType = typeof(Resource))]
        public string owner { get; set; }

        // [LocalizedDisplayName("country_name")]
        [Display(Name = "country_name", ResourceType = typeof(Resource))]
        public string countriesid { get; set; }

        // [LocalizedDisplayName("street")]
        [Display(Name = "street", ResourceType = typeof(Resource))]
        public string street { get; set; }

        // [LocalizedDisplayName("zip")]
        [Display(Name = "zip", ResourceType = typeof(Resource))]
        public string zip { get; set; }

        // [LocalizedDisplayName("city")]
        [Display(Name = "city", ResourceType = typeof(Resource))]
        public string city { get; set; }

        // [LocalizedDisplayName("phone")]
        [Display(Name = "phone", ResourceType = typeof(Resource))]
        public string phone { get; set; }

        // [LocalizedDisplayName("fax")]
        [Display(Name = "fax", ResourceType = typeof(Resource))]
        public string fax { get; set; }

        // [LocalizedDisplayName("ustd_id")]
        [Display(Name = "ustd_id", ResourceType = typeof(Resource))]
        public string ustd_id { get; set; }

        // [LocalizedDisplayName("finance_office")]
        [Display(Name = "finance_office", ResourceType = typeof(Resource))]
        public string finance_office { get; set; }

        // [LocalizedDisplayName("account_number")]
        [Display(Name = "account_number", ResourceType = typeof(Resource))]
        public string account_number { get; set; }

        // [LocalizedDisplayName("bank_name")]
        [Display(Name = "bank_name", ResourceType = typeof(Resource))]
        public string bank_name { get; set; }

        // [LocalizedDisplayName("iban")]
        [Display(Name = "iban", ResourceType = typeof(Resource))]
        public string iban { get; set; }

        // [LocalizedDisplayName("bic")]
        [Display(Name = "bic", ResourceType = typeof(Resource))]
        public string bic { get; set; }

        // [LocalizedDisplayName("picture")]
        [Display(Name = "picture", ResourceType = typeof(Resource))]
        public byte[] picture { get; set; }

    }

   

    
}