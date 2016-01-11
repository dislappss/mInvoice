using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using mInvoice.App_GlobalResources;
//using Microsoft.ReportViewer.WebForms; 
using mInvoice.Models;
using MvcReportViewer;

namespace mInvoice.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult articles()
        {
            Reports.reportsDataSet.ArticlesLabelsDataTable _articlesLabelsDataTable =
                new Reports.reportsDataSet.ArticlesLabelsDataTable();

            _articlesLabelsDataTable.AddArticlesLabelsRow(
                    Resource.article_no,
                    Resource.price,
                    Resource.description,
                    Resource.tax_rate,
                    Resource.create_at,
                    Resource.update_at);

            var model = new mInvoice.Models.ArticlesModel()
            {
                articles = LocalData.GetArticles(),
                ArticlesLabels = _articlesLabelsDataTable
            };
            return View(model);
        }
    }
}