using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorialApplicationRazor.Data;
using TutorialApplicationRazor.Models;

namespace TutorialApplicationRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            if (Category.Id != null && Category.Id != 0)
            {
                _db.Categories.Remove(Category);
                _db.SaveChanges();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
