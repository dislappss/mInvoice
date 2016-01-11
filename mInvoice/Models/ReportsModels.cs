using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mInvoice.Models
{
    
        public class ArticlesModel
        {
            public Reports.reportsDataSet.ArticlesDataTable articles { get; set; }
            public Reports.reportsDataSet.ArticlesLabelsDataTable ArticlesLabels { get; set; }
        }
    
}