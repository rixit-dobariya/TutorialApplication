using Microsoft.AspNetCore.Mvc;

namespace PTClassWork.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if(username=="admin" && password == "admin")
            {
                HttpContext.Session.SetString("username", "admin");
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect username or password";
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index","Home");
        }
    }
}
