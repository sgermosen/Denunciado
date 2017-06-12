using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Backend.Models;
using Domain;

namespace Backend.Controllers
{
    public class ComplaintsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Complaints
        public async Task<ActionResult> Index()
        {
            var complaints = db.Complaints.Include(c => c.ComplaintType);
            return View(await complaints.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Code");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ComplaintId,UserId,CreationDate,Address,Description,Image,IsActive,Lat,Lon,ComplaintTypeId")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Code", complaint.ComplaintTypeId);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Code", complaint.ComplaintTypeId);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ComplaintId,UserId,CreationDate,Address,Description,Image,IsActive,Lat,Lon,ComplaintTypeId")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Code", complaint.ComplaintTypeId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Complaint complaint = await db.Complaints.FindAsync(id);
            db.Complaints.Remove(complaint);
            await db.SaveChangesAsync();
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
