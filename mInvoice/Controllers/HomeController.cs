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

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("Index", "Home");
        }

        //public ActionResult SetCulture(PageViewModel pageViewModel)
        //{
        //    //put breakpoint on the return view and after you submit a selection you'll see that the pageViewModel argument has the language of what you selected
        //    return View(new PageViewModel
        //    {
        //        Language = "English",
        //        Languages = new SelectList(new List<string>
        //        {
        //            "English",
        //            "German"
        //        })
        //    });
        //}

        //public List<SelectListItem> GetLanguages()
        //{
        //    List<SelectListItem> _list = new List<SelectListItem>();

        //    SelectListItem _english = new SelectListItem();
        //    _english.Text = "English";
        //    _english.Value = "en-us";
        //    SelectListItem _german = new SelectListItem();
        //    _german.Text = "German";
        //    _german.Value = "de";

        //    _list.Add(_english);
        //    _list.Add(_german);

        //    //SelectList _list = new SelectList(new List<SelectListItem>
        //    //    {
        //    //        _english,
        //    //        _german
        //    //    });

        //    return _list;
        //}

       
    }

    //public class PageViewModel
    //{
    //    public string Language { get; set; }
    //    public SelectList Languages { get; set; }

       
    //}

   

}

