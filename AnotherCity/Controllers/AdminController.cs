using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AnotherCity.Controllers
{

    [Authorize(Roles = "SuperUser")]
    [Route("/donotaskmetoenterinformationtothiswebsite")]
    public class AdminController : Controller
    {
        [Route("/donotaskmetoenterinformationtothiswebsite/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("/admin/contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [Route("/admin/error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
