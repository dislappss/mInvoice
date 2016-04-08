using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class CostsController : BaseController
    {
        private myinvoice_dbEntities m_db = new myinvoice_dbEntities();

        // GET: Costs
        public ActionResult Index()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            var costs = m_db.Costs.Include(c => c.Clients).Include(c => c.Type_of_costs).Include(c => c.Articles).Include(c => c.Customers);

            return View(costs.ToList());
    
        }

        // GET: Costs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costs costs = m_db.Costs.Find(id);
            if (costs == null)
            {
                return HttpNotFound();
            }
            return View(costs);
        }

        // GET: Costs/Create
        public ActionResult Create()
        {
            int _clients_id = -1;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _clients_id = Convert.ToInt32(Session["client_id"]);

            DateTime _now = DateTime.Now;

            ViewBag.clients_id = _clients_id;
            //ViewBag.clients_id = new SelectList(m_db.Clients, "Id", "clientname");
            ViewBag.type_of_costs_id = new SelectList(m_db.Type_of_costs.Where(x => x.clients_id == _clients_id), "Id", "description");
            ViewBag.articles_id = new SelectList(m_db.Articles.Where(x => x.clients_id == _clients_id), "Id", "article_no");
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(x => x.clientsysid == _clients_id), "Id", "customer_no");

            Costs _item = new Costs();

            _item.clients_id = _clients_id ;
            _item.booking_date  = _now;

            return View(_item);          
        }

        // POST: Costs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,description,type_of_costs_id,articles_id,customers_id,booking_date,value,CreatedAt,UpdatedAt")] Costs costs)
        {
            int _clients_id = -3;

            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                _clients_id =  Convert.ToInt32(Session["client_id"]);

                costs.clients_id = _clients_id;

                m_db.Costs.Add(costs);
                m_db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.clients_id = new SelectList(m_db.Clients, "Id", "AspNetUsers_id", item.clients_id);
            ViewBag.type_of_costs_id = new SelectList(m_db.Type_of_costs.Where(x => x.clients_id == _clients_id), "Id", "description", costs.type_of_costs_id);
            ViewBag.articles_id = new SelectList(m_db.Articles.Where(x => x.clients_id == _clients_id), "Id", "article_no", costs.articles_id);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(x => x.clientsysid == _clients_id), "Id", "customer_no", costs.customers_id);

            return View(costs);           
        }

        // GET: Costs/Edit/5
        public ActionResult Edit(int? id)
        {
            int _clients_id = -3;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _clients_id = Convert.ToInt32(Session["client_id"]);

            Costs costs = m_db.Costs.Find(id);
            if (costs == null)
            {
                return HttpNotFound();
            }
            ViewBag.clients_id = _clients_id;
            ViewBag.type_of_costs_id = new SelectList(m_db.Type_of_costs.Where(x => x.clients_id == _clients_id), "Id", "description", costs.type_of_costs_id);
            ViewBag.articles_id = new SelectList(m_db.Articles.Where(x => x.clients_id == _clients_id), "Id", "article_no", costs.articles_id);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(x => x.clientsysid == _clients_id), "Id", "customer_no", costs.customers_id);
            return View(costs);
        }

        // POST: Costs/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,description,type_of_costs_id,articles_id,customers_id,booking_date,value,CreatedAt,UpdatedAt")] Costs costs)
        {
            int _clients_id = -3;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _clients_id = Convert.ToInt32(Session["client_id"]);

            if (ModelState.IsValid)
            {
                m_db.Entry(costs).State = EntityState.Modified;
                m_db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clients_id = _clients_id;
            ViewBag.type_of_costs_id = new SelectList(m_db.Type_of_costs.Where(x => x.clients_id == _clients_id), "Id", "description", costs.type_of_costs_id);
            ViewBag.articles_id = new SelectList(m_db.Articles.Where(x => x.clients_id == _clients_id), "Id", "article_no", costs.articles_id);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(x => x.clientsysid == _clients_id), "Id", "customer_no", costs.customers_id);
            return View(costs);
        }

        // GET: Costs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costs costs = m_db.Costs.Find(id);
            if (costs == null)
            {
                return HttpNotFound();
            }
            return View(costs);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Costs costs = m_db.Costs.Find(id);
            m_db.Costs.Remove(costs);
            m_db.SaveChanges();
            return RedirectToAction("Index");
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
