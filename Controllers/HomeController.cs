using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETExercises.Models;
using ASPNETExercises.Utils;
using Microsoft.AspNetCore.Http;

namespace ASPNETExercises.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVars.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            }
            if (HttpContext.Session.GetString(SessionVars.LoginStatus) == "not logged in")
            {
                HttpContext.Session.SetString(SessionVars.Message, "most functionality requires you to login!");
            }
            ViewBag.Status = HttpContext.Session.GetString(SessionVars.LoginStatus);
            ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            return View();
        }
        //public IActionResult Index()
        //{
        //    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //    return Content(environment);
        //}


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
