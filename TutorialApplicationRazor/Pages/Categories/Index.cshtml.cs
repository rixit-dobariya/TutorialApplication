using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorialApplicationRazor.Data;
using TutorialApplicationRazor.Models;

namespace TutorialApplicationRazor.Pages.Categories
{
    public class IndexModel(ApplicationDbContext db) : PageModel
    {
        ApplicationDbContext _db = db;

        public List<Category> CategoryList { get; set; }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
