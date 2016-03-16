using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using mInvoice.App_GlobalResources;
using mInvoice.Models;
using MvcReportViewer;
using System.Linq;

namespace mInvoice.Controllers
{
    public class ReportsController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities ();

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

        public ActionResult invoice(int id, bool ShowPrintButton)
        {
            var model = GetData_invoice(id);

            ViewBag.ShowPrintButton = ShowPrintButton;

            return View(model);
        }

        public ActionResult sales(
            DateTime? Date_from
            , DateTime? Date_to
            , int? Article_id
            , int? Customers_id)
        {
            var model = GetData_sales(Date_from, Date_to, Article_id, Customers_id);

            ViewBag.ShowPrintButton = true;

            return View(model);
        }


        public mInvoice.Models.rp_invoice_detailsModel GetData_invoice(int id)
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable _labelsDataTable =
                new Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable();

            _labelsDataTable.Addrp_invoice_detailsLabelsRow(
                                        Resource.invoice_no,
                        Resource.order_date,
                        Resource.delivery_date,
                        Resource.customer_reference,
                        Resource.description,
                        Resource.country_name,
                        Resource.customer_no,
                        Resource.customer,
                        Resource.email,
                        Resource.zip,
                        Resource.city,
                        Resource.street,
                        Resource.tax_rate,
                        Resource.client,
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
                        Resource.quantity_units,
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
                        Resource.delivery_terms,
                        Resource.sales, 
                        Resource.month, 
                        Resource.year, 
                        Resource.quarter, 
                        Resource.country, 
                        Resource.product, 
                        Resource.product_costs, 
                        Resource.margin, 
                        Resource.total_sales, 
                        Resource.total_sales_by_year, 
                        Resource.total_sales_by_quarter, 
                        Resource.percent_of_total_sales, 
                        Resource.total_value,
                        Resource.sales_details,
                        Resource.total_sales_by_month ,
                        Resource.due_date  
                        );

            var model = new mInvoice.Models.rp_invoice_detailsModel()
            {
                data = LocalData.GetInvoice(_client_id, id),
                labels = _labelsDataTable
            };
            return model;
        }

        public mInvoice.Models.rp_salesModel GetData_sales(
            DateTime? Date_from
            , DateTime? Date_to
            , int? Article_id
            , int? Customers_id
            )
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable _labelsDataTable =
                new Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable();

            _labelsDataTable.Addrp_invoice_detailsLabelsRow(
                                        Resource.invoice_no,
                        Resource.order_date,
                        Resource.delivery_date,
                        Resource.customer_reference,
                        Resource.description,
                        Resource.country_name,
                        Resource.customer_no,
                        Resource.customer,
                        Resource.email,
                        Resource.zip,
                        Resource.city,
                        Resource.street,
                        Resource.tax_rate,
                        Resource.client,
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
                        Resource.quantity_units,
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
                        Resource.delivery_terms,
                        Resource.sales,
                        Resource.month,
                        Resource.year,
                        Resource.quarter,
                        Resource.country,
                        Resource.product,
                        Resource.product_costs,
                        Resource.margin,
                        Resource.total_sales,
                        Resource.total_sales_by_year,
                        Resource.total_sales_by_quarter,
                        Resource.percent_of_total_sales,
                        Resource.total_value,
                        Resource.sales_details,
                        Resource.total_sales_by_month,
                        Resource.due_date  
                        );

            var model = new mInvoice.Models.rp_salesModel()
            {
                data = LocalData.GetSales(_client_id, Date_from, Date_to, Article_id, Customers_id),
                labels = _labelsDataTable
            };
            return model;
        }



        public ActionResult DownloadPDF(int id, string FileName)
        {
            var model = GetData_invoice(id);

            Dictionary<string, System.Data.DataTable> _localReportDataSources = 
                new Dictionary<string, System.Data.DataTable>();

            _localReportDataSources.Add("labels", model.labels);
            _localReportDataSources.Add("data", model.data);            

            return  this.Report(
                ReportFormat.Pdf
                , "Reports/invoice.rdlc"
                , localReportDataSources: _localReportDataSources
                , mode: Microsoft.Reporting.WebForms.ProcessingMode.Local
                , filename: FileName
                );              
        }

        public ActionResult ReportParameters()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.customers_id = new SelectList(db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no");
            ViewBag.article_id = new SelectList(db.Articles.Where(x => x.clients_id == _client_id), "Id", "article_no");

            ReportParametersModel _form = new ReportParametersModel();           

            return View(_form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportParameters(ReportParametersModel ReportParameters)
        {
            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int _client_id = Convert.ToInt32(Session["client_id"]);

                return RedirectToAction("sales", "Reports", 
                    new { date_from = ReportParameters.date_from
                        , date_to = ReportParameters.date_to
                        , article_id = ReportParameters.article_id
                        , customers_id = ReportParameters.customers_id 
                    });
            }
            else
            {


                return RedirectToAction("Index");
            }
        }

    }
}