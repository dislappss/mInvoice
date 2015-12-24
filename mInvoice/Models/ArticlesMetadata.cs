using System.ComponentModel.DataAnnotations;

namespace mInvoice.Models
{
    [MetadataType(typeof(ArticlesMetadata))]
    public partial class Articles
    {
    }

    public class ArticlesMetadata
    {
        public int Id { get; set; }

        [LocalizedDisplayName("article_no")]
        public string article_no { get; set; }

        [LocalizedDisplayName("price")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal price { get; set; }

        [LocalizedDisplayName("article")]
        public string description { get; set; }

        [LocalizedDisplayName("tax_rate")]
        public int tax_rate_id { get; set; }
    }

   

    
}