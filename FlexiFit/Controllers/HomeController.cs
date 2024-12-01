// FlexiFit/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Manages the home page and general navigation.
    /// </summary>
    public class HomeController : Controller
    {
        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/About
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        // GET: Home/Contact
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        // GET: Home/Error
        public IActionResult Error()
        {
            return View();
        }
    }
}
