using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    [MetadataType(typeof(ArticlesMetadata))]
    public partial class Articles
    {
    }

    public class ArticlesMetadata
    {
        public int Id { get; set; }

        //[LocalizedDisplayName("article_no")]
        [Display(Name = "article_no", ResourceType = typeof(Resource))]
        public string article_no { get; set; }

        //[LocalizedDisplayName("price")]
        [Display(Name = "price", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal price { get; set; }

        //[LocalizedDisplayName("article")]
        [Display(Name = "article", ResourceType = typeof(Resource))]
        public string description { get; set; }

        //[LocalizedDisplayName("tax_rate")]
        [Display(Name = "tax_rate", ResourceType = typeof(Resource))]
        public int tax_rate_id { get; set; }

        [Display(Name = "quantity_units_id", ResourceType = typeof(Resource))]
        public int quantity_units_id { get; set; }
    }

   

    
}