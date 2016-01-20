using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;
using mInvoice.Models;

namespace mInvoice.Controllers
{
    public class ClientsController : BaseController
    {
        private myinvoice_dbEntities db = new myinvoice_dbEntities();

        // GET: Clients
        public ActionResult Index()
        {
            string _AspNetUsers_id = null;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client_id = Helpers.AccountHelper.getClientIDByUserName(
                Session["email"].ToString (), ref _AspNetUsers_id);

            //int _clientsysid = Convert.ToInt32(Session["client_id"]);

            IQueryable<Clients> _list = from cust in db.Clients
                                        where cust.Id == client_id
                                        select cust;

            var clients = _list.Include(c => c.Countries);
            return View(clients.ToList());
        }

        //[HttpPost]
        //public ActionResult FileUpload(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        string pic = System.IO.Path.GetFileName(file.FileName);
        //        string path = System.IO.Path.Combine(
        //                               Server.MapPath("~/images/profile"), pic);
        //        // file is uploaded
        //        //file.SaveAs(path);

        //        // save the image path path to the database or you can send image 
        //        // directly to database
        //        // in-case if you want to store byte[] ie. for DB
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            file.InputStream.CopyTo(ms);
        //            byte[] array = ms.GetBuffer();


        //        }

        //    }
        //    // after successfully uploading redirect the user
        //    //return RedirectToAction("actionname", "controller name");
        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            Clients  _new_obj = new Clients ();

            if (Session["email"] != null)
                _new_obj.email = Session["email"].ToString();

            ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name");
            return View(_new_obj);
        }

        // POST: Clients/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AspNetUsers_id,clientname,email,CreatedAt,UpdatedAt,owner,street,zip,city,countriesid,phone,fax,www,ustd_id,finance_office,account_number,blz,bank_name,iban,bic,picture")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                clients.AspNetUsers_id = Session["AspNetUsers_id"].ToString ();

                db.Clients.Add(clients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name", clients.countriesid);
            return View(clients);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name", clients.countriesid);
            return View(clients);
        }

        // POST: Clients/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([Bind(Include = "Id,AspNetUsers_id,clientname,email,CreatedAt,UpdatedAt,owner,street,zip,city,countriesid,phone,fax,www,ustd_id,finance_office,account_number,blz,bank_name,iban,bic,picture")] Clients clients, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    clients.picture = new byte[image.ContentLength];
                    image.InputStream.Read(clients.picture, 0, image.ContentLength);
                }

                clients.AspNetUsers_id = Session["AspNetUsers_id"].ToString ();

                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.countriesid = new SelectList(db.Countries.OrderByDescending(s => s.active), "Id", "name", clients.countriesid);
            return View(clients);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int _client_id = -1;
            myinvoice_dbEntities _db = new myinvoice_dbEntities();
            AspNetDataClassesDataContext _db_aspnet = new AspNetDataClassesDataContext();

            Clients clients = db.Clients.Find(id);

            try
            {


                if (Session["client_id"] != null)
                {
                    _client_id = Convert.ToInt32(Session["client_id"]);

                    var _invoice_details = from item in _db.Invoice_details
                                           where item.clients_id == _client_id
                                           select item;
                    if (_invoice_details != null)
                        _db.Invoice_details.RemoveRange(_invoice_details);


                    var _invoice_headers = from item in _db.Invoice_header
                                           where item.clients_id == _client_id
                                           select item;
                    if (_invoice_headers != null)
                        _db.Invoice_header.RemoveRange(_invoice_headers);


                    var _payment_methods = from item in _db.Payment_method
                                           where item.clients_id == _client_id
                                           select item;
                    if (_payment_methods != null)
                        _db.Payment_method.RemoveRange(_payment_methods);


                    var _tax_rates = from item in _db.Tax_rates
                                     where item.clients_id == _client_id
                                     select item;
                    if (_tax_rates != null)
                        _db.Tax_rates.RemoveRange(_tax_rates);


                    var _customers = from item in _db.Customers
                                     where item.clientsysid == _client_id
                                     select item;
                    if (_customers != null)
                        _db.Customers.RemoveRange(_customers);

                   
                    db.Clients.Remove(clients);

                    db.SaveChanges();

                    

                    // AspNet
                    var _logins = from item in _db_aspnet.AspNetUserLogins
                                  where item.UserId == Session["AspNetUsers_id"].ToString()
                                  select item;
                    if (_logins != null && _logins.Count() > 0)
                        _db_aspnet.AspNetUserLogins.DeleteAllOnSubmit(_logins);

                    var _claims = from item in _db_aspnet.AspNetUserClaims
                                  where item.UserId == Session["AspNetUsers_id"].ToString()
                                  select item;
                    if (_claims != null && _claims.Count() > 0)
                        _db_aspnet.AspNetUserClaims.DeleteAllOnSubmit(_claims);

                    var _roles = from item in _db_aspnet.AspNetUserRoles
                                 where item.UserId == Session["AspNetUsers_id"].ToString()
                                 select item;
                    if (_roles != null && _roles.Count() > 0)
                        _db_aspnet.AspNetUserRoles.DeleteAllOnSubmit(_roles);


                    var _users = from item in _db_aspnet.AspNetUsers
                                 where item.Id == Session["AspNetUsers_id"].ToString()
                                 select item;
                    if (_users != null && _users.Count() > 0)
                        _db_aspnet.AspNetUsers.DeleteAllOnSubmit(_users);


                   
                    _db_aspnet.SubmitChanges();

                    //FormsAuthentication.SignOut();

                    AuthenticationManager.SignOut();
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ModelState.AddModelError("", ex.Message);
                // return RedirectToAction("Index");
                return View(clients);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public static int? getClientIDByUserName(string UserName, ref string AspNetUsers_id)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                myinvoice_dbEntities _db = new myinvoice_dbEntities();

                string _AspNetUsers_id = _db.v_AspNetUsers.FirstOrDefault(x => x.UserName == UserName).Id;

                AspNetUsers_id = _AspNetUsers_id;

                if (_db.Clients.Count(p => p.AspNetUsers_id == _AspNetUsers_id) > 0)
                {
                    var clients_id = _db.Clients.FirstOrDefault(p => p.AspNetUsers_id == _AspNetUsers_id).Id;

                    return clients_id;
                }
                else
                {
                    // need create Customer
                    return -2;
                }
            }
            else
                return null;
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
