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
            public Reports.reportsDataSet.ArticlesLabelsDataTable labels { get; set; }
        }

        public class CustomersModel
        {
            public Reports.reportsDataSet.CustomersDataTable customers { get; set; }
            public Reports.reportsDataSet.CustomersLabelsDataTable labels { get; set; }
        }

        public class rp_invoice_detailsModel
        {
            public Reports.reportsDataSet.rp_invoice_detailsDataTable rp_invoice_details { get; set; }
            public Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable  labels { get; set; }
        }
    
}