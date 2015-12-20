using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class Invoice_headerController : Controller
    {
        private myinvoice_dbEntities3 db = new myinvoice_dbEntities3();

        // GET: Invoice_header
        public ActionResult Index()
        {
            var invoice_header = db.Invoice_header.Include(i => i.Countries).Include(i => i.Customers);
            return View(invoice_header.ToList());
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
        public ActionResult Create([Bind(Include = "Id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt")] Invoice_header invoice_header)
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
        public ActionResult Edit([Bind(Include = "Id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt")] Invoice_header invoice_header)
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
