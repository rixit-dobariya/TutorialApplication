using Microsoft.AspNetCore.Mvc;
using PTClassWork.Models;

namespace PTClassWork.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View(new Student().GetStudents(null));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Create())
                {
                    TempData["msg"] = "Record Added successfully!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["msg"] = "Record insertion Failed!";
                }
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
                Student student = new Student().GetStudents(id).FirstOrDefault();
                return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Update())
                {
                    TempData["msg"] = "Record Updated successfully!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["msg"] = "Record Updation Failed!";
                }
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            Student student = new Student().GetStudents(id).FirstOrDefault() ?? new Student();
            return View(student);
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Delete())
                {
                    TempData["msg"] = "Record Deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Record Deletion Failed!";
                }
            }
            return View();
        }
    }
}
