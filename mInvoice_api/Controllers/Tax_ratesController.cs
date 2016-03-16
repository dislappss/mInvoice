using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using mInvoice_api.Models;

namespace mInvoice_api.Controllers
{
    public class Tax_ratesController : ApiController
    {
        private Entities db = new Entities ();

        // GET: api/Tax_rates
        public IQueryable<Tax_rates> GetTax_rates(int client_id)
        {
            var _list = from cust in db.Tax_rates
                        where cust.clients_id == client_id
                        select cust;

            return _list;
        }

        // GET: api/Tax_rates/5
        [ResponseType(typeof(Tax_rates))]
        public IHttpActionResult GetTax_ratesByID(int id)
        {
            Tax_rates tax_rates = db.Tax_rates.Find(id);
            if (tax_rates == null)
            {
                return NotFound();
            }

            return Ok(tax_rates);
        }

        // PUT: api/Tax_rates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTax_rates(int id, Tax_rates tax_rates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tax_rates.Id)
            {
                return BadRequest();
            }

            db.Entry(tax_rates).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tax_ratesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tax_rates
        [ResponseType(typeof(Tax_rates))]
        public IHttpActionResult PostTax_rates(Tax_rates tax_rates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tax_rates.Add(tax_rates);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tax_rates.Id }, tax_rates);
        }

        // DELETE: api/Tax_rates/5
        [ResponseType(typeof(Tax_rates))]
        public IHttpActionResult DeleteTax_rates(int id)
        {
            Tax_rates tax_rates = db.Tax_rates.Find(id);
            if (tax_rates == null)
            {
                return NotFound();
            }

            db.Tax_rates.Remove(tax_rates);
            db.SaveChanges();

            return Ok(tax_rates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tax_ratesExists(int id)
        {
            return db.Tax_rates.Count(e => e.Id == id) > 0;
        }
    }
}