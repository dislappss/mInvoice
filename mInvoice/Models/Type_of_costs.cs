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
    
    public partial class Type_of_costs
    {
        public Type_of_costs()
        {
            this.Costs = new HashSet<Costs>();
        }
    
        public int Id { get; set; }
        public int clients_id { get; set; }
        public string description { get; set; }
        public string shortmark { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual ICollection<Costs> Costs { get; set; }
    }
}