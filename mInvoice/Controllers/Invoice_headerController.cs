using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using mInvoice.Models;
using PagedList;

namespace mInvoice.Controllers
{
    public class Invoice_headerController : BaseController
    {
        private myinvoice_dbEntities m_db = new myinvoice_dbEntities();

        // GET: Invoice_header
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var _headers = from s in m_db.Invoice_header
                           where s.clients_id == _clientsysid
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                _headers = _headers.Where(s => 
                    s.invoice_no.Contains(searchString)
                 || s.Customers.customer_no.Contains(searchString)
                 || s.Customers.customer_name.Contains(searchString)
                 || s.order_date.ToString().Contains(searchString)
                 || s.delivery_date.ToString().Contains(searchString)
                 || s.city.ToString().Contains(searchString)
                 );
            }

            switch (sortOrder)
            {
                case "invoice_no_desc":
                    _headers = _headers.OrderByDescending(s => s.invoice_no);
                    break;
                case "customers_id_desc":
                    _headers = _headers.OrderByDescending(s => s.customers_id );
                    break;
                case "customer_desc":
                    _headers = _headers.OrderByDescending(s => s.Customers.customer_name );
                    break;
                case "order_date_desc":
                    _headers = _headers.OrderByDescending(s => s.order_date);
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
                    //_countries = _countries.OrderBy(s => s.name);
                    _headers = _headers.OrderBy(s => s.invoice_no);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(_headers.ToPagedList(pageNumber, pageSize));

            //var invoice_header = db.Invoice_header.Include(i => i.Countries).Include(i => i.Customers);
            //return View(invoice_header.ToList());
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
            int _client_id =-1;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _client_id = Convert.ToInt32(Session["client_id"]);
            ViewBag.clients_id = _client_id; 

            DateTime _now = DateTime.Now;

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name");
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no");

            var _new_item = new Invoice_header ();

            _new_item.order_date = _now;
            _new_item.delivery_date = _now;

            return View(_new_item);
        }

        // POST: Invoice_header/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt,quantity_2_column_name,quantity_3_column_name")] Invoice_header invoice_header)
        {
            int _client_id = -1;

            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                _client_id = Convert.ToInt32(Session["client_id"]);

                invoice_header.clients_id = _client_id; 

                m_db.Invoice_header.Add(invoice_header);
                m_db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
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
            return View(invoice_header);
        }

        // POST: Invoice_header/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt,quantity_2_column_name,quantity_3_column_name")] Invoice_header invoice_header)
        {           
            if (ModelState.IsValid)
            {
                m_db.Entry(invoice_header).State = EntityState.Modified;

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
            // Details
            IQueryable<Invoice_details> _details = m_db.Invoice_details.Where(x => x.invoice_header_id == id);
            m_db.Invoice_details.RemoveRange(_details); 
            
            // Header
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            m_db.Invoice_header.Remove(invoice_header);
            m_db.SaveChanges();
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
            SqlTransaction _transaction = null;
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
           

            Clients _client = db.Clients.FirstOrDefault (x => x.Id == _client_id);

            _prefix_invoices = _client.prefix_invoices;
            _no_template_invoices = _client.no_template_invoices;
            _last_index_invoices = _client.last_index_invoices;

            string _prefix = !string.IsNullOrEmpty(_prefix_invoices) ? _prefix_invoices : "";
            string _year = DateTime.Today.Year.ToString();
            _year = _year.Substring(2, 2);
            string _number = getNumberStringByNumber(_last_index_invoices + 1);

            if (!string.IsNullOrEmpty(_no_template_invoices))
            {
                _ret_val = _no_template_invoices.Replace ("%P", _prefix).Replace ("%Y", _year).Replace ("%N", _number);
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
