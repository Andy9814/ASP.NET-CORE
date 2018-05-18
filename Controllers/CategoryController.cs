using Microsoft.AspNetCore.Mvc;
using ASPNETExercises.Models;
using System.Collections.Generic;
using ASPNETExercises.Utils;
using System;
namespace ASPNETExercises.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext _db;
        public CategoryController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(CategoryViewModel vm)
        {
            // only build the catalogue once
            if (HttpContext.Session.Get<List<Category>>("categories") == null)
            {
                // no session information so let's go to the database
                try
                {
                    CategoryModel catModel = new CategoryModel(_db);
                    // now load the categories
                    List<Category> categories = catModel.GetAll();
                    HttpContext.Session.Set<List<Category>>("categories", categories);
                    vm.SetCategories(categories);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                // no need to go back to the database as information is already in the session
                vm.SetCategories(HttpContext.Session.Get<List<Category>>("categories"));
                MenuItemModel itemModel = new MenuItemModel(_db);
                vm.MenuItems = itemModel.GetAllByCategory(vm.Id);
            }
            return View(vm);
        }
    }
}