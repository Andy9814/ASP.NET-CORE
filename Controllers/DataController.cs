using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using ASPNetExercises.Models;

namespace ASPNETExercises.Controllers
{
    public class DataController : Controller
    {
        AppDbContext _ctx;
        public DataController(AppDbContext context)
        {
            _ctx = context;
        }
        public async Task<IActionResult> Index()
        {
            DataUtility util = new DataUtility(_ctx);
            string msg = "";
            var json = await getMenuItemJsonFromWeb();
            try
            {
                msg = (util.loadNutritionInfoFromWebToDb(json)) ? "tables loaded" : "problem loading tables";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Content(msg);
        }

        private async Task<String> getMenuItemJsonFromWeb()
        {
            string url = "https://raw.githubusercontent.com/pffy/data-mcdonalds-nutritionfacts/master/json/mcd.json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

    }
}