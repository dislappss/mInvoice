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
        [Display(Name = "email", ResourceType = typeof(Resource))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }

       
        [Display(Name = "owner", ResourceType = typeof(Resource))]
        public string owner { get; set; }

        [Display(Name = "country_name", ResourceType = typeof(Resource))]
        public string countriesid { get; set; }

        [Display(Name = "street", ResourceType = typeof(Resource))]
        public string street { get; set; }

        [Display(Name = "zip", ResourceType = typeof(Resource))]
        public string zip { get; set; }

        [Display(Name = "city", ResourceType = typeof(Resource))]
        public string city { get; set; }
     
        [Display(Name = "phone", ResourceType = typeof(Resource))]
        public string phone { get; set; }

        [Display(Name = "fax", ResourceType = typeof(Resource))]
        public string fax { get; set; }

        [Display(Name = "ustd_id", ResourceType = typeof(Resource))]
        public string ustd_id { get; set; }

        [Display(Name = "finance_office", ResourceType = typeof(Resource))]
        public string finance_office { get; set; }

        [Display(Name = "account_number", ResourceType = typeof(Resource))]
        public string account_number { get; set; }

        [Display(Name = "bank_name", ResourceType = typeof(Resource))]
        public string bank_name { get; set; }

        [Display(Name = "iban", ResourceType = typeof(Resource))]
        public string iban { get; set; }

        [Display(Name = "bic", ResourceType = typeof(Resource))]
        public string bic { get; set; }

        [Display(Name = "picture", ResourceType = typeof(Resource))]
        public byte[] picture { get; set; }

        [Display(Name = "prefix_invoices", ResourceType = typeof(Resource))]
        public string prefix_invoices { get; set; }

        [Required]
        [Display(Name = "last_index_invoices", ResourceType = typeof(Resource))]
        public int last_index_invoices { get; set; }

        [Display(Name = "no_template_invoices", ResourceType = typeof(Resource))]
        public string no_template_invoices { get; set; }

        [Display(Name = "text_to_table", ResourceType = typeof(Resource))]
        public string text_to_table { get; set; }

        [Display(Name = "text_under_the_table_bold", ResourceType = typeof(Resource))]
        public string text_under_the_table_bold { get; set; }

        [Display(Name = "text_under_the_table", ResourceType = typeof(Resource))]
        public string text_under_the_table { get; set; }
    }

   

    
}