using Microsoft.AspNetCore.Mvc;

namespace FlexiFit.Controllers
{
    public class ClassesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
