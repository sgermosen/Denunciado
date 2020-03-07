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
using Domain;

namespace API.Controllers
{
    [Authorize]
    public class ComplaintsTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ComplaintsTypes
        public IQueryable<ComplaintsType> GetComplaintsTypes()
        {
            return db.ComplaintsTypes;
        }

        // GET: api/ComplaintsTypes/5
        [ResponseType(typeof(ComplaintsType))]
        public async Task<IHttpActionResult> GetComplaintsType(int id)
        {
            ComplaintsType complaintsType = await db.ComplaintsTypes.FindAsync(id);
            if (complaintsType == null)
            {
                return NotFound();
            }

            return Ok(complaintsType);
        }

        // PUT: api/ComplaintsTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComplaintsType(int id, ComplaintsType complaintsType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complaintsType.ComplaintTypeId)
            {
                return BadRequest();
            }

            db.Entry(complaintsType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintsTypeExists(id))
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

        // POST: api/ComplaintsTypes
        [ResponseType(typeof(ComplaintsType))]
        public async Task<IHttpActionResult> PostComplaintsType(ComplaintsType complaintsType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComplaintsTypes.Add(complaintsType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = complaintsType.ComplaintTypeId }, complaintsType);
        }

        // DELETE: api/ComplaintsTypes/5
        [ResponseType(typeof(ComplaintsType))]
        public async Task<IHttpActionResult> DeleteComplaintsType(int id)
        {
            ComplaintsType complaintsType = await db.ComplaintsTypes.FindAsync(id);
            if (complaintsType == null)
            {
                return NotFound();
            }

            db.ComplaintsTypes.Remove(complaintsType);
            await db.SaveChangesAsync();

            return Ok(complaintsType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintsTypeExists(int id)
        {
            return db.ComplaintsTypes.Count(e => e.ComplaintTypeId == id) > 0;
        }
    }
}