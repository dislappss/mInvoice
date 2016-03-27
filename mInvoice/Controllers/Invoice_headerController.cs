using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using mInvoice.App_GlobalResources;
using mInvoice.Models;
using MvcReportViewer;
using PagedList;

namespace mInvoice.Controllers
{
    public class Invoice_headerController : BaseController
    {
        string m_zugferd_file_path = null;

        private myinvoice_dbEntities m_db = new myinvoice_dbEntities();
        private string m_pdf_output_file;

        // GET: Invoice_header
        public ActionResult Index(
            string sortOrder
            , string currentFilter
            , string searchString
            , int? page
            , string radioPaid)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.invoice_noSortParm = sortOrder == "invoice_no" ? "invoice_no_desc" : "invoice_no";
            ViewBag.customers_idSortParm = sortOrder == "customers_id" ? "customers_id_desc" : "customers_id";
            ViewBag.customerSortParm = sortOrder == "customer" ? "customer_desc" : "customer";
            ViewBag.order_dateSortParm = sortOrder == "order_date" ? "order_date_desc" : "order_date";
            ViewBag.delivery_dateSortParm = sortOrder == "delivery_date" ? "delivery_date_desc" : "delivery_date";
            ViewBag.citySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.country_codeSortParm = sortOrder == "country_code" ? "country_code_desc" : "country_code";
            ViewBag.paid_atParm = sortOrder == "paid_at" ? "paid_at_desc" : "paid_at";
            ViewBag.IsPaid = radioPaid == "true" ? true : (radioPaid == "null" ? new bool?() : false);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            IQueryable <Invoice_header> _headers = null;

            if (String.IsNullOrEmpty(radioPaid))
            {
                _headers = from s in m_db.Invoice_header
                           where s.clients_id == _clientsysid &&
                                 s.paid_at == null
                           select s;


            }
            else
            {
                if (radioPaid != "true" && radioPaid != "false")
                {
                    _headers = from s in m_db.Invoice_header
                               where s.clients_id == _clientsysid
                               select s;

                }
                else if (radioPaid == "true")
                    _headers = from s in m_db.Invoice_header
                               where s.clients_id == _clientsysid &&
                                     s.paid_at != null
                               select s;
                else if (radioPaid == "false")
                    _headers = from s in m_db.Invoice_header
                               where s.clients_id == _clientsysid &&
                                     s.paid_at == null
                               select s;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                _headers = _headers.Where(s =>
                    s.invoice_no.Contains(searchString)
                 || s.Customers.customer_no.Contains(searchString)
                 || s.Customers.customer_name.Contains(searchString)
                 || s.order_date.ToString().Contains(searchString)
                 || s.delivery_date.ToString().Contains(searchString)
                 || s.city.ToString().Contains(searchString)
                 || s.paid_at.ToString().Contains(searchString)
                 );
            }

            switch (sortOrder)
            {
                case "invoice_no_desc":
                    _headers = _headers.OrderByDescending(s => s.invoice_no);
                    break;
                case "customers_id_desc":
                    _headers = _headers.OrderByDescending(s => s.customers_id);
                    break;
                case "customer_desc":
                    _headers = _headers.OrderByDescending(s => s.Customers.customer_name);
                    break;
                case "order_date_desc":
                    _headers = _headers.OrderByDescending(s => s.order_date);
                    break;
                case "paid_at_desc":
                    _headers = _headers.OrderByDescending(s => s.paid_at);
                    break;
                case "paid_at":
                    _headers = _headers.OrderBy(s => s.paid_at);
                    break;
                case "delivery_date_desc":
                    _headers = _headers.OrderByDescending(s => s.delivery_date);
                    break;
                case "city_desc":
                    _headers = _headers.OrderByDescending(s => s.city);
                    break;

                case "invoice_no":
                    _headers = _headers.OrderBy(s => s.invoice_no);
                    break;
                case "customer":
                    _headers = _headers.OrderBy(s => s.Customers.customer_name);
                    break;
                case "customers_id":
                    _headers = _headers.OrderBy(s => s.customers_id);
                    break;
                case "order_date":
                    _headers = _headers.OrderBy(s => s.order_date);
                    break;
                case "delivery_date":
                    _headers = _headers.OrderBy(s => s.delivery_date);
                    break;
                case "city":
                    _headers = _headers.OrderBy(s => s.city);
                    break;
                
                default:
                    _headers = _headers.OrderBy(s => s.invoice_no);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(_headers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Invoice_header/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }
            return View(invoice_header);
        }

