using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Backend.Models;
using Domain;

namespace Backend.Controllers
{
    [Authorize]
    public class ComplaintsTypesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ComplaintsTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ComplaintsTypes.ToListAsync());
        }

        // GET: ComplaintsTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintsType complaintsType = await db.ComplaintsTypes.FindAsync(id);
            if (complaintsType == null)
            {
                return HttpNotFound();
            }
            return View(complaintsType);
        }

        // GET: ComplaintsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComplaintsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ComplaintTypeId,Code,Description")] ComplaintsType complaintsType)
        {
            if (ModelState.IsValid)
            {
                db.ComplaintsTypes.Add(complaintsType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(complaintsType);
        }

        // GET: ComplaintsTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintsType complaintsType = await db.ComplaintsTypes.FindAsync(id);
            if (complaintsType == null)
            {
                return HttpNotFound();
            }
            return View(complaintsType);
        }

        // POST: ComplaintsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ComplaintTypeId,Code,Description")] ComplaintsType complaintsType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaintsType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(complaintsType);
        }

        // GET: ComplaintsTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintsType complaintsType = await db.ComplaintsTypes.FindAsync(id);
            if (complaintsType == null)
            {
                return HttpNotFound();
            }
            return View(complaintsType);
        }

        // POST: ComplaintsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ComplaintsType complaintsType = await db.ComplaintsTypes.FindAsync(id);
            db.ComplaintsTypes.Remove(complaintsType);
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
