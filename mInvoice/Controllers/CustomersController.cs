using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class CustomersController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Customers
        public ActionResult Index()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            IQueryable<Customers> customers = from cust in db.Customers
                                               where cust.clientsysid == _clientsysid
                                               select cust;

            customers = customers.Include(c => c.Clients).Include(c => c.Countries).Include(c => c.Payment_method).Include(c => c.Currency );

            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (Session["email"] != null 
                && Session["client_id"] != null) 
            {              
                ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name");
                ViewBag.payment_methodid = new SelectList(db.Payment_method.Where(k => k.clients_id == _client_id), "Id", "name");
                ViewBag.currency_id = new SelectList(db.Currency.OrderBy(s => s.name), "Id", "name");
                ViewBag.payment_terms_id = new SelectList(db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description");
                ViewBag.delivery_terms_id = new SelectList(db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description");
                ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description");

                Customers _new_obj = new Customers();
                _new_obj.clientsysid = _client_id;

                return View(_new_obj);
            }   
            else 
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Customers/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clientsysid,customer_name,countriesid,email,zip,city,street,CreatedAt,UpdatedAt,payment_methodid,customer_no,phone,fax,phone_2,www,ustd_id,finance_office,account_number,blz,bank_name,iban,bic,currency_id,payment_terms_id,delivery_terms_id,tax_rate_id")] Customers customers)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            try
            {
                if (ModelState.IsValid)
                {                    
                    db.Customers.Add(customers);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name", customers.countriesid);
                ViewBag.payment_methodid = new SelectList(db.Payment_method.Where(k => k.clients_id == _client_id), "Id", "name", customers.payment_methodid);
                ViewBag.currency_id = new SelectList(db.Currency.OrderBy(s => s.name), "Id", "name", customers.currency_id);
                ViewBag.payment_terms_id = new SelectList(db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", customers.payment_terms_id);
                ViewBag.delivery_terms_id = new SelectList(db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", customers.delivery_terms_id);
                ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", customers.tax_rate_id );

                return View(customers);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ModelState.AddModelError("", ex.Message);
                return View(customers);
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            Customers customers = db.Customers.Find(id);

            try
            {
                var _client_id = Convert.ToInt32(Session["client_id"]);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                if (customers == null)
                {
                    return HttpNotFound();
                }
                ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name", customers.countriesid);
                ViewBag.payment_methodid = new SelectList(db.Payment_method.Where(k => k.clients_id == _client_id), "Id", "name", customers.payment_methodid);
                ViewBag.currency_id = new SelectList(db.Currency.OrderBy(s => s.name), "Id", "name", customers.currency_id);
                ViewBag.payment_terms_id = new SelectList(db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", customers.payment_terms_id);
                ViewBag.delivery_terms_id = new SelectList(db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", customers.delivery_terms_id);
                ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", customers.tax_rate_id);

                return View(customers);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ModelState.AddModelError("", ex.Message);
                return View(customers);
            }
        }

        // POST: Customers/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clientsysid,customer_name,countriesid,email,zip,city,street,CreatedAt,UpdatedAt,payment_methodid,customer_no,phone,fax,phone_2,www,ustd_id,finance_office,account_number,blz,bank_name,iban,bic,currency_id,payment_terms_id,delivery_terms_id,tax_rate_id")] Customers customers)
        {
            var _client_id = Convert.ToInt32(Session["client_id"]);

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customers).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name", customers.countriesid);
                ViewBag.payment_methodid = new SelectList(db.Payment_method.Where(k => k.clients_id == _client_id), "Id", "name", customers.payment_methodid);
                ViewBag.currency_id = new SelectList(db.Currency.OrderBy(s => s.name), "Id", "name", customers.currency_id);
                ViewBag.payment_terms_id = new SelectList(db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", customers.payment_terms_id);
                ViewBag.delivery_terms_id = new SelectList(db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", customers.delivery_terms_id);
                ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", customers.tax_rate_id);

                return View(customers);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ModelState.AddModelError("", ex.Message);
                return View(customers);
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
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
