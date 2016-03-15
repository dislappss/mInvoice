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
        [Display(Name = "date_bis", ResourceType = typeof(Resource))]
        public Nullable<DateTime> date_bis { get; set; }
    }
}