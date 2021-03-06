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
    
    public partial class Invoice_header
    {
        public int Id { get; set; }
        public int clients_id { get; set; }
        public string invoice_no { get; set; }
        public System.DateTime order_date { get; set; }
        public System.DateTime delivery_date { get; set; }
        public int customers_id { get; set; }
        public string customer_reference { get; set; }
        public int countriesid { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string quantity_2_column_name { get; set; }
        public string quantity_3_column_name { get; set; }
        public Nullable<int> tax_rate_id { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<int> payment_terms_id { get; set; }
        public Nullable<int> delivery_terms_id { get; set; }
        public Nullable<System.DateTime> paid_at { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public int currency_id { get; set; }
        public Nullable<System.DateTime> due_date { get; set; }
        public Nullable<decimal> freight_costs { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual Delivery_terms Delivery_terms { get; set; }
        public virtual Payment_terms Payment_terms { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Countries Countries { get; set; }
    }
}
