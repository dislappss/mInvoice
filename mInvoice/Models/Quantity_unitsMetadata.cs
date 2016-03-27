using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(Quantity_unitsMetadata))]
    public partial class Quantity_units
    {
    }

    public class Quantity_unitsMetadata
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "description", ResourceType = typeof(Resource))]
        public string description { get; set; }

        [Required]
        [Display(Name = "description_en", ResourceType = typeof(Resource))]
        public string description_en { get; set; }

        [Required]
        [Display(Name = "code", ResourceType = typeof(Resource))]
        public string code { get; set; }       

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