using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [LocalizedDisplayName("client")]
        public int clientname { get; set; }

        [Required]
        [LocalizedDisplayName("email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }

        [LocalizedDisplayName("owner")]
        public string owner { get; set; }

        [LocalizedDisplayName("country_name")]
        public string countriesid { get; set; }

        [LocalizedDisplayName("street")]
        public string street { get; set; }

        [LocalizedDisplayName("zip")]
        public string zip { get; set; }

        [LocalizedDisplayName("city")]
        public string city { get; set; }

        [LocalizedDisplayName("phone")]
        public string phone { get; set; }

        [LocalizedDisplayName("fax")]
        public string fax { get; set; }

        [LocalizedDisplayName("ustd_id")]
        public string ustd_id { get; set; }

        [LocalizedDisplayName("finance_office")]
        public string finance_office { get; set; }

        [LocalizedDisplayName("account_number")]
        public string account_number { get; set; }

        [LocalizedDisplayName("bank_name")]
        public string bank_name { get; set; }

        [LocalizedDisplayName("iban")]
        public string iban { get; set; }

        [LocalizedDisplayName("bic")]
        public string bic { get; set; }

        [LocalizedDisplayName("picture")]
        public byte[] picture { get; set; }

    }

   

    
}