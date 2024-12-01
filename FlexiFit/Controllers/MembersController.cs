using Microsoft.AspNetCore.Mvc;

namespace FlexiFit.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
