//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mInvoice_api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment_method
    {
        public Payment_method()
        {
            this.Customers = new HashSet<Customers>();
        }
    
        public int Id { get; set; }
        public int clients_id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
