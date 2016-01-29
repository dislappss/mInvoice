using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class ArticlesController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Articles
        public ActionResult Index()
        {
            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            IQueryable<Articles> _list = from cust in db.Articles
                                              where cust.clients_id == _clientsysid
                                              select cust;


            var articles = _list.Include(a => a.Tax_rates); //.Include(a => a.quantity_units_id );

            return View(articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (//Session["email"] != null && 
                Session["client_id"] != null)
            {
                ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description");
                ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(s => s.clients_id == _client_id), "Id", "description");

                Articles _new_item = new Articles();

                _new_item.clients_id = _client_id;


                return View(_new_item);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Articles/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,article_no,price,description,tax_rate_id,quantity_units_id,CreatedAt,UpdatedAt")] Articles articles)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (ModelState.IsValid)
            {               
                articles.clients_id = Convert.ToInt32(Session["client_id"]); 

                db.Articles.Add(articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", articles.tax_rate_id);
            ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(s => s.clients_id == _client_id), "Id", "description", articles.quantity_units_id  );

            return View(articles);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", articles.tax_rate_id);
            ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(s => s.clients_id == _client_id), "Id", "description", articles.quantity_units_id);


            return View(articles);

        }

        // POST: Articles/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,article_no,price,description,tax_rate_id,quantity_units_id,CreatedAt,UpdatedAt")] Articles articles)
        {
            var _client_id = Convert.ToInt32(Session["client_id"]);

            if (ModelState.IsValid)
            {
                db.Entry(articles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", articles.tax_rate_id);
            ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(s => s.clients_id == _client_id), "Id", "description", articles.quantity_units_id);

            return View(articles);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
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
