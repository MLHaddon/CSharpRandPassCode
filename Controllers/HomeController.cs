using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPassCode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPassCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string randPass ="";
            Random rand = new Random();
            List<string> chars = new List<string>() 
            {"1","2","3","4","5","6",
            "7","8","9","0","A", "B", 
            "C", "D", "E", "F", "G", 
            "H", "I", "J", "K", "L", 
            "M", "N", "O", "P", "Q", 
            "R", "S", "T", "U", "V", 
            "W", "X", "Y", "Z"};
            for (int i = 0; i < 14; i++)
            {
                randPass += chars[rand.Next(0, chars.Count - 1)];
            }
            
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 0);
            }
            HttpContext.Session.SetString("RandPass", randPass);
            ViewBag.Count = (int)HttpContext.Session.GetInt32("Count");
            ViewBag.RandPass = randPass;
            return View();
        }

        public IActionResult Click()
        {
            int count = (int)HttpContext.Session.GetInt32("Count");
            count++;
            HttpContext.Session.SetInt32("Count", count);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
