using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class Invoice_detailsController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Invoice_details
        //public ActionResult Index()
        //{
        //    var invoice_details = db.Invoice_details.Include(i => i.Articles).Include(i => i.Invoice_header).Include(i => i.Tax_rates);
        //    return View(invoice_details.ToList());
        //}

        public ActionResult Index(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["invoice_header_id"] = null;

            if (id >= 0)
            {
                Session["invoice_header_id"] = id;

                var result = GetData();

                return View(result.ToList());
            }
            else
            {
                var invoice_details = db.Invoice_details.Include(i => i.Articles).Include(i => i.Tax_rates).Include(i => i.Quantity_units);

                return View(invoice_details.ToList());
            }
        }

        private IQueryable<Invoice_details> GetData()
        {
            int _invoice_header_id = (int)Session["invoice_header_id"];
            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            var invoice_details =
                from cust in db.Invoice_details
                where cust.invoice_header_id == _invoice_header_id &&
                      cust.clients_id == _clientsysid
                //orderby cust.Name ascending
                select cust;
            var result = invoice_details.Include(i => i.Articles).Include(i => i.Tax_rates).Include(i => i.Quantity_units);
            return result;
        }

        // GET: Invoice_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_details invoice_details = db.Invoice_details.Find(id);
            if (invoice_details == null)
            {
                return HttpNotFound();
            }
            return View(invoice_details);
        }

        // GET: Invoice_details/Create
        public ActionResult Create()
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.clients_id = _client_id;
            ViewBag.article_id = new SelectList(db.Articles.Where (x=>x.clients_id == _client_id ), "Id", "article_no");
            ViewBag.invoice_header_id = Session["invoice_header_id"]; // new SelectList(db.Invoice_header, "Id", "invoice_no");
            ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(x => x.clients_id == _client_id), "Id", "description");
            ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(x => x.clients_id == _client_id), "Id", "description");

            var _new_item = new Invoice_details();
            _new_item.clients_id = _client_id;
            _new_item.invoice_header_id = Convert.ToInt32(Session["invoice_header_id"]);
            return View(_new_item);
        }

        // POST: Invoice_details/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,invoice_header_id,article_id,description,tax_rate_id,quantity_units_id,quantity,quantity_2,quantity_3,price_netto,discount,cash_discount")] Invoice_details invoice_details)
        {
            int _client_id = -1;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _client_id = Convert.ToInt32(Session["client_id"]);

            invoice_details.invoice_header_id = Convert.ToInt32(Session["invoice_header_id"]);
            invoice_details.clients_id = _client_id;

            if (ModelState.IsValid)
            {
                db.Invoice_details.Add(invoice_details);
                db.SaveChanges();

                var result = GetData();

                return RedirectToAction("Index", new { @id = Session["invoice_header_id"] });
            }
            else
            {
                ViewBag.article_id = new SelectList(db.Articles.Where(x => x.clients_id == _client_id), "Id", "article_no", invoice_details.article_id);
                ViewBag.invoice_header_id = new SelectList(db.Invoice_header, "Id", "invoice_no", invoice_details.invoice_header_id);
                ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(x => x.clients_id == _client_id), "Id", "description", invoice_details.tax_rate_id);
                ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(x => x.clients_id == _client_id), "Id", "description", invoice_details.quantity_units_id);
                
                return View(invoice_details);

            }
        }

        // GET: Invoice_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_details invoice_details = db.Invoice_details.Find(id);
            if (invoice_details == null)
            {
                return HttpNotFound();
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.article_id = new SelectList(db.Articles.Where(x => x.clients_id == _client_id), "Id", "article_no", invoice_details.article_id);
            ViewBag.invoice_header_id = new SelectList(db.Invoice_header, "Id", "invoice_no", invoice_details.invoice_header_id);
            ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(x => x.clients_id == _client_id), "Id", "description", invoice_details.tax_rate_id);
            ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(x => x.clients_id == _client_id), "Id", "description", invoice_details.quantity_units_id);

            return View(invoice_details);
        }

        // POST: Invoice_details/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,invoice_header_id,article_id,description,tax_rate_id,quantity_units_id,quantity,quantity_2,quantity_3,price_netto,discount,cash_discount,CreatedAt,UpdatedAt")] Invoice_details invoice_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["invoice_header_id"] });
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.article_id = new SelectList(db.Articles.Where(x => x.clients_id == _client_id), "Id", "article_no", invoice_details.article_id);
            ViewBag.invoice_header_id = new SelectList(db.Invoice_header, "Id", "invoice_no", invoice_details.invoice_header_id);
            ViewBag.tax_rate_id = new SelectList(db.Tax_rates.Where(x => x.clients_id == _client_id), "Id", "description", invoice_details.tax_rate_id);
            ViewBag.quantity_units_id = new SelectList(db.Quantity_units.Where(x => x.clients_id == _client_id), "Id", "description", invoice_details.quantity_units_id);

            return View(invoice_details);
        }

        // GET: Invoice_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_details invoice_details = db.Invoice_details.Find(id);
            if (invoice_details == null)
            {
                return HttpNotFound();
            }
            return View(invoice_details);
        }

        // POST: Invoice_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice_details invoice_details = db.Invoice_details.Find(id);
            db.Invoice_details.Remove(invoice_details);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["invoice_header_id"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult getPrice(string article_id)
        {
            if (!string.IsNullOrEmpty(article_id))
            {
                Articles articles = db.Articles.Find(Convert.ToInt32(article_id));
                if (articles != null)
                {
                    string result = articles.price.ToString();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public JsonResult getTax_rate_id(string article_id)
        {
            if (!string.IsNullOrEmpty(article_id))
            {
                Articles articles = db.Articles.Find(Convert.ToInt32(article_id));
                if (articles != null)
                {
                    string result = articles.tax_rate_id.ToString();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public JsonResult getArticleDescription(string article_id)
        {
            if (!string.IsNullOrEmpty(article_id))
            {
                Articles articles = db.Articles.Find(Convert.ToInt32(article_id));
                if (articles != null)
                {
                    string result = articles.description;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public JsonResult getQuantity_unit(string article_id)
        {
            if (!string.IsNullOrEmpty(article_id))
            {
                Articles articles = db.Articles.Find(Convert.ToInt32(article_id));
                if (articles != null)
                {
                    string result = articles.quantity_units_id.ToString();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public JsonResult getArticle(int id)
        {
            if (id > 0)
            {
                Articles _item = db.Articles.Find(id);
                if (_item != null)
                {
                    List<string> _res_arr = new List<string>();

                    _res_arr.Add(_item.quantity_units_id.ToString());
                    _res_arr.Add(_item.description.ToString());
                    _res_arr.Add(_item.tax_rate_id.ToString());
                    _res_arr.Add(_item.price.ToString());

                    JsonResult _result = Json(_res_arr, JsonRequestBehavior.AllowGet);

                    return _result;
                }
                else
                    return null;
            }
            else
                return null;
        }
       
    }
}
