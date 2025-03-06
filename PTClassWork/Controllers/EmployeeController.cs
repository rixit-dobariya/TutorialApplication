using Microsoft.AspNetCore.Mvc;
using PTClassWork.Models;

namespace PTClassWork.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            EmployeeModel model = new EmployeeModel();
            List<EmployeeModel> employeeList = model.GetAll();
            return View(employeeList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            if(new EmployeeModel().Add(employee))
            {
                TempData["message"] = "Employee added successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Employee added failed!";
                return View(employee);
            }
        }
        public IActionResult Edit(int employeeId)
        {
            return View(new EmployeeModel().Get(employeeId));
        }
        [HttpPost]
        public IActionResult Edit(EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            if (new EmployeeModel().Update(employee))
            {
                TempData["message"] = "Employee updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Employee updating failed!";
                return View(employee);
            }
        }
        public IActionResult Delete(int employeeId)
        {
            return View(new EmployeeModel().Get(employeeId));
        }
        [HttpPost]
        public IActionResult Delete(EmployeeModel employee)
        {
            if (employee.Id == 0)
            {
                return View(employee);
            }
            if (new EmployeeModel().Delete(employee.Id))
            {
                TempData["message"] = "Employee deleted successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Employee deletion failed!";
                return View(employee);
            }
        }
    }
}
