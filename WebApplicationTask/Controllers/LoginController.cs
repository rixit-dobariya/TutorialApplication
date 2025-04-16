using Microsoft.AspNetCore.Mvc;
using WebApplicationTask.Models;

namespace WebApplicationTask.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            if (user == null)
            {
                return View();
            }
            else if(user.Login())
            {
                TempData["msg"] = "You are logged in successfully!";
                HttpContext.Session.SetString("email", user.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            TempData["msg"] = "You are logged out successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
