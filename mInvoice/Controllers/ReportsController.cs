using System;
using System.Web.Mvc;
using mInvoice.App_GlobalResources;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult articles()
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);
                
            Reports.reportsDataSet.ArticlesLabelsDataTable _labelsDataTable =
                new Reports.reportsDataSet.ArticlesLabelsDataTable();

            _labelsDataTable.AddArticlesLabelsRow(
                    Resource.article_no,
                    Resource.price,
                    Resource.description,
                    Resource.tax_rate,
                    Resource.create_at,
                    Resource.update_at,
                    Resource.article  );

            var model = new mInvoice.Models.ArticlesModel()
            {
                data = LocalData.GetArticles(_client_id),
                labels = _labelsDataTable
            };
            return View(model);
        }

        public ActionResult customers()
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            Reports.reportsDataSet.CustomersLabelsDataTable _labelsDataTable =
                new Reports.reportsDataSet.CustomersLabelsDataTable ();

            _labelsDataTable.AddCustomersLabelsRow(
                        Resource.payment_method,
                        Resource.customer_no,
                        Resource.description,
                        Resource.countries,
                        Resource.email,
                        Resource.zip,
                        Resource.city,
                        Resource.street,
                        Resource.countries,
                        Resource.email,
                        Resource.zip,
                        Resource.city,
                        Resource.street,
                        Resource.phone,
                        Resource.fax,
                        Resource.phone_2,
                        "WWW",
                        Resource.ustd_id,
                        Resource.finance_office,
                        Resource.account_number,
                        Resource.bank_name,
                        Resource.iban,
                        Resource.bic,
                        Resource.create_at,
                        Resource.update_at,
                        Resource.customers);

            var model = new mInvoice.Models.CustomersModel ()
            {
                data  = LocalData.GetCustomers(_client_id),
                labels = _labelsDataTable
            };
            return View(model);
        }

        public ActionResult rp_invoice_details(int id)
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable  _labelsDataTable =
                new Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable ();

            _labelsDataTable.Addrp_invoice_detailsLabelsRow (
                                        Resource.invoice_no, 
                        Resource.order_date, 
                        Resource.delivery_date, 
                        Resource.customer_reference, 
                        Resource.description, 
                        Resource.country_name , 
                        Resource.customer_no, 
                        Resource.customer , 
                        Resource.email, 
                        Resource.zip, 
                        Resource.city, 
                        Resource.street, 
                        Resource.tax_rate, 
                        Resource.client , 
                        Resource.email, 
                        Resource.owner, 
                        Resource.street, 
                        Resource.zip, 
                        Resource.city, 
                        Resource.phone, 
                        Resource.fax, 
                        "WWW", 
                        Resource.ustd_id, 
                        Resource.finance_office, 
                        Resource.account_number, 
                        Resource.bank_name, 
                        Resource.iban, 
                        Resource.bic, 
                        Resource.picture, 
                        Resource.phone_2, 
                        Resource.description, 
                        Resource.quantity, 
                        Resource.quantity_2, 
                        Resource.quantity_3, 
                        Resource.price_netto, 
                        Resource.discount, 
                        Resource.article_no, 
                        Resource.invoice, 
                        Resource.date, 
                        Resource.positions_short, 
                        Resource.price, 
                        Resource.sum_price, 
                        Resource.quantity_units , 
                        Resource.description, 
                        Resource.net_amount, 
                        Resource.subtotal, 
                        Resource.plus_vat, 
                        Resource.total_amount, 
                        Resource.positions_short, 
                        Resource.unit, 
                        Resource.quantity_report, 
                        Resource.discount_report, 
                        Resource.phone_report, 
                        Resource.fax_report, 
                        Resource.tax_number, 
                        Resource.bic_report, 
                        Resource.iban_report, 
                        Resource.owner_report, 
                        Resource.delivery_date_report,
                        Resource.payment_terms,
                        Resource.delivery_terms  
                        );

            var model = new mInvoice.Models.rp_invoice_detailsModel()
            {
                data = LocalData.GetInvoice(_client_id, id),
                labels = _labelsDataTable
            };
            return View(model);
        }



    }
}