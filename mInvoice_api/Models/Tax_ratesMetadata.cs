using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Tax_ratesMetadata))]
    public partial class Tax_rates
    {
    }

    public class Tax_ratesMetadata
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        // [LocalizedDisplayName("tax_rate")]
        [Display(Name = "tax_rate", ResourceType = typeof(Resource))]
        public string description { get; set; }

        [Required]
        // [LocalizedDisplayName("code")]   
        [Display(Name = "code", ResourceType = typeof(Resource))]
        public string code { get; set; }

        [Required]
        // [LocalizedDisplayName("value")]
        [Display(Name = "value", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal value { get; set; }

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