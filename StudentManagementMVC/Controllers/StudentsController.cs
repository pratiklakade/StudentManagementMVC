using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StudentManagementMVC.Models;

namespace StudentManagementMVC.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        private List<string> GetCourseList()
        {
            return new List<string> { "Computer Science", "IT", "Electronics", "Mechanical" };
        }

        // GET: Students
        [OutputCache(Duration = 30, VaryByParam = "page")]
        public ActionResult Index( int? page)
        {

            try
            {
                int pageSize = 2;
                int pageNumber = (page ?? 1);
                var students = db.Students.OrderBy(s => s.FullName).ToPagedList(pageNumber, pageSize);
                return View(students);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: Students/Details/5
        [OutputCache(Duration = 60, VaryByParam = "id")]
        public ActionResult Details(int? id)
        {
           try
            {
                if (id == null)
                    return HttpNotFound();

                var student = db.Students.Find(id);
                if (student == null)
                    return HttpNotFound();

                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Courses = new SelectList(GetCourseList());
            ViewData["PageInfo"] = "Add new student details below";
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();

                    // Store data in Session
                    Session["StudentName"] = student.FullName;

                    TempData["SuccessMessage"] = "Student created successfully!";
                    return RedirectToAction("Dashboard");
                }
                return View(student);
            }
            catch(Exception ex)
            {
                // If model fails, reload dropdown
                ViewBag.Courses = new SelectList(GetCourseList());
                return View(student);
            }
            

           
        }


        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (id == null)
                    return HttpNotFound();

                var student = db.Students.Find(id);
                if (student == null)
                    return HttpNotFound();

                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }


        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        { 

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction("Dashboard");
                }
                return View(student);// In case of model error
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }


            
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {

            try
            {
                if (id == null)
                    return HttpNotFound();

                var student = db.Students.Find(id);
                if (student == null)
                    return HttpNotFound();

                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Student deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }


        public ActionResult Dashboard()
        {
            if (Session["StudentName"] == null)
            {
                return RedirectToAction("Index");  // 👈 Go back to home if session is null
            }

            ViewBag.Name = Session["StudentName"].ToString();
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();        // clears all session keys
            Session.Abandon();      // ends the session

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
