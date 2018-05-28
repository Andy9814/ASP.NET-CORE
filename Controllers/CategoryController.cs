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
        public IActionResult SelectCategory(CategoryViewModel vm)
        {
            CategoryModel catModel = new CategoryModel(_db);
            MenuItemModel menuModel = new MenuItemModel(_db);
            List<MenuItem> items = menuModel.GetAllByCategory(vm.CategoryId);
            List<MenuItemViewModel> vms = new List<MenuItemViewModel>();
            if (items.Count > 0)
            {
                foreach (MenuItem item in items)
                {
                    MenuItemViewModel mvm = new MenuItemViewModel();
                    mvm.Qty = 0;
                    mvm.CategoryId = item.CategoryId;
                    mvm.CategoryName = catModel.GetName(item.CategoryId);
                    mvm.Description = item.Description;
                    mvm.Id = item.Id;
                    mvm.PRO = item.Protein;
                    mvm.SALT = item.Salt;
                    mvm.FAT = Convert.ToDecimal(item.Fat);
                    mvm.FBR = item.Fibre;
                    mvm.CHOL = item.Cholesterol;
                    mvm.CAL = item.Calories;
                    mvm.CARB = item.Carbs;
                    vms.Add(mvm);
                }
                MenuItemViewModel[] myMenu = vms.ToArray();
                HttpContext.Session.Set<MenuItemViewModel[]>("menu", myMenu);
            }
            vm.SetCategories(HttpContext.Session.Get<List<Category>>("categories"));
            return View("Index", vm); // need the original Index View here
        }        [HttpPost]
        public ActionResult SelectItem(CategoryViewModel vm)
        {
            // cs we  change int to string 
            Dictionary<int, object> tray;
            if (HttpContext.Session.Get<Dictionary<int, Object>>("tray") == null)
            {
                tray = new Dictionary<int, object>();
            }
            else
            {
                tray = HttpContext.Session.Get<Dictionary<int, object>>("tray");
            }
            MenuItemViewModel[] menu = HttpContext.Session.Get<MenuItemViewModel[]>("menu");
            String retMsg = "";
            foreach (MenuItemViewModel item in menu)
            {
                if (item.Id == vm.Id)// change that to .productId
                {
                    if (vm.Qty > 0) // update only selected item
                    {
                        item.Qty = vm.Qty;
                        retMsg = vm.Qty + " - item(s) Added!";
                        tray[item.Id] = item;
                    }
                    else
                    {
                        item.Qty = 0;
                        tray.Remove(item.Id);
                        retMsg = "item(s) Removed!";
                    }
                    vm.CategoryId = item.CategoryId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.Set<Dictionary<int, Object>>("tray", tray);
            vm.SetCategories(HttpContext.Session.Get<List<Category>>("categories"));
            return View("Index", vm);
        }
    }

}

