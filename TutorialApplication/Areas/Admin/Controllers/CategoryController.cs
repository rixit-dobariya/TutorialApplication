using Bulky.DataAccess.Respository.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;

//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TutorialApplication.Data;
using TutorialApplication.Models;

namespace TutorialApplication.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //fetch data into list
            List<Category> categoriesList = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(categoriesList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //Data annotation for post method
        public IActionResult Create(Category category) //post method have argument
        {
            List<Category> categoriesList = _unitOfWork.CategoryRepository.GetAll().ToList();
            bool isPresent = false;
            foreach (var cat in categoriesList)
            {
                if (cat.DisplayOrder.Equals(category.DisplayOrder))
                {
                    isPresent = true;
                    break;
                }
            }
            if (isPresent)
            {
                ModelState.AddModelError("", "This display order is already taken");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category); //here database entry would not have happened
                _unitOfWork.Save(); // here change would be done
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? categoryid)
        {
            if (categoryid == null && categoryid < 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.CategoryRepository.Get(e => e.Id == categoryid);
            //Category? category = _db.Categories.Find(id); 
            //can be used with only primary key
            //Category? category1 = _db.Categories.FirstOrDefault(e=>e.Id==id);
            //can be used with any field name of the model
            //Category? category2 = _db.Categories.Where(e => e.Id == id).FirstOrDefault();
            //can be used with some extra conditions 
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null && id < 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.CategoryRepository.Get(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _unitOfWork.CategoryRepository.Get(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}


//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using TutorialApplication.Data;
//using TutorialApplication.Models;

//namespace TutorialApplication.Controllers
//{
//    public class CategoryController : Controller
//    {
//        private ApplicationDbContext _db;
//        public CategoryController(ApplicationDbContext db)
//        {
//            _db = db;
//        }
//        public IActionResult Index()
//        {
//            //fetch data into list
//            List<Category> categoriesList = _db.Categories.ToList();
//            return View(categoriesList);
//        }
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost] //Data annotation for post method
//        public IActionResult Create(Category category) //post method have argument
//        {
//            List<Category> categoriesList = _db.Categories.ToList();
//            bool isPresent = false;
//            foreach (var cat in categoriesList)
//            {
//                if (cat.DisplayOrder.Equals(category.DisplayOrder))
//                {
//                    isPresent = true;
//                    break;
//                }
//            }
//            if (isPresent)
//            {
//                ModelState.AddModelError("", "This display order is already taken");
//            }
//            if (ModelState.IsValid)
//            {
//                _db.Categories.Add(category); //here database entry would not have happened
//                _db.SaveChanges(); // here change would be done
//                return RedirectToAction("Index");
//            }
//            return View();
//        }
//        public IActionResult Edit(int? id)
//        {
//            if (id == null && id < 0)
//            {
//                return NotFound();
//            }
//            Category? category = _db.Categories.Find(id);
//            //can be used with only primary key
//            Category? category1 = _db.Categories.FirstOrDefault(e => e.Id == id);
//            //can be used with any field name of the model
//            Category? category2 = _db.Categories.Where(e => e.Id == id).FirstOrDefault();
//            //can be used with some extra conditions 
//            if (category == null)
//            {
//                return NotFound();
//            }

//            return View(category);
//        }
//        [HttpPost]
//        public IActionResult Edit(Category category)
//        {
//            //if (ModelState.IsValid)
//            //{
//            _db.Categories.Update(category); //Will update the record based on the primary key
//                                             //if id is 0 then it will create new record with this object
//            _db.SaveChanges();
//            return RedirectToAction("Index");
//            //}
//            return View();
//        }

//        public IActionResult Delete(int? id)
//        {
//            if (id == null && id < 0)
//            {
//                return NotFound();
//            }
//            Category? category = _db.Categories.Find(id);
//            if (category == null)
//            {
//                return NotFound();
//            }

//            return View(category);
//        }
//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeletePOST(int? id)
//        {
//            Category? category = _db.Categories.Find(id);
//            if (category == null)
//            {
//                return NotFound();
//            }
//            _db.Categories.Remove(category);
//            _db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//    }
//}
