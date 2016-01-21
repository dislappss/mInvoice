using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class Tax_ratesController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Tax_rates
        public ActionResult Index()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            var _list = from cust in db.Tax_rates
                                              where cust.clients_id == _clientsysid
                                              select cust;

            return View(_list.ToList());
        }

        // GET: Tax_rates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax_rates tax_rates = db.Tax_rates.Find(id);
            if (tax_rates == null)
            {
                return HttpNotFound();
            }
            return View(tax_rates);
        }

        // GET: Tax_rates/Create
        public ActionResult Create()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.clientsysid = Convert.ToInt32(Session["client_id"]); 

            return View();
        }

        // POST: Tax_rates/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,description,code,value,CreatedAt,UpdatedAt")] Tax_rates tax_rates)
        {
            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                tax_rates.clients_id = Convert.ToInt32(Session["client_id"]); 

                db.Tax_rates.Add(tax_rates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tax_rates);
        }

        // GET: Tax_rates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax_rates tax_rates = db.Tax_rates.Find(id);
            if (tax_rates == null)
            {
                return HttpNotFound();
            }
            return View(tax_rates);
        }

        // POST: Tax_rates/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,description,code,value,CreatedAt,UpdatedAt")] Tax_rates tax_rates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tax_rates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tax_rates);
        }

        // GET: Tax_rates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax_rates tax_rates = db.Tax_rates.Find(id);
            if (tax_rates == null)
            {
                return HttpNotFound();
            }
            return View(tax_rates);
        }

        // POST: Tax_rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tax_rates tax_rates = db.Tax_rates.Find(id);
            db.Tax_rates.Remove(tax_rates);
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
