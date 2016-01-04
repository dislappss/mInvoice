using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using mInvoice.App_GlobalResources;

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
        [Display(Name = "invoice_no", ResourceType = typeof(Resource))]
        public string invoice_no { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "order_date", ResourceType = typeof(Resource))]
        public DateTime? order_date   {   get;  set;   }

        [Display(Name = "delivery_date", ResourceType = typeof(Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? delivery_date  {  get;  set; }

        [Display(Name = "customer", ResourceType = typeof(Resource))]
        public int customers_id { get; set; }

        [Display(Name = "customer_reference", ResourceType = typeof(Resource))]
        public string customer_reference { get; set; }

        [Required]
        //[LocalizedDisplayName("country_code")]

        public int countriesid { get; set; }

        [Required]
        [Display(Name = "zip", ResourceType = typeof(Resource))]
        public string zip { get; set; }

        [Required]
        [Display(Name = "city", ResourceType = typeof(Resource))]
        public string city { get; set; }

        [Required]
        [Display(Name = "street", ResourceType = typeof(Resource))]
        public string street { get; set; }

        [Display(Name = "quantity_2_column_name", ResourceType = typeof(Resource))]
        public string quantity_2_column_name { get; set; }

        [Display(Name = "quantity_3_column_name", ResourceType = typeof(Resource))]
        public string quantity_3_column_name { get; set; }
        
    }
}

   

    
