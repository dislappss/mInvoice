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
    
    public partial class Countries
    {
        public Countries()
        {
            this.Customers = new HashSet<Customers>();
            this.Clients = new HashSet<Clients>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public bool active { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
