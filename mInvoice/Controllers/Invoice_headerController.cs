using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using mInvoice.App_GlobalResources;
using mInvoice.Models;
using MvcReportViewer;
using PagedList;



namespace mInvoice.Controllers
{
    public class Invoice_headerController : BaseController
    {
        private myinvoice_dbEntities m_db = new myinvoice_dbEntities();

        // GET: Invoice_header
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.invoice_noSortParm = sortOrder == "invoice_no" ? "invoice_no_desc" : "invoice_no";
            ViewBag.customers_idSortParm = sortOrder == "customers_id" ? "customers_id_desc" : "customers_id";
            ViewBag.customerSortParm = sortOrder == "customer" ? "customer_desc" : "customer";
            ViewBag.order_dateSortParm = sortOrder == "order_date" ? "order_date_desc" : "order_date";
            ViewBag.delivery_dateSortParm = sortOrder == "delivery_date" ? "delivery_date_desc" : "delivery_date";
            ViewBag.citySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.country_codeSortParm = sortOrder == "country_code" ? "country_code_desc" : "country_code";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int _clientsysid = Convert.ToInt32(Session["client_id"]);

            var _headers = from s in m_db.Invoice_header
                           where s.clients_id == _clientsysid
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                _headers = _headers.Where(s =>
                    s.invoice_no.Contains(searchString)
                 || s.Customers.customer_no.Contains(searchString)
                 || s.Customers.customer_name.Contains(searchString)
                 || s.order_date.ToString().Contains(searchString)
                 || s.delivery_date.ToString().Contains(searchString)
                 || s.city.ToString().Contains(searchString)
                 );
            }

            switch (sortOrder)
            {
                case "invoice_no_desc":
                    _headers = _headers.OrderByDescending(s => s.invoice_no);
                    break;
                case "customers_id_desc":
                    _headers = _headers.OrderByDescending(s => s.customers_id);
                    break;
                case "customer_desc":
                    _headers = _headers.OrderByDescending(s => s.Customers.customer_name);
                    break;
                case "order_date_desc":
                    _headers = _headers.OrderByDescending(s => s.order_date);
                    break;
                case "delivery_date_desc":
                    _headers = _headers.OrderByDescending(s => s.delivery_date);
                    break;
                case "city_desc":
                    _headers = _headers.OrderByDescending(s => s.city);
                    break;

                case "invoice_no":
                    _headers = _headers.OrderBy(s => s.invoice_no);
                    break;
                case "customer":
                    _headers = _headers.OrderBy(s => s.Customers.customer_name);
                    break;
                case "customers_id":
                    _headers = _headers.OrderBy(s => s.customers_id);
                    break;
                case "order_date":
                    _headers = _headers.OrderBy(s => s.order_date);
                    break;
                case "delivery_date":
                    _headers = _headers.OrderBy(s => s.delivery_date);
                    break;
                case "city":
                    _headers = _headers.OrderBy(s => s.city);
                    break;
                default:
                    //_countries = _countries.OrderBy(s => s.name);
                    _headers = _headers.OrderBy(s => s.invoice_no);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(_headers.ToPagedList(pageNumber, pageSize));

            //var invoice_header = db.Invoice_header.Include(i => i.Countries).Include(i => i.Customers);
            //return View(invoice_header.ToList());
        }

