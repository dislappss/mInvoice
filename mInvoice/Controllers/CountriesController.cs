using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mInvoice.Models;
using PagedList;

namespace mInvoice.Controllers
{
    public class CountriesController : Controller
    {
        private myinvoice_dbEntities3 db = new myinvoice_dbEntities3();

        // GET: Countries
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


            var _countries = from s in db.Countries
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                _countries = _countries.Where(s => s.name.Contains(searchString)
                                       || s.code.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    _countries = _countries.OrderByDescending(s => s.name );
                    break;
                case "Code":
                    _countries = _countries.OrderBy(s => s.code);
                    break;
                case "code_desc":
                    _countries = _countries.OrderByDescending(s => s.code);
                    break;
                case "Active":
                    _countries = _countries.OrderBy(s => s.active);
                    break;
                case "active_desc":
                    _countries = _countries.OrderByDescending(s => s.active);
                    break;
                default:
                    //_countries = _countries.OrderBy(s => s.name);
                    _countries = _countries.OrderByDescending(s => s.active);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(_countries.ToPagedList(pageNumber, pageSize));

            //return View(_countries.ToList());
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,code,name,active")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(countries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(countries);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: Countries/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,code,name,active")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countries);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Countries countries = db.Countries.Find(id);
            db.Countries.Remove(countries);
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
