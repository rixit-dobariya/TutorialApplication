using Microsoft.AspNetCore.Mvc;
using PracticeApplication.Data;
using PracticeApplication.Models;

namespace PracticeApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db= db;
        }

        public IActionResult Index()
        {
            List<Student> students = _db.Students.ToList();
            return View(students);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid){
                _db.Students.Add(student);
                _db.SaveChanges();
                TempData["msg"] = "Student record inserted successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }

        public IActionResult Edit(int studentId)
        {
            if(studentId==null || studentId == 0)
            {
                TempData["msg"] = "Invalid request";
                return RedirectToAction("Index");
            }
            else
            {
                Student student = _db.Students.Find(studentId);
                return View(student);
            }
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                TempData["msg"] = "Student record updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
        public IActionResult Delete(int studentId)
        {
            if (studentId == null || studentId == 0)
            {
                TempData["msg"] = "Invalid request";
                return RedirectToAction("Index");
            }
            else
            {
                Student student = _db.Students.Find(studentId);
                return View(student);
            }
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                TempData["msg"] = "Student record deleted successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
    }
}
