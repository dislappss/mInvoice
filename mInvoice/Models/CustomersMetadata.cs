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
        [LocalizedDisplayName("customer")]
        public string customer_name { get; set; }

        [Required]
        [LocalizedDisplayName("client")]
        public int clientsysid { get; set; }

        [Required]
        [LocalizedDisplayName("country_name")]
        public int countriesid { get; set; }

        [Required]
        [LocalizedDisplayName("zip")]
        public int zip { get; set; }

        [Required]
        [LocalizedDisplayName("city")]
        public int city { get; set; }

        [Required]
        [LocalizedDisplayName("street")]
        public int street { get; set; }

        [Required]
        [LocalizedDisplayName("email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "err_email")]
        public string email { get; set; }

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