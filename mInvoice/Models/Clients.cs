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
    
    public partial class Clients
    {
        public Clients()
        {
            this.Articles = new HashSet<Articles>();
            this.Customers = new HashSet<Customers>();
            this.Delivery_terms = new HashSet<Delivery_terms>();
            this.Invoice_details = new HashSet<Invoice_details>();
            this.Invoice_header = new HashSet<Invoice_header>();
            this.Payment_terms = new HashSet<Payment_terms>();
            this.Tax_rates = new HashSet<Tax_rates>();
            this.Costs = new HashSet<Costs>();
            this.Type_of_costs = new HashSet<Type_of_costs>();
        }
    
        public int Id { get; set; }
        public string AspNetUsers_id { get; set; }
        public string clientname { get; set; }
        public string email { get; set; }
        public string owner { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public Nullable<int> countriesid { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string www { get; set; }
        public string ustd_id { get; set; }
        public string tax_number { get; set; }
        public string finance_office { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string iban { get; set; }
        public string bic { get; set; }
        public byte[] picture { get; set; }
        public string prefix_invoices { get; set; }
        public int last_index_invoices { get; set; }
        public string no_template_invoices { get; set; }
        public string text_to_table { get; set; }
        public string text_under_the_table_bold { get; set; }
        public string text_under_the_table { get; set; }
        public string email_subject { get; set; }
        public string email_message { get; set; }
        public string email_user_name { get; set; }
        public string email_password { get; set; }
        public string email_host { get; set; }
        public Nullable<int> email_port { get; set; }
        public Nullable<int> currency_id { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
        public virtual ICollection<Articles> Articles { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Delivery_terms> Delivery_terms { get; set; }
        public virtual ICollection<Invoice_details> Invoice_details { get; set; }
        public virtual ICollection<Invoice_header> Invoice_header { get; set; }
        public virtual ICollection<Payment_terms> Payment_terms { get; set; }
        public virtual ICollection<Tax_rates> Tax_rates { get; set; }
        public virtual Countries Countries { get; set; }
        public virtual ICollection<Costs> Costs { get; set; }
        public virtual ICollection<Type_of_costs> Type_of_costs { get; set; }
    }
}
