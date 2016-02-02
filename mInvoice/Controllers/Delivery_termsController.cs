using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class Delivery_termsController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Delivery_terms
        public ActionResult Index()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            var _list = from cust in db.Delivery_terms
                        where cust.clients_id == _clientsysid
                        select cust;

            return View(_list.ToList());            
        }

        // GET: Delivery_terms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_terms item = db.Delivery_terms.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Delivery_terms/Create
        public ActionResult Create()
        {
            int _client_id = -1;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _client_id = Convert.ToInt32(Session["client_id"]);
            ViewBag.clients_id = _client_id;

            Delivery_terms _item = new Delivery_terms();
            _item.clients_id = _client_id;

            return View(_item);
        }

        // POST: Delivery_terms/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,description,code,CreatedAt,UpdatedAt")] Delivery_terms item)
        {            
            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                item.clients_id = Convert.ToInt32(Session["client_id"]); 

                db.Delivery_terms.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Delivery_terms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_terms item = db.Delivery_terms.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Delivery_terms/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,description,code,CreatedAt,UpdatedAt")] Delivery_terms item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Delivery_terms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_terms item = db.Delivery_terms.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Delivery_terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delivery_terms item = db.Delivery_terms.Find(id);
            db.Delivery_terms.Remove(item);
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
