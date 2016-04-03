using System;
using System.ComponentModel.DataAnnotations;
using mInvoice.App_GlobalResources;

namespace mInvoice.Models
{
    public class Archive
    {
        [Key]
        [Display(Name = "FileName", ResourceType = typeof(Resource))]
        public string Id { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "FilePath", ResourceType = typeof(Resource))]
        public string FilePath { get; set; }
        
        [Display(Name = "CreateDate", ResourceType = typeof(Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public DateTime CreateDate { get; set; }

        public enum archiveType { Mail, Report, Unknown}

        [Display(Name = "type", ResourceType = typeof(Resource))]
        public archiveType Type { get; set; }

        [Display(Name = "email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        public Archive(string FilePath, string FileName, DateTime CreateDate, archiveType ArchiveType, string Email)
        {
            this.Id = FileName;
            this.FilePath = FilePath;            
            this.CreateDate = CreateDate;
            this.Type = ArchiveType;
            this.Email = Email;

        }

       
        
    }
}