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
    
    public partial class Tax_rates
    {
        public Tax_rates()
        {
            this.Articles = new HashSet<Articles>();
            this.Iinvoice_details = new HashSet<Iinvoice_details>();
        }
    
        public int Id { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public decimal value { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual ICollection<Articles> Articles { get; set; }
        public virtual ICollection<Iinvoice_details> Iinvoice_details { get; set; }
    }
}