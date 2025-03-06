using Microsoft.AspNetCore.Mvc;
using WorkshopWork.Models;

namespace WorkshopWork.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> employees = new Employee().GetData();
            return View(employees);
        }
    }
}
