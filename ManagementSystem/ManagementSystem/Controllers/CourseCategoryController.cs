using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagementSystem.Models;

namespace ManagementSystem.Controllers
{
    [Authorize(Roles = "Training Staff")]
    public class CourseCategoryController : Controller
    {
        private tuananhEntities db = new tuananhEntities();

        // GET: CourseCategory
        public ActionResult Index()
        {
            return View(db.CourceCategories.ToList());
        }

        // GET: CourseCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourceCategory courceCategory = db.CourceCategories.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        // GET: CourseCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseCategoryID,CourseCategoryName,Description")] CourceCategory courceCategory)
        {
            if (ModelState.IsValid)
            {
                db.CourceCategories.Add(courceCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courceCategory);
        }

        // GET: CourseCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourceCategory courceCategory = db.CourceCategories.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        // POST: CourseCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseCategoryID,CourseCategoryName,Description")] CourceCategory courceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courceCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courceCategory);
        }

        // GET: CourseCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourceCategory courceCategory = db.CourceCategories.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        // POST: CourseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourceCategory courceCategory = db.CourceCategories.Find(id);
            db.CourceCategories.Remove(courceCategory);
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
