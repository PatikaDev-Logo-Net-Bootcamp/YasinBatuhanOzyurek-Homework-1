using HW1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace HW1.Controllers
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
            return View();
        }

        public IActionResult Login()
        {
            ResponseViewModel response = new ResponseViewModel();
            return View(response);
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            ResponseViewModel response = new ResponseViewModel();

            if (!ModelState.IsValid)
            {
                response.login = login;
                response.Success = false;
                response.Error = "Hatalı giriş";

                return View(response);
            }
            response.Success = true;
            response.Data = "Giriş işlemi başarılı";

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
