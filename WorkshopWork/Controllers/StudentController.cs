using Microsoft.AspNetCore.Mvc;
using WorkshopWork.Models;

namespace WorkshopWork.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            Student student = new Student();
            List<Student> students =student.getData("");
            return View(students);
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
                if (student.Insert(student))
                {
                    TempData["message"] = "Added Successfully!";
                }
                else
                {
                    TempData["message"] = "Not added. Something went wrong!";
                }
            }
            
            return View();
        }
        public IActionResult Edit(string id)
        {
            List<Student> students = new Student().getData(id);
            return View(students.FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Update(student))
                {
                    TempData["message"] = "Updated Successfully!";
                }
                else
                {
                    TempData["message"] = "Not added. Something went wrong!";
                }
            }
            return View();
        }
        public IActionResult Delete(string id)
        {
            List<Student> students = new Student().getData(id);
            return View(students.FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Delete(student.Id.ToString()))
                {
                    TempData["message"] = "Deleted Successfully!";
                }
                else
                {
                    TempData["message"] = "Not added. Something went wrong!";
                }
            }
            return View();
        }
    }
}
