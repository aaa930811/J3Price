using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using J3Price.DAL;
using J3PriceModels;

namespace J3Price.Controllers
{
    public class QuotesController : ApiController
    {
        private J3PriceEntities db = new J3PriceEntities();
        DALQuiz DAL = new DALQuiz();

        public IEnumerable<QuotesShowModel> GetQuotes([FromUri]QuotesQueryModel model)
        {
            var list= DAL.GetQuotesList(model);
            List<QuotesShowModel> qsmlist = new List<QuotesShowModel>();
            foreach (var item in list)
            {
                QuotesShowModel qsm = new QuotesShowModel();
                qsm.ID = item.ID;
                qsm.RegionName = item.RegionName;
                qsm.ServiceName = item.ServiceName;
                qsm.SaleTypeName = item.SaleTypeName;
                qsm.ProductName = item.ProductName;
                qsm.ProducPrice = item.ProducPrice;
                qsm.DealTime = item.DealTime.Value.ToShortDateString();
                qsm.Bidder = item.Bidder;
                qsm.QuotationTime = item.QuotationTime.Value.ToShortDateString();
                qsmlist.Add(qsm);
            }
            return qsmlist;
        }
        // GET: api/Quotes
        //public IQueryable<Quotes> GetQuotes()
        //{
        //    return db.Quotes;
        //}

        // GET: api/Quotes/5
        //[ResponseType(typeof(Quotes))]
        //public async Task<IHttpActionResult> GetQuotes(int id)
        //{
        //    Quotes quotes = await db.Quotes.FindAsync(id);
        //    if (quotes == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(quotes);
        //}

        // PUT: api/Quotes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuotes(int id, Quotes quotes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quotes.ID)
            {
                return BadRequest();
            }

            db.Entry(quotes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotesExists(id))
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

        // POST: api/Quotes
        [ResponseType(typeof(Quotes))]
        public async Task<IHttpActionResult> PostQuotes(Quotes quotes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quotes.Add(quotes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = quotes.ID }, quotes);
        }

        // DELETE: api/Quotes/5
        [ResponseType(typeof(Quotes))]
        public async Task<IHttpActionResult> DeleteQuotes(int id)
        {
            Quotes quotes = await db.Quotes.FindAsync(id);
            if (quotes == null)
            {
                return NotFound();
            }

            db.Quotes.Remove(quotes);
            await db.SaveChangesAsync();

            return Ok(quotes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuotesExists(int id)
        {
            return db.Quotes.Count(e => e.ID == id) > 0;
        }
    }
}