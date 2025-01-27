using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorialApplicationRazor.Data;
using TutorialApplicationRazor.Models;

namespace TutorialApplicationRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
        ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if(id!=null && id != 0)
            { 
                Category = _db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            _db.Categories.Update(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
