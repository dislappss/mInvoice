﻿using System;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using mInvoice.Helpers;
using mInvoice.Properties;

namespace mInvoice.Controllers
{
    /// <summary>
    /// Defines the base controller.
    /// </summary>
    /// <remarks>
    /// This is the base class for all site's controllers.
    /// </remarks>
    ///    
    public class BaseController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public enum MessageType { Success, Error, Info, Warning };

        protected void ShowMessage(string Message, MessageType type)
        {
           
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            string _fileName = Server.MapPath(Settings.Default.LogErrorFile);

            WriteLog(_fileName, filterContext.Exception.ToString());

            if (filterContext.Exception.InnerException != null)
                WriteLog(_fileName,
                    "Innere Exception: " + filterContext.Exception.InnerException.ToString() + " ");

            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
                this.View("Error").ExecuteResult(this.ControllerContext);
            }
        }

        static void WriteLog(string logFile, string text)
        {
             StringBuilder message = new StringBuilder();
            message.AppendLine(DateTime.Now.ToString());
            message.AppendLine(text);
            message.AppendLine("=========================================");

            logger.Info(message.ToString());

            //System.IO.File.AppendAllText(logFile, message.ToString());
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Exception exception = filterContext.Exception;

        //    //Logging the Exception
        //    filterContext.ExceptionHandled = true;

        //    var Result = this.View("Error", new HandleErrorInfo(exception,
        //        filterContext.RouteData.Values["controller"].ToString(),
        //        filterContext.RouteData.Values["action"].ToString()));

        //    filterContext.Result = Result;

        //}
    }
}