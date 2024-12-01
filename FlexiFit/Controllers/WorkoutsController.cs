using Microsoft.AspNetCore.Mvc;

namespace FlexiFit.Controllers
{
    public class WorkoutsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
