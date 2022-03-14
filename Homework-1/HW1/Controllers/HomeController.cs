using HW1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
        // HttpGet for login page
        public IActionResult Login()
        {
            // Empty ViewModel for login page
            ResponseViewModel response = new ResponseViewModel();
            return View(response);
        }

        // HttpPost for login
        [HttpPost]
        public IActionResult Login([FromForm] Login login) // get data from form
        {
            // make new responseViewModel
            ResponseViewModel response = new ResponseViewModel();

            if (!ModelState.IsValid)
            {
                // put info inside ViewModel
                response.login = login;
                response.Success = false;
                response.Error = "Hatalı giriş";
                // send it to user if login fails
                return View(response);
            }
            response.Success = true;
            response.Data = "Giriş işlemi başarılı";
            // send it to user if login is success
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
