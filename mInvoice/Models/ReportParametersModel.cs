using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    public class ReportParametersModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "date_from", ResourceType = typeof(Resource))]
        public Nullable<DateTime> date_from { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "date_to", ResourceType = typeof(Resource))]
        public Nullable<DateTime> date_to { get; set; }

        [Display(Name = "article", ResourceType = typeof(Resource))]
        public int? article_id { get; set; }

        [Display(Name = "customer", ResourceType = typeof(Resource))]
        public int? customers_id { get; set; }
    }
}