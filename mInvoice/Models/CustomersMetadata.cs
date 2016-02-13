using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(CustomerseMetadata))]
    public partial class Customers  { }

    public class CustomerseMetadata
    {
        [Editable(false)]
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("customer")]
        [Display(Name = "customer", ResourceType = typeof(Resource))]
        public string customer_name { get; set; }

        [Required]
        // [LocalizedDisplayName("client")]
        [Display(Name = "client", ResourceType = typeof(Resource))]
        public int clientsysid { get; set; }

        [Required]
        // [LocalizedDisplayName("country_name")]
        [Display(Name = "country_name", ResourceType = typeof(Resource))]
        public int countriesid { get; set; }

        [Required]
        // [LocalizedDisplayName("zip")]
        [Display(Name = "zip", ResourceType = typeof(Resource))]
        public int zip { get; set; }

        [Required]
        // [LocalizedDisplayName("city")]
        [Display(Name = "city", ResourceType = typeof(Resource))]
        public int city { get; set; }

        [Required]
        // [LocalizedDisplayName("street")]
        [Display(Name = "street", ResourceType = typeof(Resource))]
        public int street { get; set; }

        [Required]
        // [LocalizedDisplayName("email")]
        [Display(Name = "email", ResourceType = typeof(Resource))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }

        // [LocalizedDisplayName("payment_methodid")]
         [Display(Name = "payment_methodid", ResourceType = typeof(Resource))]
        public int payment_methodid { get; set; }

        // [LocalizedDisplayName("customer_no")]
         [Display(Name = "customer_no", ResourceType = typeof(Resource))]
        public int customer_no { get; set; }

        // [LocalizedDisplayName("phone")]
         [Display(Name = "phone", ResourceType = typeof(Resource))]
        public int phone { get; set; }

        // [LocalizedDisplayName("phone_2")]
         [Display(Name = "phone_2", ResourceType = typeof(Resource))]
        public int phone_2 { get; set; }

        // [LocalizedDisplayName("ustd_id")]
         [Display(Name = "ustd_id", ResourceType = typeof(Resource))]
        public int ustd_id { get; set; }

        // [LocalizedDisplayName("finance_office")]
         [Display(Name = "finance_office", ResourceType = typeof(Resource))]
        public int finance_office { get; set; }

        // [LocalizedDisplayName("bank_name")]
         [Display(Name = "bank_name", ResourceType = typeof(Resource))]
        public int bank_name { get; set; }

        // [LocalizedDisplayName("account_number")]
         [Display(Name = "account_number", ResourceType = typeof(Resource))]
        public int account_number { get; set; }

         [Display(Name = "currency_id", ResourceType = typeof(Resource))]
         public int currency_id { get; set; }

         [Display(Name = "payment_terms", ResourceType = typeof(Resource))]
         public Nullable<decimal> payment_terms_id { get; set; }

         [Display(Name = "delivery_terms", ResourceType = typeof(Resource))]
         public Nullable<decimal> delivery_terms_id { get; set; }

         [Display(Name = "tax_rate", ResourceType = typeof(Resource))]
         public Nullable<decimal> tax_rate_id { get; set; }

        [ScaffoldColumn(false)]
        [Editable(false)]
        //[UIHint("Hidden")]
        private DateTime? _CreatedAt;
        public DateTime? CreatedAt
        {
            get
            {
                if (_CreatedAt == null)
                    _CreatedAt = DateTime.Now;
                return _CreatedAt;
            }
            set
            {
                _CreatedAt = value;
            }
        }   

        [ScaffoldColumn(false)]
        [Editable(false)]
        //[UIHint("Hidden")]
        private DateTime? _UpdatedAt;
        public DateTime? UpdatedAt
        {
            get
            {
                if (_UpdatedAt == null)
                    _UpdatedAt = DateTime.Now;
                return _UpdatedAt;
            }
            set
            {
                _UpdatedAt = value;
            }
        }
    }




}