using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mInvoice.Models
{
    public class Nullable<DateTime> : System.DateTime
    {
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }

        //public IEnumerable<SelectListItem> Day
        //{
        //    get
        //    {
        //        for (int i = 1; i < 32; i++)
        //        {
        //            yield return new SelectListItem
        //            {
        //                Value = i.ToString(),
        //                Text = i.ToString()
        //            };
        //        }
        //    }
        //}

        //public IEnumerable<SelectListItem> Month
        //{
        //    get
        //    {
        //        for (int i = 1; i < 13; i++)
        //        {
        //            yield return new SelectListItem
        //            {
        //                Value = i.ToString(),
        //                Text = new DateTime(2000, i, 1).ToString("MMMM"),
        //                Selected = Month == i
        //            };
        //        }
        //    }
        //}

        //public IEnumerable<SelectListItem> Year
        //{
        //    get
        //    {
        //        for (int i = 1910; i < DateTime.Now.Year; i++)
        //        {
        //            yield return new SelectListItem
        //            {
        //                Value = i.ToString(),
        //                Text = i.ToString(),
        //                Selected = Year == i
        //            };
        //        }
        //    }
        //}

    }
}