using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Backend.Helpers;
using Backend.Models;
using Domain;
 

namespace Backend.Controllers
{
    public class ComplaintsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

       
        public async Task<ActionResult> Index()
        {
            var complaints = db.Complaints.Include(c => c.ComplaintType);
            return View(await complaints.ToListAsync());
        }

       
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var complaint = await db.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }

            return View(complaint);
        }

       
        public ActionResult Create()
        {
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Description");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ComplaintView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.PhotoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PhotoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var complaint = ToComplaint(view);
                complaint.Image = pic;
                db.Complaints.Add(complaint);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Description", view.ComplaintTypeId);
            return View(view);
        }

        private Complaint ToComplaint(ComplaintView view)
        {
            return new Complaint
            {
                ComplaintId = view.ComplaintId,
                UserId = view.UserId,
                CreationDate = view.CreationDate,
                Address = view.Address,
                Description = view.Description,
                Image = view.Image,
                IsActive = view.IsActive,
                Lat = view.Lat,
                Lon = view.Lon,
                ComplaintTypeId = view.ComplaintTypeId,
            };
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var complaint = await db.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }

            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Description", complaint.ComplaintTypeId);
            var view = ToView  (complaint);
            return View(view);
        }
        private ComplaintView ToView(Complaint complaint)
        {
            return new ComplaintView
            {
                ComplaintId = complaint.ComplaintId,
                UserId = complaint.UserId,
                CreationDate = complaint.CreationDate,
                Address = complaint.Address,
                Description = complaint.Description,
                Image = complaint.Image,
                IsActive = complaint.IsActive,
                Lat = complaint.Lat,
                Lon = complaint.Lon,
                ComplaintTypeId = complaint.ComplaintTypeId,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( ComplaintView view)
        {
            if (ModelState.IsValid)
            {

                var pic = view.Image; //la inicializo en la imagen vieja por si el no la cambio no la dañe
                var folder = "~/Content/Images";

                if (view.PhotoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PhotoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var complaint = ToComplaint(view); 
                complaint.Image = pic;
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

                //db.Entry(complaint).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintsTypes, "ComplaintTypeId", "Description", view.ComplaintTypeId);
            return View(view);
        }

       
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
