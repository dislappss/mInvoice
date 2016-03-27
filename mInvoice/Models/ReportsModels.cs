using System;
namespace mInvoice.Models
{
    
        public class ArticlesModel
        {
            public int client_id = -3;
            public string language = null;

            public ArticlesModel(int Client_id, string Language) //, Reports.reportsDataSet.ArticlesLabelsDataTable Labels)
            {
                this.client_id = Client_id;
                this.language = Language;
            }

        }

        public class CustomersModel
        {
            public int client_id = -3;
            public string language = null;

            public CustomersModel(int Client_id, string Language) 
            {
                this.client_id = Client_id;
                this.language = Language;
            }
        }

        public class InvoiceModel
        {
            public int client_id = -3;
            public string language = null;
            public int Invoice_header_id = -3;
            public string Invoice_no = null;

            public InvoiceModel(int Client_id, string Language, int Invoice_header_id, string Invoice_no)
            {
                this.client_id = Client_id;
                this.language = Language;
                this.Invoice_header_id = Invoice_header_id;
                this.Invoice_no = Invoice_no;
            }
        }



        public class rp_invoice_detailsModel
        {
            public Reports.reportsDataSet.rp_invoice_detailsDataTable  data { get; set; }
            public Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable  labels { get; set; }
        }

        public class rp_salesModel
        {
            public int client_id = -3;
            public DateTime? date_from = null;
            public DateTime? date_to = null;
            public int? article_id = -3;
            public int? customers_id = -3;
            public string language = null;

            public rp_salesModel(int Client_id, DateTime? date_from, DateTime? date_to, int? article_id, int? customers_id, string Language)
            {
                this.client_id = Client_id;
                this.date_from = date_from;
                this.date_to = date_to;
                this.article_id = article_id;
                this.customers_id = customers_id ;
                this.language = Language;
            }
        }
}