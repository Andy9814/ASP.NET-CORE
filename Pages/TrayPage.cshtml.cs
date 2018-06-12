using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETExercises.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNETExercises.Pages
{
    public class TrayPageModel : PageModel
    {
        AppDbContext _db;
        public TrayPageModel(AppDbContext context)
        {
            _db = context;
        }
        public List<Tray> Tray { get; set; }
        public void OnGet()
        {

            Tray = _db.Tray.ToList<Tray>();
        }
    }
}