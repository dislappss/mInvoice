//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mInvoice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Articles
    {
        public Articles()
        {
            this.Invoice_details = new HashSet<Invoice_details>();
        }
    
        public int Id { get; set; }
        public string article_no { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int tax_rate_id { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual Tax_rates Tax_rates { get; set; }
        public virtual ICollection<Invoice_details> Invoice_details { get; set; }
    }
}
