using System.Collections.Generic;
using System.Linq;
using ASPNETExercises.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ASPNETExercises.Pages
{
    public class CategoryPageModel : PageModel
    {
        AppDbContext _db;
        public CategoryPageModel(AppDbContext context)
        {
            _db = context;
        }
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _db.Categories.ToList<Category>();
        }
    }
}
