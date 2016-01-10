using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mInvoice.Helpers;

namespace mInvoice.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SetCulture(int? id)
        {
            string culture_str = null;

            if (id >= 0)
            {
                if (id == 1)
                    culture_str = "en";
                else if (id == 2)
                    culture_str = "de";
            }

            // Validate input
            var culture = CultureHelper.GetImplementedCulture(culture_str);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture_str;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture_str;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("Index", "Home");
        }

      

       
    }


   

}

