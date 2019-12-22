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
    [Authorize]
    public class CourseController : Controller
    {
        private tuananhEntities db = new tuananhEntities();
        [HttpGet]
        [Authorize(Roles = "Training Staff,Trainee,Trainer")]
        // GET: Course
        public ActionResult Index(string searchBy,string search)
        {
            if(searchBy == "Description")
            {
                return View(db.Courses.Where(x => x.Description == search || search == null).ToList());
            }
            else
            {
                return View(db.Courses.Where(x => x.CourseName.StartsWith(search) || search == null).ToList());
            }
        }
        [HttpGet]
        [Authorize(Roles = "Training Staff")]
        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles = "Training Staff")]
        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.CourseCategoryID = new SelectList(db.CourceCategories, "CourseCategoryID", "CourseCategoryName");
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Staff")]
        public ActionResult Create([Bind(Include = "CourseID,CourseCategoryID,CourseName,Description")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseCategoryID = new SelectList(db.CourceCategories, "CourseCategoryID", "CourseCategoryName", course.CourseCategoryID);
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles = "Training Staff")]
        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseCategoryID = new SelectList(db.CourceCategories, "CourseCategoryID", "CourseCategoryName", course.CourseCategoryID);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Staff")]
        public ActionResult Edit([Bind(Include = "CourseID,CourseCategoryID,CourseName,Description")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseCategoryID = new SelectList(db.CourceCategories, "CourseCategoryID", "CourseCategoryName", course.CourseCategoryID);
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles = "Training Staff")]
        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
