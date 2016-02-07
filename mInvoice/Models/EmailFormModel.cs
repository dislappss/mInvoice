using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    public class EmailFormModel
    {
        public int ID { get; set; }

        [Required, EmailAddress]
        [Display(Name = "email_from", ResourceType = typeof(Resource))]
        public string From { get; set; }

        [Required, EmailAddress]
        [Display(Name = "email_to", ResourceType = typeof(Resource))]
        public string To { get; set; }

        [Required]
        [Display(Name = "email_subject", ResourceType = typeof(Resource))]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "email_body", ResourceType = typeof(Resource))]
        public string Message { get; set; }

        [Required]
        [Display(Name = "email_attachment", ResourceType = typeof(Resource))]
        public string Attachment { get; set; }
    }
}