using CollegeWork.Data;
using CollegeWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeWork.Controllers
{
    public class StudentController : Controller
    {
        Student student;
        ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Student> studentsList = _db.Students.ToList();
            return View(studentsList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int studentId)
        {
            if(studentId==null || studentId == 0)
            {
                return NotFound();
            }
            Student student = _db.Students.Find(studentId);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _db.Students.Update(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int studentId)
        {
            if (studentId == null || studentId == 0)
            {
                return NotFound();
            }
            Student student = _db.Students.FirstOrDefault(s=>s.StudentId==studentId);
            return View(student);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(Student student)
        {
            _db.Students.Remove(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
