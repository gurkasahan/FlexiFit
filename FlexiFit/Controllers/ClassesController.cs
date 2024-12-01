using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System.Linq;

namespace FlexiFit.Controllers
{
    [Route("[controller]/[action]")]
    public class ClassesController : Controller
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public ClassesController(IRepository<Class> classRepository, IRepository<Booking> bookingRepository)
        {
            _classRepository = classRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var classes = _classRepository.GetAll().ToList();
            return View(classes);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var cls = _classRepository.GetById(id);
            if (cls == null)
            {
                return NotFound();
            }
            return View(cls);
        }

        [HttpGet]
        public IActionResult BookClasses()
        {
            ViewBag.Classes = _classRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult BookClasses(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookingRepository.Add(booking);
                return RedirectToAction("List");
            }

            ViewBag.Classes = _classRepository.GetAll().ToList();
            return View(booking);
        }
    }
}
