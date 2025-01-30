using Bulky.DataAccess.Respository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TutorialApplication.Models;


namespace TutorialApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //fetch data into list
            List<Product> productsList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return View(productsList);
        }
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product= new Product(),
                CategoryList =  _unitOfWork.CategoryRepository.GetAll().Select(e => new SelectListItem{
                    Text = e.Name,
                    Value = e.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(productVM.Product);
                _unitOfWork.Save(); 
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
                return View(productVM);
            }
        }
        public IActionResult Upsert(int? id)//will be used for both create and edit
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                })
            };
            if(id==null || id == 0)//create
            {
                return View(productVM);
            }
            else//update
            {
                productVM.Product = _unitOfWork.ProductRepository.Get(e => e.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        //file is the name of the input control in razor view
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)//will be used for both create and edit
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if(productVM.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(productVM.Product);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
                return View(productVM);
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null && id < 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.ProductRepository.Get(e => e.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryListItems = _unitOfWork.CategoryRepository.GetAll().Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            });
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryListItems = _unitOfWork.CategoryRepository.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
                return View();
            }
        }

        //public IActionResult Delete(int? id)
        //{
        //    Product? product = _unitOfWork.ProductRepository.Get(e => e.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    if (!string.IsNullOrEmpty(product.ImageUrl))
        //    {
        //        //delete old image
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;

        //        var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
        //        if (System.IO.File.Exists(oldImagePath))
        //        {
        //            System.IO.File.Delete(oldImagePath);
        //        }
        //    }
        //    _unitOfWork.ProductRepository.Remove(product);
        //    _unitOfWork.Save();
        //    return RedirectToAction("Index");
        //}
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> productsList = _unitOfWork.ProductRepository.GetAll(includeProperties:"Category").ToList();
            return Json(new { data = productsList });
        }
        //[HttpDelete]
        public IActionResult Delete(int? id)
        {
            if(id==null || id == 0)
            {
                return Json(new {message= "No id found", success=false});
            }
            Product product = _unitOfWork.ProductRepository.Get(e=>e.Id== id);
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                //delete old image
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            return Json(new {success=true, message="Delete Successful"});
        }
        #endregion

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Product? product = _unitOfWork.ProductRepository.Get(e => e.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    if (!string.IsNullOrEmpty(product.ImageUrl))
        //    {
        //        //delete old image
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;

        //        var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
        //        if (System.IO.File.Exists(oldImagePath))
        //        {
        //            System.IO.File.Delete(oldImagePath);
        //        }
        //    }
        //    _unitOfWork.ProductRepository.Remove(product);
        //    _unitOfWork.Save();
        //    return RedirectToAction("Index");
        //}
    }
}
