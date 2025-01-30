using System.Diagnostics;
using Bulky.DataAccess.Respository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.Models;

namespace TutorialApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _unitOfWork.ProductRepository.GetAll("Category");
            return View(productsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Details(int? productId)
        {
            Product product = _unitOfWork.ProductRepository.Get(e=>e.Id==productId, includeProperties: "Category");
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
