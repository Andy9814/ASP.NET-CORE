using Microsoft.AspNetCore.Mvc;
using ASPNETExercises.Models;
namespace ASPNETExercises.Controllers
{
    public class MenuItemController : Controller
    {
        AppDbContext _db;
        public MenuItemController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(CategoryViewModel category)
        {
            MenuItemModel model = new MenuItemModel(_db);
            MenuItemViewModel viewModel = new MenuItemViewModel();
            viewModel.CategoryName = category.CategoryName;
            viewModel.MenuItems = model.GetAllByCategory(category.Id);
            return View(viewModel);
        }
    }
}