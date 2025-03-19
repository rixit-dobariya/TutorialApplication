using Microsoft.AspNetCore.Mvc;
using PTClassWork.Models;
using System.Diagnostics;

namespace PTClassWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor context;

        public HomeController(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetInt32("userid", 1);
            context.HttpContext.Session.SetInt32("RollNo", 15);
            context.HttpContext.Session.SetString("Name", "Rixit");
            return View();
        }

        public IActionResult Privacy()
        {
            //ViewData["userid"] = HttpContext.Session.GetInt32("userid");
            return View();
        }
        public IActionResult About()
        {
            ViewData["CompanyName"] = "Flone";
            ViewBag.CompanyName = "Flone";
            TempData["CompanyName"] = "Flone";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
