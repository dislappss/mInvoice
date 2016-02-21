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
        public Customers()
        {
            this.Invoice_header = new HashSet<Invoice_header>();
        }
    
        public int Id { get; set; }
        public int clientsysid { get; set; }
        public Nullable<int> payment_methodid { get; set; }
        public string customer_no { get; set; }
        public string customer_name { get; set; }
        public int countriesid { get; set; }
        public string email { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public Nullable<int> countriesid_invoice { get; set; }
        public string email_invoice { get; set; }
        public string zip_invoice { get; set; }
        public string city_invoice { get; set; }
        public string street_invoice { get; set; }
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
        public Nullable<int> currency_id { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> payment_terms_id { get; set; }
        public Nullable<int> delivery_terms_id { get; set; }
        public Nullable<int> tax_rate_id { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Payment_method Payment_method { get; set; }
        public virtual ICollection<Invoice_header> Invoice_header { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Delivery_terms Delivery_terms { get; set; }
        public virtual Payment_terms Payment_terms { get; set; }
        public virtual Tax_rates Tax_rates { get; set; }
        public virtual Countries Countries { get; set; }
    }
}
