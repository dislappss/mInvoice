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
    
    public partial class Customers
    {
        public int Id { get; set; }
        public int clientsysid { get; set; }
        public string customer_name { get; set; }
        public int countriesid { get; set; }
        public string email { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> payment_methodid { get; set; }
        public string customer_no { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string phone_2 { get; set; }
        public string www { get; set; }
        public string ustd_id { get; set; }
        public string finance_office { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string iban { get; set; }
        public string bic { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Countries Countries { get; set; }
        public virtual Payment_method Payment_method { get; set; }
    }
}
