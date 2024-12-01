using Microsoft.AspNetCore.Mvc;

namespace FlexiFit.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
