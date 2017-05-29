using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DenunciadoBackEnd.Models;

namespace DenunciadoBackEnd.Controllers
{
    public class ComplaintTypesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: ComplaintTypes
        public ActionResult Index()
        {
            return View(db.ComplaintTypes.ToList());
        }

        // GET: ComplaintTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintType complaintType = db.ComplaintTypes.Find(id);
            if (complaintType == null)
            {
                return HttpNotFound();
            }
            return View(complaintType);
        }

        // GET: ComplaintTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComplaintTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplaintTypeId,Code,Description")] ComplaintType complaintType)
        {
            if (ModelState.IsValid)
            {
                db.ComplaintTypes.Add(complaintType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaintType);
        }

        // GET: ComplaintTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintType complaintType = db.ComplaintTypes.Find(id);
            if (complaintType == null)
            {
                return HttpNotFound();
            }
            return View(complaintType);
        }

        // POST: ComplaintTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplaintTypeId,Code,Description")] ComplaintType complaintType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaintType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(complaintType);
        }

        // GET: ComplaintTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintType complaintType = db.ComplaintTypes.Find(id);
            if (complaintType == null)
            {
                return HttpNotFound();
            }
            return View(complaintType);
        }

        // POST: ComplaintTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComplaintType complaintType = db.ComplaintTypes.Find(id);
            db.ComplaintTypes.Remove(complaintType);
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
