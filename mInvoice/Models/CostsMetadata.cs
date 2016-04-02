using System;
using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(CostsMetadata))]
    public partial class Costs
    {
    }

    public class CostsMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "costs", ResourceType = typeof(Resource))]
        public int Id { get; set; }

        [Required]
        [Display(Name = "description", ResourceType = typeof(Resource))]
        public string description { get; set; }

        [Display(Name = "type_of_costs", ResourceType = typeof(Resource))]
        public int? type_of_costs_id { get; set; }

        [Display(Name = "article", ResourceType = typeof(Resource))]
        public int? articles_id { get; set; }

        [Display(Name = "customer", ResourceType = typeof(Resource))]
        public int? customers_id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "booking_date", ResourceType = typeof(Resource))]
        public DateTime booking_date { get; set; }

        [Required]
        [Display(Name = "value", ResourceType = typeof(Resource))]
        public decimal value { get; set; }
    }
}