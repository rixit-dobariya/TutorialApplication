using Microsoft.AspNetCore.Mvc;
using WorkshopWork.Models;

namespace WorkshopWork.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View(new TeacherModel().GetAll()??new List<TeacherModel>());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeacherModel teacherModel)
        {
            if (!ModelState.IsValid)
            {
                return View(teacherModel);
            }
            if (teacherModel.Create())
            {
                TempData["msg"] = "Insert successful";
            }
            else
            {
                TempData["msg"] = "Insertion failed";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int teacherId)
        {
            TeacherModel teacher = new TeacherModel().Get(teacherId);
            if (teacher == null)
            {
                TempData["msg"] = "This record does not exist";
                return RedirectToAction("Index");
            }
            return View(teacher);
        }
        [HttpPost]
        public IActionResult Edit(TeacherModel teacherModel)
        {
            if (!ModelState.IsValid)
            {
                return View(teacherModel);
            }
            if (teacherModel.Update())
            {
                TempData["msg"] = "Update successful";
            }
            else
            {
                TempData["msg"] = "Update failed";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int teacherId)
        {
            TeacherModel teacher = new TeacherModel().Get(teacherId);
            if (teacher == null)
            {
                TempData["msg"] = "This record does not exist";
                return RedirectToAction("Index");
            }
            return View(teacher);
        }
        [HttpPost]
        public IActionResult Delete(TeacherModel teacherModel)
        {
            if (!ModelState.IsValid)
            {
                return View(teacherModel);
            }
            if (teacherModel.Delete())
            {
                TempData["msg"] = "Delete successful";
            }
            else
            {
                TempData["msg"] = "Delete failed";
            }
            return RedirectToAction("Index");
        }
    }
}
