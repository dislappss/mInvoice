using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mInvoice.Models
{
    public class ReportsModels
    {
        public class Articles
        {
            public Reports.reportsDataSet.ArticlesDataTable articles { get; set; }
        }
    }
}