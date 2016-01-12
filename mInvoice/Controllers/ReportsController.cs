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
                articles = LocalData.GetArticles(),
                labels = _labelsDataTable
            };
            return View(model);
        }

        public ActionResult customers()
        {
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
                customers = LocalData.GetCustomers(),
                labels = _labelsDataTable
            };
            return View(model);
        }
    }
}