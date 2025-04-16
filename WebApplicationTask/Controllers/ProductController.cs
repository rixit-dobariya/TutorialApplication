using Microsoft.AspNetCore.Mvc;
using WebApplicationTask.Models;

namespace WebApplicationTask.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = Product.GetAll(); 
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //if (product.Add())
                //{

                //}
            }  
            return View();
        }
    }
}
