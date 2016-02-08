using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mInvoice.Models;
using PagedList;

namespace mInvoice.Controllers
{
    public class CurrencyController : BaseController
    {
        /*         
         http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application 
         */

        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Currency
        public ActionResult Index(  string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodeSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Code" ? "code_desc" : "Code";
            ViewBag.ActiveSortParm = sortOrder == "Active" ? "active_desc" : "Active";

           //ViewBag.CurrentSort = sortOrder;
           //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

           if (searchString != null)
           {
              page = 1;
           }
           else
           {
              searchString = currentFilter;
           }

           ViewBag.CurrentFilter = searchString;


            var _Currency = from s in db.Currency
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                _Currency = _Currency.Where(s => s.name.Contains(searchString)
                                       || s.code.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    _Currency = _Currency.OrderByDescending(s => s.name );
                    break;
                case "Code":
                    _Currency = _Currency.OrderBy(s => s.code);
                    break;
                case "code_desc":
                    _Currency = _Currency.OrderByDescending(s => s.code);
                    break;
                //case "Active":
                //    _Currency = _Currency.OrderBy(s => s.active);
                //    break;
                //case "active_desc":
                //    _Currency = _Currency.OrderByDescending(s => s.active);
                //    break;
                default:
                    //_Currency = _Currency.OrderBy(s => s.name);
                    _Currency = _Currency.OrderByDescending(s => s.name );
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(_Currency.ToPagedList(pageNumber, pageSize));

            //return View(_Currency.ToList());
        }

        // GET: Currency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency Currency = db.Currency.Find(id);
            if (Currency == null)
            {
                return HttpNotFound();
            }
            return View(Currency);
        }

        // GET: Currency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,code,name,active")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                db.Currency.Add(currency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        // GET: Currency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency Currency = db.Currency.Find(id);
            if (Currency == null)
            {
                return HttpNotFound();
            }
            return View(Currency);
        }

        // POST: Currency/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,code,name,active")] Currency Currency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Currency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Currency);
        }

        // GET: Currency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency Currency = db.Currency.Find(id);
            if (Currency == null)
            {
                return HttpNotFound();
            }
            return View(Currency);
        }

        // POST: Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Currency Currency = db.Currency.Find(id);
            db.Currency.Remove(Currency);
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
