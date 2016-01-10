using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
//using Microsoft.ReportViewer.WebForms; 
using mInvoice.Models;
using MvcReportViewer; 

namespace mInvoice.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult articles()
        {
            var model = new mInvoice.Models.ReportsModels.Articles()
            {
                articles = LocalData.GetArticles()
            };       
            return View(model);

            //return MvcReportViewer.ReportRunnerExtensions.Report(
            //    this,
            //    MvcReportViewer.ReportFormat.Pdf,
            //    "~/Reports/articles.rdlc",
            //    ProcessingMode.Local);

            //,
            //    null,
            //    //new { Parameter1 = "Test", Parameter2 = 123 },
            //     ProcessingMode.Local );
            //    //,
            //    //new Dictionary<string, DataTable>
            //    //    {
            //    //        { "articles", LocalData.GetArticles () }
            //    //    });
        }
    }
}