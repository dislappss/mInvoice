using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class Type_of_costsController : Controller
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Type_of_costs
        public ActionResult Index()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);


            // var _list = _controller.GetTax_rates(_clientsysid);

            var _list = from cust in db.Type_of_costs
                        where cust.clients_id == _clientsysid
                        select cust;

            return View(_list.ToList());
        }

        // GET: Type_of_costs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Type_of_costs type_of_costs = db.Type_of_costs.Find(id);
        //    if (type_of_costs == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(type_of_costs);
        //}

        // GET: Type_of_costs/Create
        public ActionResult Create()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.clientsysid = Convert.ToInt32(Session["client_id"]); 

            Type_of_costs _item = new Type_of_costs();
            _item.clients_id = Convert.ToInt32(Session["client_id"]);

            return View(_item);
        }

        // POST: Type_of_costs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,description,shortmark,CreatedAt,UpdatedAt")] Type_of_costs type_of_costs)
        {
            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                type_of_costs.clients_id = Convert.ToInt32(Session["client_id"]);

                db.Type_of_costs.Add(type_of_costs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(type_of_costs);
        }

        // GET: Type_of_costs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_of_costs _item = db.Type_of_costs.Find(id);
            if (_item == null)
            {
                return HttpNotFound();
            }
            return View(_item);
        }

        // POST: Type_of_costs/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,description,shortmark,CreatedAt,UpdatedAt")] Type_of_costs type_of_costs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type_of_costs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clients_id = new SelectList(db.Clients, "Id", "AspNetUsers_id", type_of_costs.clients_id);
            return View(type_of_costs);
        }

        // GET: Type_of_costs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_of_costs type_of_costs = db.Type_of_costs.Find(id);
            if (type_of_costs == null)
            {
                return HttpNotFound();
            }
            return View(type_of_costs);
        }

        // POST: Type_of_costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Type_of_costs type_of_costs = db.Type_of_costs.Find(id);
            db.Type_of_costs.Remove(type_of_costs);
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
