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
    
    public partial class Quantity_units
    {
        public Quantity_units()
        {
            this.Invoice_details = new HashSet<Invoice_details>();
            this.Articles = new HashSet<Articles>();
        }
    
        public int Id { get; set; }
        public int clients_id { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual ICollection<Invoice_details> Invoice_details { get; set; }
        public virtual ICollection<Articles> Articles { get; set; }
        public virtual Clients Clients { get; set; }
    }
}
