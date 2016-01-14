using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;
using PagedList;

namespace mInvoice.Controllers
{
    public class Invoice_headerController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Invoice_header
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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


            var _headers = from s in db.Invoice_header 
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
            Invoice_header invoice_header = db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }
            return View(invoice_header);
        }

        // GET: Invoice_header/Create
        public ActionResult Create()
        {
            DateTime _now = DateTime.Now;

            ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name");
            ViewBag.customers_id = new SelectList(db.Customers, "Id", "customer_no");

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
        public ActionResult Create([Bind(Include = "Id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt,quantity_2_column_name,quantity_3_column_name")] Invoice_header invoice_header)
        {
            if (ModelState.IsValid)
            {


                db.Invoice_header.Add(invoice_header);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.countriesid = new SelectList(db.Countries, "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(db.Customers, "Id", "customer_no", invoice_header.customers_id);
            return View(invoice_header);
        }

        // GET: Invoice_header/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }
            ViewBag.countriesid = new SelectList(db.Countries, "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(db.Customers, "Id", "customer_no", invoice_header.customers_id);
            return View(invoice_header);
        }

        // POST: Invoice_header/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt,quantity_2_column_name,quantity_3_column_name")] Invoice_header invoice_header)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_header).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.countriesid = new SelectList(db.Countries, "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(db.Customers, "Id", "customer_no", invoice_header.customers_id);
            return View(invoice_header);
        }

        // GET: Invoice_header/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = db.Invoice_header.Find(id);
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
            Invoice_header invoice_header = db.Invoice_header.Find(id);
            db.Invoice_header.Remove(invoice_header);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
