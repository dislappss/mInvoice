using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mInvoice.Models
{
    [MetadataType(typeof(Invoice_headerMetadata))]
    public partial class Invoice_header
    {
        public int BirthdateDay { get; set; }
        public int BirthdateMonth { get; set; }
        public int BirthdateYear { get; set; }

        public IEnumerable<SelectListItem>order_dateDaySelectList 
        {
            get
            {
                for (int i = 1; i < 32; i++)
                {
                    yield return new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                        Selected = BirthdateDay == i
                    };
                }
            }
        }

        public IEnumerable<SelectListItem> order_dateMonthSelectList
        {
            get
            {
                for (int i = 1; i < 13; i++)
                {
                    yield return new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = new DateTime(2000, i, 1).ToString("MMMM"),
                        Selected = BirthdateMonth == i
                    };
                }
            }
        }

        public IEnumerable<SelectListItem> order_dateYearSelectList
        {
            get
            {
                for (int i = 1910; i < DateTime.Now.Year; i++)
                {
                    yield return new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                        Selected = BirthdateYear == i
                    };
                }
            }
        }

    }

    public class Invoice_headerMetadata
    {
        //[ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [LocalizedDisplayName("invoice_no")]
        public string invoice_no { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [LocalizedDisplayName("order_date")]
        public DateTime? order_date   {   get;  set;   }

        [LocalizedDisplayName("delivery_date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? delivery_date  {  get;  set; }

        [Required]
        [LocalizedDisplayName("customer")]
        public int customers_id { get; set; }

        [LocalizedDisplayName("customer_reference")]
        public string customer_reference { get; set; }

        [Required]
        [LocalizedDisplayName("country_code")]
        public int countriesid { get; set; }

        [Required]
        [LocalizedDisplayName("zip")]
        public string zip { get; set; }

        [Required]
        [LocalizedDisplayName("city")]
        public string city { get; set; }

        [Required]
        [LocalizedDisplayName("street")]
        public string street { get; set; }

        
    }
}

   

    
