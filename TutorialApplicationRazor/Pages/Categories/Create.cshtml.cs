using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorialApplicationRazor.Data;
using TutorialApplicationRazor.Models;

namespace TutorialApplicationRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Category = new Category();
        }
        public IActionResult OnPost() 
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
