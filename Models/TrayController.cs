using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using ASPNETExercises.Utils;
namespace ASPNETExercises.Controllers
    
{
    public class TrayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ClearTray() // clear out current tray
        {
            HttpContext.Session.Remove("tray");
            HttpContext.Session.Set<String>("Message", "Tray Cleared");
            return Redirect("/Home");
        } 
    }
}