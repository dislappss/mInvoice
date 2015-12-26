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
    public class Payment_methodController : Controller
    {
        private myinvoice_dbEntities3 db = new myinvoice_dbEntities3();

        // GET: Payment_method
        public ActionResult Index()
        {
            return View(db.Payment_method.ToList());
        }

        // GET: Payment_method/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_method payment_method = db.Payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // GET: Payment_method/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment_method/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,code,CreatedAt,UpdatedAt")] Payment_method payment_method)
        {
            if (ModelState.IsValid)
            {
                db.Payment_method.Add(payment_method);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment_method);
        }

        // GET: Payment_method/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_method payment_method = db.Payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // POST: Payment_method/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,code,CreatedAt,UpdatedAt")] Payment_method payment_method)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_method).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment_method);
        }

        // GET: Payment_method/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_method payment_method = db.Payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // POST: Payment_method/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_method payment_method = db.Payment_method.Find(id);
            db.Payment_method.Remove(payment_method);
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