        // GET: Invoice_header/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }
            return View(invoice_header);
        }

        // GET: Invoice_header/Create
        public ActionResult Create()
        {
            int _client_id = -1;

            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _client_id = Convert.ToInt32(Session["client_id"]);
            ViewBag.clients_id = _client_id;

            DateTime _now = DateTime.Now;

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name");
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no");
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description");
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description");
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name");
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description");

            var _new_item = new Invoice_header();

            _new_item.clients_id = _client_id;
            _new_item.order_date = _now;
            _new_item.delivery_date = _now;

            return View(_new_item);
        }

        // POST: Invoice_header/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clients_id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt,quantity_2_column_name,quantity_3_column_name,discount,delivery_terms_id,payment_terms_id,paid_at,currency_id,tax_rate_id")] Invoice_header invoice_header)
        {
            int _client_id = -1;

            if (ModelState.IsValid)
            {
                if (Session["client_id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                _client_id = Convert.ToInt32(Session["client_id"]);

                invoice_header.invoice_no = getNewInvoiceNo(m_db);
                invoice_header.clients_id = _client_id;

                m_db.Invoice_header.Add(invoice_header);
                m_db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.customers_id);
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.customers_id);
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name", invoice_header.currency_id);
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.tax_rate_id);

            return View(invoice_header);
        }

        // GET: Invoice_header/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.payment_terms_id);
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.delivery_terms_id);
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name", invoice_header.currency_id);
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.tax_rate_id);

            return View(invoice_header);
        }

        // POST: Invoice_header/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clients_id,invoice_no,order_date,delivery_date,customers_id,customer_reference,countriesid,zip,city,street,CreatedAt,UpdatedAt,quantity_2_column_name,quantity_3_column_name,discount,delivery_terms_id,payment_terms_id,paid_at,currency_id,tax_rate_id")] Invoice_header invoice_header)
        {
            if (ModelState.IsValid)
            {
                m_db.Entry(invoice_header).State = EntityState.Modified;

                m_db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (invoice_header == null)
            {
                return HttpNotFound();
            }

            var _client_id = Convert.ToInt32(Session["client_id"]);

            ViewBag.countriesid = new SelectList(m_db.Countries.OrderByDescending(s => s.active), "Id", "name", invoice_header.countriesid);
            ViewBag.customers_id = new SelectList(m_db.Customers.Where(s => s.clientsysid == _client_id), "Id", "customer_no", invoice_header.customers_id);
            ViewBag.payment_terms_id = new SelectList(m_db.Payment_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.payment_terms_id);
            ViewBag.delivery_terms_id = new SelectList(m_db.Delivery_terms.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.delivery_terms_id);
            ViewBag.currency_id = new SelectList(m_db.Currency, "Id", "name", invoice_header.currency_id);
            ViewBag.tax_rate_id = new SelectList(m_db.Tax_rates.Where(s => s.clients_id == _client_id), "Id", "description", invoice_header.tax_rate_id);

            return View(invoice_header);
        }

        // GET: Invoice_header/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_header invoice_header = m_db.Invoice_header.Find(id);
            if (invoice_header == null)
            {
                return HttpNotFound();
            }
            return View(invoice_header);
        }

        // POST: Invoice_header/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            using (var _db = new myinvoice_dbEntities())
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        // Details
                        IQueryable<Invoice_details> _details =
                            m_db.Invoice_details.Where(x => x.invoice_header_id == id).Where(x => x.clients_id == _client_id);
                        m_db.Invoice_details.RemoveRange(_details);

                        // Header
                        Invoice_header invoice_header = m_db.Invoice_header.Find(id);
                        m_db.Invoice_header.Remove(invoice_header);

                        m_db.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError("", ex);
                        dbContextTransaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex);
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Copy_invoice(int? id)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice_header _invoice_header = m_db.Invoice_header.Find(id);

            Copy_Invoice _copy_Invoice = new Copy_Invoice();
            _copy_Invoice.invoice_header_id = (int)id;
            _copy_Invoice.quantity_2_column_name = _invoice_header.quantity_2_column_name;
            _copy_Invoice.quantity_3_column_name = _invoice_header.quantity_3_column_name;

            return View(_copy_Invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy_invoice(Copy_Invoice copy_invoice)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            int _new_invoice_header_id = -1;
            Invoice_header _new_header = new Invoice_header();

            if (ModelState.IsValid)
            {
                using (var _db = new myinvoice_dbEntities())
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Old header
                            Invoice_header _invoice_header = _db.Invoice_header.Find(copy_invoice.invoice_header_id);

                            // Old details
                            int _invoice_header_id = copy_invoice.invoice_header_id;
                            int _client_id = Convert.ToInt32(Session["client_id"]);

                            IQueryable<Invoice_details> invoice_details_arr =
                                from cust in _db.Invoice_details
                                where cust.invoice_header_id == _invoice_header_id &&
                                      cust.clients_id == _client_id
                                select cust;

                            //var result = invoice_details.Include(i => i.Articles).Include(i => i.Invoice_header).Include(i => i.Tax_rates);

                            // New header

                            _new_header = _invoice_header;

                            DateTime _now = DateTime.Now;
                            _new_header.order_date = _now;
                            _new_header.invoice_no = getNewInvoiceNo(_db);
                            _new_header.delivery_date = _now;
                            if (!string.IsNullOrEmpty(copy_invoice.quantity_2_column_name))
                                _new_header.quantity_2_column_name = copy_invoice.quantity_2_column_name;
                            if (!string.IsNullOrEmpty(copy_invoice.quantity_3_column_name))
                                _new_header.quantity_3_column_name = copy_invoice.quantity_3_column_name;

                            Invoice_header _inserted_header = _db.Invoice_header.Add(_new_header);

                            _db.SaveChanges();

                            _new_invoice_header_id = _inserted_header.Id;

                            // New details
                            foreach (Invoice_details _old_position in invoice_details_arr)
                            {
                                Invoice_details _new_position = new Invoice_details();

                                _new_position = _old_position;

                                _new_position.clients_id = _client_id;
                                _new_position.invoice_header_id = _new_invoice_header_id;
                                if (copy_invoice.quantity_2 >= 0)
                                    _new_position.quantity_2 = copy_invoice.quantity_2;
                                if (copy_invoice.quantity_3 >= 0)
                                    _new_position.quantity_3 = copy_invoice.quantity_3;

                                _db.Invoice_details.Add(_new_position);
                                _db.SaveChanges();
                            }

                            // Update Clients
                            Clients _client = _db.Clients.FirstOrDefault(x => x.Id == _client_id);
                            _client.last_index_invoices = _client.last_index_invoices + 1;
                            _db.Entry(_client).State = EntityState.Modified;
                            _db.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            ModelState.AddModelError("", ex);
                            dbContextTransaction.Rollback();
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex);
                            dbContextTransaction.Rollback();
                        }
                    }
                }
            }
            return RedirectToAction("Edit", new { id = _new_invoice_header_id });
        }

        private string getNewInvoiceNo(myinvoice_dbEntities db)
        {
            string _ret_val = null;
            string _prefix_invoices = null, _no_template_invoices = null;
            int _last_index_invoices = 1;
            int _client_id = Convert.ToInt32(Session["client_id"]);


            Clients _client = db.Clients.FirstOrDefault(x => x.Id == _client_id);

            _prefix_invoices = _client.prefix_invoices;
            _no_template_invoices = _client.no_template_invoices;
            _last_index_invoices = _client.last_index_invoices;

            string _prefix = !string.IsNullOrEmpty(_prefix_invoices) ? _prefix_invoices : "";
            string _year = DateTime.Today.Year.ToString();
            _year = _year.Substring(2, 2);
            string _number = getNumberStringByNumber(_last_index_invoices + 1);

            if (!string.IsNullOrEmpty(_no_template_invoices))
            {
                _ret_val = _no_template_invoices.Replace("%P", _prefix).Replace("%Y", _year).Replace("%N", _number);
            }
            else
            {
                _ret_val = _number;
            }

            return _ret_val;
        }

        private string getNumberStringByNumber(int Number)
        {
            string _ret_val = null;

            int _len = Number.ToString().Length;
            int _count_of_zeros = 6 - _len;

            _ret_val = new String('0', _count_of_zeros);
            _ret_val = _ret_val + Number;

            return _ret_val;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private string m_tmp_pdf_filename = null; //"invoice_" + Guid.NewGuid().ToString() + ".pdf";

        public ActionResult EmailForm(int? id, bool zugferd)
        {
            if (Session["client_id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice_header _invoice_header = m_db.Invoice_header.Find(id);
            EmailFormModel _email_form = new EmailFormModel();
            Clients _client = m_db.Clients.Find(Convert.ToInt32(Session["client_id"]));
            Customers _customer = m_db.Customers.Find(_invoice_header.customers_id);

            _email_form.ID = (int)id;
            _email_form.From = _client.email;
            _email_form.Subject = _client.email_subject.Replace("%1", _invoice_header.invoice_no);
            _email_form.Message = _client.email_message.Replace("%1", _invoice_header.invoice_no);
            _email_form.To = _customer.email;

            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            string _filename = "invoice_" + Guid.NewGuid().ToString() + ".pdf";
            string _file_path = Path.Combine(pathDownload, _filename);

            _email_form.Attachment = _file_path;

            return View(_email_form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> EmailForm(EmailFormModel EmailForm)
        {
            try
            {
                string _zugferd_file_path = null;
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection _connection = new SqlConnection(connectionString);

                if (ModelState.IsValid)
                {
                    if (Session["client_id"] == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    int _client_id = Convert.ToInt32(Session["client_id"]);

                    var model = GetData_invoice(EmailForm.ID);

                    Dictionary<string, System.Data.DataTable> _localReportDataSources =
                        new Dictionary<string, System.Data.DataTable>();

                    _localReportDataSources.Add("labels", model.labels);
                    _localReportDataSources.Add("data", model.data);

                    string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string pathDownload = Path.Combine(pathUser, "Downloads");

                    //string _filename = "invoice_" + Guid.NewGuid().ToString() + ".pdf";
                    //string _file_path = Path.Combine(pathDownload, PDFFileName);


                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress("dnepr65@gmail.com"); //EmailForm.From);
                    msg.To.Add(new MailAddress("dnepr65@gmail.com")); //EmailForm.To));
                    msg.Subject = EmailForm.Subject;
                    msg.Body = EmailForm.Message;

                    Stream stream = this.Report(
                        ReportFormat.Pdf
                        , "Reports/invoice.rdlc"
                        , localReportDataSources: _localReportDataSources
                        , mode: Microsoft.Reporting.WebForms.ProcessingMode.Local
                        , filename: EmailForm.Attachment
                        ).FileStream;

                    if (EmailForm.Zugferd)
                    {
                        string _pdf_tmp_file = HttpContext.Server.MapPath("~/App_Data/invoice.pdf");
                        string _pdf_tmp_file_zugferd = HttpContext.Server.MapPath("~/App_Data/invoice_zugferd.pdf");

                        if (System.IO.File.Exists(_pdf_tmp_file))
                            System.IO.File.Delete(_pdf_tmp_file);

                        byte[] buffer = new byte[20480]; 

                        using (System.IO.FileStream output = new FileStream(_pdf_tmp_file, FileMode.OpenOrCreate))
                        {
                            int readBytes = 0;
                            while ((readBytes = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, readBytes);
                            }
                        }

                        //ZUGFeRD_Test.main_form _zugferd = new ZUGFeRD_Test.main_form();
                        //_zugferd_file_path = _zugferd.getZugFeRD_PDF(
                        //    _pdf_tmp_file,
                        //    _connection,
                        //    _client_id,
                        //    //g_current_culture_sysid,
                        //    //m_document_number,
                        //    EmailForm.ID);
                    }
                    else
                    {
                        ContentType ct = new ContentType(MediaTypeNames.Text.Html);
                        Attachment att1 = new Attachment(stream, EmailForm.Attachment);
                        msg.Attachments.Add(att1);
                    }


                    var _client = m_db.Clients.Find(_client_id);

                    try
                    {
                        using (var smtp = new SmtpClient())
                        {
                            var credential = new NetworkCredential
                            {
                                UserName = _client.email_user_name,
                                Password = _client.email_password
                            };
                            smtp.Host = _client.email_host;
                            smtp.Port = (int)_client.email_port;
                            smtp.EnableSsl = true;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = credential;
                            smtp.Timeout = 20000;

                            await smtp.SendMailAsync(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                              ex.ToString());
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        private rp_invoice_detailsModel GetData_invoice(int id)
        {
            int _client_id = Convert.ToInt32(Session["client_id"]);

            Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable _labelsDataTable =
                new Reports.reportsDataSet.rp_invoice_detailsLabelsDataTable();

            _labelsDataTable.Addrp_invoice_detailsLabelsRow(
                                        Resource.invoice_no,
                        Resource.order_date,
                        Resource.delivery_date,
                        Resource.customer_reference,
                        Resource.description,
                        Resource.country_name,
                        Resource.customer_no,
                        Resource.customer,
                        Resource.email,
                        Resource.zip,
                        Resource.city,
                        Resource.street,
                        Resource.tax_rate,
                        Resource.client,
                        Resource.email,
                        Resource.owner,
                        Resource.street,
                        Resource.zip,
                        Resource.city,
                        Resource.phone,
                        Resource.fax,
                        "WWW",
                        Resource.ustd_id,
                        Resource.finance_office,
                        Resource.account_number,
                        Resource.bank_name,
                        Resource.iban,
                        Resource.bic,
                        Resource.picture,
                        Resource.phone_2,
                        Resource.description,
                        Resource.quantity,
                        Resource.quantity_2,
                        Resource.quantity_3,
                        Resource.price_netto,
                        Resource.discount,
                        Resource.article_no,
                        Resource.invoice,
                        Resource.date,
                        Resource.positions_short,
                        Resource.price,
                        Resource.sum_price,
                        Resource.quantity_units,
                        Resource.description,
                        Resource.net_amount,
                        Resource.subtotal,
                        Resource.plus_vat,
                        Resource.total_amount,
                        Resource.positions_short,
                        Resource.unit,
                        Resource.quantity_report,
                        Resource.discount_report,
                        Resource.phone_report,
                        Resource.fax_report,
                        Resource.tax_number,
                        Resource.bic_report,
                        Resource.iban_report,
                        Resource.owner_report,
                        Resource.delivery_date_report,
                        Resource.payment_terms,
                        Resource.delivery_terms
                        );

            var model = new mInvoice.Models.rp_invoice_detailsModel()
            {
                data = LocalData.GetInvoice(_client_id, id),
                labels = _labelsDataTable
            };
            return model;
        }


        public JsonResult getCustomer(int customer_id)
        {
            if (customer_id > 0)
            {
                Customers _customer = m_db.Customers.Find(customer_id);
                if (_customer != null)
                {                   
                    List<string> _res_arr = new List<string>();

                    _res_arr.Add(_customer.countriesid.ToString());
                    _res_arr.Add(_customer.zip.ToString());
                    _res_arr.Add(_customer.city.ToString());
                    _res_arr.Add(_customer.street.ToString());
                    _res_arr.Add(_customer.payment_terms_id.ToString());
                    _res_arr.Add(_customer.delivery_terms_id.ToString());
                    _res_arr.Add(_customer.tax_rate_id.ToString());
                    _res_arr.Add(_customer.currency_id.ToString());                    

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