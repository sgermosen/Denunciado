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
using DenunciadoBackEnd.Models;

namespace DenunciadoBackEnd.Controllers.API
{
    public class ComplaintsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Complaints
        public IQueryable<Complaint> GetComplaints()
        {
            return db.Complaints;
        }

        // GET: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult GetComplaint(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        // PUT: api/Complaints/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComplaint(int id, Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complaint.ComplaintId)
            {
                return BadRequest();
            }

            db.Entry(complaint).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintExists(id))
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

        // POST: api/Complaints
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult PostComplaint(Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complaints.Add(complaint);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complaint.ComplaintId }, complaint);
        }

        // DELETE: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult DeleteComplaint(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return NotFound();
            }

            db.Complaints.Remove(complaint);
            db.SaveChanges();

            return Ok(complaint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintExists(int id)
        {
            return db.Complaints.Count(e => e.ComplaintId == id) > 0;
        }
    }
}