        // GET: Invoice_header/Create
        public ActionResult Create()
        {
            int _client_id = -1;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _client_id = Convert.ToInt32(Session["client_id"]);
            ViewBag.clients_id = _client_id;

            DateTime _now = DateTime.Now;

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name");
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no");
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description");
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description");
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name");
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description");

            var _new_item = new Invoice_header();

            _new_item.clients_id = _client_id;
            _new_item.order_date = _now;
            _new_item.delivery_date = _now;

            return View(_new_item);
        }

        // POST: Invoice_header/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,invoice_no,order_date,delivery_date,due_date,paid_at,customers_id,customer_reference,countriesid,zip,city,street,quantity_2_column_name,quantity_3_column_name,tax_rate_id,discount,payment_terms_id,delivery_terms_id,currency_id,freight_costs")] Invoice_header invoice_header)
        {
            int _client_id = -1;

            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                _client_id = Convert.ToInt32(Session["client_id"]);

                invoice_header.invoice_no = getNewInvoiceNo(m_db);
                invoice_header.clients_id = _client_id;

                m_db.Invoice_header.Add(invoice_header);
                m_db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.customers_id);
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.customers_id);
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name", invoice_header.currency_id);
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.tax_rate_id);

            return View(invoice_header);
        }

        // GET: Invoice_header/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.payment_terms_id);
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.delivery_terms_id);
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name", invoice_header.currency_id);
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.tax_rate_id);        

            return View(invoice_header);
        }

        // POST: Invoice_header/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,invoice_no,order_date,delivery_date,due_date,paid_at,customers_id,customer_reference,countriesid,zip,city,street,quantity_2_column_name,quantity_3_column_name,tax_rate_id,discount,payment_terms_id,delivery_terms_id,currency_id,freight_costs")] Invoice_header invoice_header)
        {
            if (ModelState.IsValid)
            {
                m_db.Entry(invoice_header).State = EntityState.Modified;

                //var _due_date = invoice_header.due_date;
                //var _paid_at = invoice_header.paid_at;
                //invoice_header.delivery_date = new DateTime(2016, 3, 3);
                //var _order_date = invoice_header.order_date;

                m_db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (invoice_header == null)
            {
                return HttpNotFound();
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.payment_terms_id);
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.delivery_terms_id);
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name", invoice_header.currency_id);
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.tax_rate_id);

            return View(invoice_header);
        }

        // GET: Invoice_header/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }
            return View(invoice_header);
        }

        // POST: Invoice_header/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            using (var _db = new myinvoice_dbEntities())
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        // Details
                        IQueryable<Invoice_details> _details =
                            m_db.Invoice_details.Where(x => x.invoice_header_id == id).Where(x => x.clients_id == _client_id);
                        m_db.Invoice_details.RemoveRange(_details);

                        // Header
                        Invoice_header invoice_header = m_db.Invoice_header.Find(id);
                        m_db.Invoice_header.Remove(invoice_header);

                        m_db.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError("", ex);
                        dbContextTransaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex);
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Copy_invoice(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice_header _invoice_header = m_db.Invoice_header.Find(id);

            Copy_Invoice _copy_Invoice = new Copy_Invoice();
            _copy_Invoice.invoice_header_id = (int)id;
            _copy_Invoice.quantity_2_column_name = _invoice_header.quantity_2_column_name;
            _copy_Invoice.quantity_3_column_name = _invoice_header.quantity_3_column_name;

            return View(_copy_Invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy_invoice(Copy_Invoice copy_invoice)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            int _new_invoice_header_id = -1;
            Invoice_header _new_header = new Invoice_header();

            if (ModelState.IsValid)
            {
                using (var _db = new myinvoice_dbEntities())
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Old header
                            Invoice_header _invoice_header = _db.Invoice_header.Find(copy_invoice.invoice_header_id);

                            // Old details
                            int _invoice_header_id = copy_invoice.invoice_header_id;
                            int _client_id = Convert.ToInt32(Session["client_id"]);

                            IQueryable<Invoice_details> invoice_details_arr =
                                from cust in _db.Invoice_details
                                where cust.invoice_header_id == _invoice_header_id &&
                                      cust.clients_id == _client_id
                                select cust;

                            //var result = invoice_details.Include(i => i.Articles).Include(i => i.Invoice_header).Include(i => i.Tax_rates);

                            // New header

                            _new_header = _invoice_header;

                            DateTime _now = DateTime.Now;
                            _new_header.order_date = _now;
                            _new_header.invoice_no = getNewInvoiceNo(_db);
                            _new_header.delivery_date = _now;
                            if (!string.IsNullOrEmpty(copy_invoice.quantity_2_column_name))
                                _new_header.quantity_2_column_name = copy_invoice.quantity_2_column_name;
                            if (!string.IsNullOrEmpty(copy_invoice.quantity_3_column_name))
                                _new_header.quantity_3_column_name = copy_invoice.quantity_3_column_name;

                            Invoice_header _inserted_header = _db.Invoice_header.Add(_new_header);

                            _db.SaveChanges();

                            _new_invoice_header_id = _inserted_header.Id;

                            // New details
                            foreach (Invoice_details _old_position in invoice_details_arr)
                            {
                                Invoice_details _new_position = new Invoice_details();

                                _new_position = _old_position;

                                _new_position.clients_id = _client_id;
                                _new_position.invoice_header_id = _new_invoice_header_id;
                                if (copy_invoice.quantity_2 >= 0)
                                    _new_position.quantity_2 = copy_invoice.quantity_2;
                                if (copy_invoice.quantity_3 >= 0)
                                    _new_position.quantity_3 = copy_invoice.quantity_3;

                                _db.Invoice_details.Add(_new_position);
                                _db.SaveChanges();
                            }

                            // Update Clients
                            Clients _client = _db.Clients.FirstOrDefault(x => x.Id == _client_id);
                            _client.last_index_invoices = _client.last_index_invoices + 1;
                            _db.Entry(_client).State = EntityState.Modified;
                            _db.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            ModelState.AddModelError("", ex);
                            dbContextTransaction.Rollback();
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex);
                            dbContextTransaction.Rollback();
                        }
                    }
                }
            }
            return RedirectToAction("Edit", new { id = _new_invoice_header_id });
        }

        private string getNewInvoiceNo(myinvoice_dbEntities db)
        {
            string _ret_val = null;
            string _prefix_invoices = null, _no_template_invoices = null;
            int _last_index_invoices = 1;
            int _client_id = Convert.ToInt32(Session["client_id"]);


            Clients _client = db.Clients.FirstOrDefault(x => x.Id == _client_id);

            _prefix_invoices = _client.prefix_invoices;
            _no_template_invoices = _client.no_template_invoices;
            _last_index_invoices = _client.last_index_invoices;

            string _prefix = !string.IsNullOrEmpty(_prefix_invoices) ? _prefix_invoices : "";
            string _year = DateTime.Today.Year.ToString();
            _year = _year.Substring(2, 2);
            string _number = getNumberStringByNumber(_last_index_invoices + 1);

            if (!string.IsNullOrEmpty(_no_template_invoices))
            {
                _ret_val = _no_template_invoices.Replace("%P", _prefix).Replace("%Y", _year).Replace("%N", _number);
            }
            else
            {
                _ret_val = _number;
            }

            return _ret_val;
        }

        private string getNumberStringByNumber(int Number)
        {
            string _ret_val = null;

            int _len = Number.ToString().Length;
            int _count_of_zeros = 6 - _len;

            _ret_val = new String('0', _count_of_zeros);
            _ret_val = _ret_val + Number;

            return _ret_val;
        }       

        public ActionResult mark_as_paid(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice_header invoice_header = m_db.Invoice_header.Find(id);

            if (invoice_header == null)
            {
                return HttpNotFound();
            }

            if (invoice_header.paid_at.HasValue)
                invoice_header.paid_at = null;
            else
                invoice_header.paid_at = DateTime.Today;

            m_db.SaveChanges();

            return RedirectToAction("Index", new { @sortOrder = "null" });
        }

        public ActionResult EmailForm(int? id, bool zugferd)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice_header _invoice_header = m_db.Invoice_header.Find(id);
            EmailFormModel _email_form = new EmailFormModel();
            Clients _client = m_db.Clients.Find(Convert.ToInt32(Session["client_id"]));
            Customers _customer = m_db.Customers.Find(_invoice_header.customers_id);

            _email_form.ID = (int)id;
            _email_form.Invoice_No = _invoice_header.invoice_no;
            _email_form.From = _client.email;
            _email_form.Subject = _client.email_subject.Replace("%1", _invoice_header.invoice_no);
            _email_form.Message = _client.email_message.Replace("%1", _invoice_header.invoice_no);
            _email_form.To = _customer.email;

            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            string _filename = "invoice_" + Guid.NewGuid().ToString() + ".pdf";
            string _file_path = Path.Combine(pathDownload, _filename);

            _email_form.Attachment = _file_path;

            return View(_email_form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> EmailForm(EmailFormModel EmailForm)
        {
            string _guid = Guid.NewGuid().ToString();
            string _pdf_output_file = HttpContext.Server.MapPath("~/App_Data/invoice_" + _guid + ".pdf");

            m_pdf_output_file = null;

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection _connection = new SqlConnection(connectionString);

                if (ModelState.IsValid)
                {
                    if (Session["client_id"] == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    int _client_id = Convert.ToInt32(Session["client_id"]);

                    var model = GetData_invoice(EmailForm.ID, EmailForm.Invoice_No);               

                    string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string pathDownload = Path.Combine(pathUser, "Downloads");

                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress("dnepr65@gmail.com"); //EmailForm.From);
                    msg.To.Add(new MailAddress("dnepr65@gmail.com")); //EmailForm.To));
                    msg.Subject = EmailForm.Subject;
                    msg.Body = EmailForm.Message;

                    Attachment att1 = null;

                    // Create PDF-File
                    //Stream _stream = 
                    CreateInvoicePDFFile(
                        EmailForm.Attachment,
                       _pdf_output_file,
                        model);

                    // ZUGFeRD
                    //if (EmailForm.Zugferd)
                    //{
                    //    Reports.reportsDataSetTableAdapters.rp_invoice_detailsTableAdapter
                    //        _rp_invoice_detailsTableAdapter = new Reports.reportsDataSetTableAdapters.rp_invoice_detailsTableAdapter();
                    //    Reports.reportsDataSet _reportsDataSet = new Reports.reportsDataSet();

                    //    _rp_invoice_detailsTableAdapter.Fill(
                    //        _reportsDataSet.rp_invoice_details,
                    //        _client_id,
                    //        EmailForm.ID);

                    //    Reports.reportsDataSet.rp_invoice_detailsRow _row = _reportsDataSet.rp_invoice_details[0];

                    //    ZUGFeRD_Test.main_form _zugferd = new ZUGFeRD_Test.main_form();

                    //    TotalInvoiceInfo _total_info = getTotalInvoiceInfo(_reportsDataSet.rp_invoice_details);

                    //    m_zugferd_file_path = _zugferd.getZugFeRD_PDF(
                    //          _pdf_output_file
                    //        , _connection
                    //        , _client_id
                    //        , EmailForm.ID  // invoice_header_id ?
                    //        , _row.invoice_no
                    //        , _row.order_date
                    //        , _row.Currency_customer_code
                    //        , _row.currency_code   //CurrencyShortmark_client
                    //        , _row.customer_name
                    //        , _row.Customers_zip
                    //        , _row.Customers_city
                    //        , _row.Customers_street
                    //        , _row.customer_no
                    //        , _row.tax_number
                    //        , _row.clientname
                    //        , _row.Clients_zip
                    //        , _row.Customers_city
                    //        , _row.Customers_street
                    //        , _row.Clients_ustd_id
                    //        , _row.Countries_iso  //Clients_country_code
                    //        , _row.delivery_date
                    //        , _total_info.valueofgoods
                    //        , _total_info.valueofgoods_without_discount
                    //        , _row.Isfreight_costsNull() ? new decimal?() : _row.freight_costs
                    //        , _total_info.subtotal
                    //        , _total_info.taxtotalAmount
                    //        , _total_info.total
                    //        , _row.Tax_rates_value
                    //        , _row.Payment_terms_description
                    //        , _row.Isdue_dateNull() ? new DateTime?() : _row.due_date
                    //        , _row.Clients_iban
                    //        , _row.Clients_bic
                    //        , _row.Clients_account_number
                    //        , _row.Clients_bic
                    //        , _row.Clients_bank_name
                    //        , _reportsDataSet.rp_invoice_details
                    //        , Resource.TradeLineCommentItem
                    //        , Server.MapPath("~/images/profile") //ZugFERDResourceDirectory
                    //        , Resource.LogisticsServiceChargeDescription //= "Versandkosten"
                    //        , Resource.TradeAllowanceChargeDescription //= "Sondernachlass"
                    //        );

                    //    att1 = new Attachment(m_zugferd_file_path);
                    //    att1.Name = Path.GetFileName(EmailForm.Attachment);
                    //    msg.Attachments.Add(att1);

                    //    System.IO.File.Delete(m_zugferd_file_path);
                    //}
                    //else
                    //{
                    att1 = new Attachment(_pdf_output_file);
                    att1.Name = Path.GetFileName(EmailForm.Attachment);
                    msg.Attachments.Add(att1);
                    //}

                    // Save invoice file
                    if (Session["invoicesPath"] != null)
                    {
                        var _invoiceDirectory = Session["invoicesPath"].ToString();

                        //tring targetFolder = HttpContext.Current.Server.MapPath("~/uploads/logo"); 
                        DateTime _now = DateTime.Now;

                        string targetPath = Path.Combine(_invoiceDirectory,
                            EmailForm.Invoice_No + "_"
                            + _now.Year
                            + (_now.Month.ToString().Length == 1 ? "0" + _now.Month.ToString() : _now.Month.ToString())
                            + (_now.Day.ToString().Length == 1 ? "0" + _now.Day.ToString() : _now.Day.ToString())
                            + (_now.Hour.ToString().Length == 1 ? "0" + _now.Hour.ToString() : _now.Hour.ToString())
                            + (_now.Minute.ToString().Length == 1 ? "0" + _now.Minute.ToString() : _now.Minute.ToString())
                             + "_email_"
                            + ".pdf"
                            );

                        // Archive
                        if (!Directory.Exists(_invoiceDirectory))
                            Directory.CreateDirectory(_invoiceDirectory);

                        if (System.IO.File.Exists(targetPath))
                            System.IO.File.Delete(targetPath);

                        if (EmailForm.Zugferd)
                        {
                            System.IO.File.Copy(m_zugferd_file_path, targetPath);
                        }
                        else
                        {
                            System.IO.File.Copy(_pdf_output_file, targetPath);
                        }

                        System.IO.File.SetCreationTime(targetPath, _now);
                    }

                    if (!string.IsNullOrEmpty(_pdf_output_file))
                        m_pdf_output_file = _pdf_output_file;
                    else
                        m_pdf_output_file = m_zugferd_file_path;

                    // Send E-Mail
                    var _client = m_db.Clients.Find(_client_id);

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = _client.email_user_name,
                            Password = _client.email_password
                        };
                        smtp.Host = _client.email_host;
                        smtp.Port = (int)_client.email_port;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = credential;
                        smtp.Timeout = 20000;

                        smtp.SendCompleted += new SendCompletedEventHandler(SendCompleted);

                        await smtp.SendMailAsync(msg);
                    }
                }             

                return RedirectToAction("Index");
            }
            catch (FormatException ex)
            {
                FlashHelpers.FlashError(this, ex.Message);
            }
            catch (Exception ex)
            {
                FlashHelpers.FlashError(this, ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult PrintHeader(int id, string invoice_no)
        {
            string _targetPath = null;
            string _guid = Guid.NewGuid().ToString(); 
            string _output_full_file_path = HttpContext.Server.MapPath("~/App_Data/invoice_" + _guid + ".pdf");

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _client_id = Convert.ToInt32(Session["client_id"]);

            var model = GetData_invoice(id, invoice_no);         

            // Create PDF-File
            string _file_name = model.Invoice_no ;           

            Stream _stream = CreateInvoicePDFFile(_file_name, _output_full_file_path, model);

            // Archive
            if (Session["invoicesPath"] != null)
            {
                var _invoiceDirectory = Session["invoicesPath"].ToString();

                DateTime _now = DateTime.Now;

                _targetPath = Path.Combine(_invoiceDirectory,
                           _file_name + "_" 
                           + _now.Year
                           + (_now.Month.ToString().Length == 1 ? "0" + _now.Month.ToString() : _now.Month.ToString())
                           + (_now.Day.ToString().Length == 1 ? "0" + _now.Day.ToString() : _now.Day.ToString())
                           + (_now.Hour.ToString().Length == 1 ? "0" + _now.Hour.ToString() : _now.Hour.ToString())
                           + (_now.Minute.ToString().Length == 1 ? "0" + _now.Minute.ToString() : _now.Minute.ToString())
                           + "_print_"
                           + ".pdf"
                           );

                if (!Directory.Exists(_invoiceDirectory))
                    Directory.CreateDirectory(_invoiceDirectory);

                if (System.IO.File.Exists(_targetPath))
                    System.IO.File.Delete(_targetPath);

                System.IO.File.SetCreationTime(_output_full_file_path, _now);
                System.IO.File.Copy(_output_full_file_path, _targetPath);               
                System.IO.File.Delete(_output_full_file_path);
            }

            // Open PDF-File       
            if (!string.IsNullOrEmpty(_targetPath))
            {
                return RedirectToAction("invoice", "Reports", new { id = id, ShowPrintButton = true });
            }
            else
            {
                FlashHelpers.FlashError(this, Resource.cant_open_file);

                return RedirectToAction("Index");
            }
        }

        public static byte[] Stream2ByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private Stream CreateInvoicePDFFile(
            string TmpFileName
            , string OutputFileName
            , InvoiceModel _localReportDataSources)
        {
            Stream _stream = null;

            string _lang = Thread.CurrentThread.CurrentCulture.EnglishName == "German" ? "de" : "en";
            string _culture =
               System.Threading.Thread.CurrentThread.CurrentCulture.Name + "-" +
               System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper();

            if(!string.IsNullOrEmpty (TmpFileName))
                _stream = this.Report(
                    ReportFormat.Pdf
                    , "/mInvoiceReports/invoice"
                    , reportParameters: 
                            new
                            {
                                clientid = _localReportDataSources.client_id,
                                language = _localReportDataSources.language,
                                culture = _culture,
                                invoiceheaderid = _localReportDataSources.Invoice_header_id  
                            }
                    //, localReportDataSources: _localReportDataSources
                    , mode: Microsoft.Reporting.WebForms.ProcessingMode.Remote
                    , filename: TmpFileName
                    ).FileStream;
            else
                _stream = this.Report(
                    ReportFormat.Pdf
                    , "Reports/invoice.rdlc"
                    , reportParameters: _localReportDataSources
                    //, localReportDataSources: _localReportDataSources
                    , mode: Microsoft.Reporting.WebForms.ProcessingMode.Remote
                    ).FileStream;

            if (System.IO.File.Exists(OutputFileName))
                System.IO.File.Delete(OutputFileName);

            byte[] buffer = new byte[20480];

            using (System.IO.FileStream output = new FileStream(OutputFileName, FileMode.OpenOrCreate))
            {
                int readBytes = 0;
                while ((readBytes = _stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, readBytes);
                }
            }
            return _stream;
        }

        private void SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("The message [{0}] was canceled.", e.UserState.ToString());

                FlashHelpers.FlashWarning(this, string.Format(Resource.email_canceled, e.UserState.ToString()));
            }

            if (e.Error != null)
            {
                StringBuilder errorMsg = new StringBuilder();
                errorMsg.Append(string.Format(Resource.email_message_error, e.UserState.ToString(), e.Error.Message));

                if (e.Error.InnerException != null)
                    errorMsg.Append(Environment.NewLine + "Inner exception message: " + e.Error.InnerException.Message);

                FlashHelpers.FlashError(this, errorMsg.ToString ());
            }
            else
            {
                Response.Write("<script>alert('" + Resource.email_message_sent + "');</script>");
                Console.WriteLine("Message [{0}] sent.", e.UserState.ToString());

                FlashHelpers.FlashSuccess(this, Resource.email_message_sent);
            }

            try
            {
                if (!string.IsNullOrEmpty(m_pdf_output_file) &&
                    System.IO.File.Exists(m_pdf_output_file))

                    System.IO.File.Delete(m_pdf_output_file);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.InnerException   ); 
            }
        }

        public void CancelMail(SmtpClient client)
        {
            client.SendAsyncCancel();
        }

        public ActionResult Archive(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["invoicesPath"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice_header _invoice_header = m_db.Invoice_header.Find(id);

            if (_invoice_header == null)
            {
                return HttpNotFound();
            }

            var invoice_nr = _invoice_header.invoice_no;
            var _invoiceDirectory = Session["invoicesPath"].ToString();

            var _files = Directory.GetFiles(_invoiceDirectory, invoice_nr + "*.pdf").ToList();

            List<Models.Archive> _list = new List<Models.Archive>();

            foreach (string _file in _files)
            {
                string _filename = Path.GetFileNameWithoutExtension(_file);
                Models.Archive.archiveType _type = Models.Archive.archiveType.Unknown;

                if (_filename.Contains("email"))
                    _type = Models.Archive.archiveType.Mail;
                else if (_filename.Contains("print"))
                    _type = Models.Archive.archiveType.Report;
                else
                    _type = Models.Archive.archiveType.Unknown;

                _list.Add(
                    new Archive(
                        _file
                        , Path.GetFileName(_file).Replace(".pdf", "")
                        , System.IO.File.GetCreationTime(_file)
                        , _type
                    ));
            }
            return View(_list.OrderByDescending(p => p.CreateDate));
        }

        public ActionResult GetFile(string Id)
        {
            var _invoiceDirectory = Session["invoicesPath"].ToString();
            var _filePath = Path.Combine(_invoiceDirectory, Id + ".pdf");

            //return File(_filePath, "application/pdf");

            if (System.IO.File.Exists(_filePath))
            {
                return File(_filePath, "application/pdf", Path.GetFileName(_filePath)); // Here is where I want to cause the download, but this isn't working
            }
            else
            {
                FlashHelpers.FlashError(this, Resource.file_not_exists);

                return RedirectToAction("Edit", "Editor"); // Goes back to the editing page
            }
        }

        //private TotalInvoiceInfo getTotalInvoiceInfo(Reports.reportsDataSet.rp_invoice_detailsDataTable rp_invoice_detailsDataTable)
        //{
        //    TotalInvoiceInfo _totalInvoiceInfo = new TotalInvoiceInfo();

        //     decimal _valueofgoods_without_discount = rp_invoice_detailsDataTable.Sum(
        //        pos => pos.Invoice_details_price_netto * 
        //            pos.Invoice_details_quantity *
        //            (pos.IsInvoice_details_quantity_2Null() ? 1 : pos.Invoice_details_quantity_2) * 
        //            (pos.IsInvoice_details_quantity_3Null() ? 1 : pos.Invoice_details_quantity_3) *
        //            (pos.Isfreight_costsNull() ? 1 : pos.freight_costs > 0 ? pos.freight_costs : 1) 
        //        );

        //    decimal _valueofgoods = rp_invoice_detailsDataTable.Sum(
        //        pos => pos.Invoice_details_price_netto * 
        //            pos.Invoice_details_quantity *
        //            (pos.IsInvoice_details_quantity_2Null() ? 1 : pos.Invoice_details_quantity_2) * 
        //            (pos.IsInvoice_details_quantity_3Null() ? 1 : pos.Invoice_details_quantity_3) *
        //            (pos.Isfreight_costsNull() ? 1 : pos.freight_costs > 0 ? pos.freight_costs : 1) *
        //            (pos.IsInvoice_details_discountNull () ? 1 :  (100 - pos.Invoice_details_discount) / 100 )
        //        );

        //    decimal _vat = rp_invoice_detailsDataTable.Sum(
        //        pos => pos.Invoice_details_price_netto *
        //            pos.Invoice_details_quantity *
        //            (pos.IsInvoice_details_quantity_2Null() ? 1 : pos.Invoice_details_quantity_2) *
        //            (pos.IsInvoice_details_quantity_3Null() ? 1 : pos.Invoice_details_quantity_3) *
        //             (pos.Isfreight_costsNull() ? 1 : pos.freight_costs > 0 ? pos.freight_costs : 1) *
        //            (pos.IsInvoice_details_discountNull() ? 1 : (100 - pos.Invoice_details_discount) / 100) *
        //            (pos.IsTax_rates_valueNull() ? 0 : pos.Tax_rates_value)
        //        );

        //    _totalInvoiceInfo.valueofgoods = _valueofgoods;
        //    _totalInvoiceInfo.valueofgoods_without_discount = _valueofgoods_without_discount;
        //    _totalInvoiceInfo.subtotal = _valueofgoods;
        //    _totalInvoiceInfo.taxtotalAmount = _vat;
        //    _totalInvoiceInfo.total = _valueofgoods + _vat;

        //    return _totalInvoiceInfo;
        //}

        //public class TotalInvoiceInfo
        //{
        //    public decimal valueofgoods { get; set; }
        //    public decimal valueofgoods_without_discount { get; set; }
        //    public decimal subtotal { get; set; }
        //    public decimal taxtotalAmount { get; set; }
        //    public decimal total { get; set; }
        //}

        private mInvoice.Models.InvoiceModel GetData_invoice(int id, string Invoice_no)
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);
            string _lang = Thread.CurrentThread.CurrentCulture.EnglishName == "German" ? "de" : "en";

            var model = new mInvoice.Models.InvoiceModel (_client_id, _lang, id, Invoice_no);
            return model;
        }

        public JsonResult getCustomer(int customer_id)
        {
            if (customer_id > 0)
            {
                Customers _customer = m_db.Customers.Find(customer_id);
                if (_customer != null)
                {                   
                    List<string> _res_arr = new List<string>();

                    _res_arr.Add(_customer.countriesid.ToString());
                    _res_arr.Add(_customer.zip.ToString());
                    _res_arr.Add(_customer.city.ToString());
                    _res_arr.Add(_customer.street.ToString());
                    _res_arr.Add(_customer.payment_terms_id.ToString());
                    _res_arr.Add(_customer.delivery_terms_id.ToString());
                    _res_arr.Add(_customer.tax_rate_id.ToString());
                    _res_arr.Add(_customer.currency_id.ToString());                    

                    JsonResult _result = Json(_res_arr, JsonRequestBehavior.AllowGet);

                    return _result;
                }
                else
                    return null;
            }
            else
                return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
   
}