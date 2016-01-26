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
    public class Quantity_unitsController : Controller
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Quantity_units
        public ActionResult Index()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            var _list = from cust in db.Quantity_units
                        where cust.clients_id == _clientsysid
                        select cust;

            return View(_list.ToList());

            //var quantity_units = db.Quantity_units.Include(q => q.Clients);
            //return View(quantity_units.ToList());
        }

        // GET: Quantity_units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quantity_units quantity_units = db.Quantity_units.Find(id);
            if (quantity_units == null)
            {
                return HttpNotFound();
            }
            return View(quantity_units);
        }

        // GET: Quantity_units/Create
        public ActionResult Create()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.clients_id = new SelectList(db.Clients, "Id", "clientname");

            Quantity_units _item = new Quantity_units();
            _item.clients_id = Convert.ToInt32(Session["client_id"]);

            return View(_item);
        }

        // POST: Quantity_units/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,description,code,CreatedAt,UpdatedAt")] Quantity_units quantity_units)
        {
            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                quantity_units.clients_id = Convert.ToInt32(Session["client_id"]);

                db.Quantity_units.Add(quantity_units);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quantity_units);
        }

        // GET: Quantity_units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quantity_units quantity_units = db.Quantity_units.Find(id);
            if (quantity_units == null)
            {
                return HttpNotFound();
            }
            return View(quantity_units);
        }

        // POST: Quantity_units/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,description,code,CreatedAt,UpdatedAt")] Quantity_units quantity_units)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quantity_units).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clients_id = new SelectList(db.Clients, "Id", "clientname", quantity_units.clients_id);
            return View(quantity_units);
        }

        // GET: Quantity_units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quantity_units quantity_units = db.Quantity_units.Find(id);
            if (quantity_units == null)
            {
                return HttpNotFound();
            }
            return View(quantity_units);
        }

        // POST: Quantity_units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quantity_units quantity_units = db.Quantity_units.Find(id);
            db.Quantity_units.Remove(quantity_units);
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
