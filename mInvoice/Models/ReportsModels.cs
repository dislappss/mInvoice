namespace mInvoice.Models
{
    
        public class ArticlesModel
        {
            public Reports.reportsDataSet.ArticlesDataTable data { get; set; }
            public Reports.reportsDataSet.ArticlesLabelsDataTable labels { get; set; }
        }

        public class CustomersModel
        {
            public Reports.reportsDataSet.CustomersDataTable data { get; set; }
            public Reports.reportsDataSet.CustomersLabelsDataTable labels { get; set; }
        }

        public class rp_invoice_detailsModel
        {
            public Reports.reportsDataSet.rp_invoice_detailsDataTable data { get; set; }
            public Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable  labels { get; set; }
        }
    
}