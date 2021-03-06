﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mInvoice.Helpers;

namespace mInvoice.Controllers
{
    public class HomeController : BaseController
    {
        public bool ShowMenu { get; set; }
        public bool IsAdmin { get; set; }


        public ActionResult Index()
        {
            string _AspNetUsers_id = null;

            // Set "Session(client_id)"
            Session.Add("client_id", null);
            var client_id = Helpers.AccountHelper.getClientIDByUserName(User.Identity.Name, ref _AspNetUsers_id);
            if (client_id == -2) // need to create customer
                RedirectToAction("Create", "Customers");
            Session.Add("client_id", client_id);

            string email = Helpers.AccountHelper.getClientEMailByUserName(User.Identity.Name, ref _AspNetUsers_id);
            if (!string.IsNullOrEmpty(email) && Session["email"] == null)
                Session["email"] = email;

            ShowMenu = User.Identity.IsAuthenticated;

            //  Admin ?
            //ViewBag.IsAdmin = email == "dnepr5555@gmail.com";

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
            //var culture = CultureHelper.GetImplementedCulture(culture_str);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
            {
                cookie.Value = culture_str;   // update cookie value
                Response.Cookies.Set(cookie);
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture_str;
                Response.Cookies.Add(cookie);
            }
            cookie.Expires = DateTime.Now.AddYears(1);



            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("Index", "Home");
        }




    }


   

}

