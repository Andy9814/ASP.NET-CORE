using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ASPNETExercises.Utils;
using ASPNETExercises.Models;
using System.Collections.Generic;
using System;
namespace ASPNetExercises.Controllers
{
    public class TrayController : Controller
    {
        AppDbContext _db;
        public TrayController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ClearTray() // clear out current tray
        {
            HttpContext.Session.Remove("Tray");
            HttpContext.Session.SetString(SessionVars.Message, "Tray Cleared");
            return Redirect("/Home");
        }
        // Add the tray, pass the session variable info to the db
        public ActionResult AddTray()
        {
            TrayModel model = new TrayModel(_db);
            int retVal = -1;
            string retMessage = "";
            try
            {
                Dictionary<string, object> trayItems = HttpContext.Session.Get<Dictionary<string, object>>(SessionVars.Tray);
                retVal = model.AddTray(trayItems, HttpContext.Session.GetString(SessionVars.User));
                if (retVal > 0) // Tray Added
                {
                    retMessage = "Tray " + retVal + " Created!";
                }
                else // problem
                {
                    retMessage = "Tray not added, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Tray was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.Tray); // clear out current tray once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }
    }
}