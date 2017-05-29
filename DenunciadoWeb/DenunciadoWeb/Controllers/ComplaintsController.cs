using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DenunciadoBackEnd.Models;
using DenunciadoBackEnd.Classes;

namespace DenunciadoBackEnd.Controllers
{
    public class ComplaintsController : Controller
    {
        private DataContext db ; //= new DataContext();

        public ComplaintsController()
        {
            db = new DataContext();
        }


        public ActionResult Index()
        {
            //return View(db.Complaints.OrderBy(f => f.Description).ToList());
            var complaints = db.Complaints.Include(c => c.ComplaintType);
            return View(complaints.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

       
        public ActionResult Create()
        {
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "ComplaintId,UserId,CreationDate,Address,Description,Image,IsActive,Lat,Lon,ComplaintTypeId")] Complaint complaint)
        public ActionResult Create(ComplaintView view)
        {
            if (ModelState.IsValid)
            {

                //db.Complaints.Add(complaint);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var complaint = ToComplaint(view);
                complaint.Image = pic;
                //db.Flowers.Add(flower);
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description", view.ComplaintTypeId);
            return View(view);//  complaint);
        }
        private Complaint ToComplaint(ComplaintView view)
        {
            return new Models.Complaint
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

        // GET: Complaints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var complaint = db.Complaints.Find(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description", complaint.ComplaintTypeId);
            return View(ToView(complaint));
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

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Edit([Bind(Include = "ComplaintId,UserId,CreationDate,Address,Description,Image,IsActive,Lat,Lon,ComplaintTypeId")] Complaint complaint)
        public ActionResult Edit(ComplaintView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image; //la inicializo en la imagen vieja por si el no la cambio no la dañe
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var flower = ToComplaint(view); //convertimos a vista
                flower.Image = pic;
                db.Entry(flower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                //db.Entry(complaint).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description", view.ComplaintTypeId);
           // return View(complaint);
            return View(view);
        }

        // GET: Complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
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
