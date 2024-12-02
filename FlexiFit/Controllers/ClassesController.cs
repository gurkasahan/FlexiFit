using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Http;

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

        // GET: Classes/BookClasses
        [HttpGet]
        public IActionResult BookClasses()
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            var classes = _classRepository.GetAll().ToList();
            ViewBag.Classes = classes;

            return View(new Booking());
        }

        // POST: Classes/BookClasses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookClasses(Booking booking)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            booking.MemberId = memberId.Value;

            if (ModelState.IsValid)
            {
                var cls = _classRepository.GetById(booking.ClassId);
                if (cls == null)
                {
                    ModelState.AddModelError("", "Invalid class.");
                    ViewBag.Classes = _classRepository.GetAll().ToList();
                    return View(booking);
                }

                _bookingRepository.Add(booking);
                return RedirectToAction("Schedule", "Bookings");
            }

            ViewBag.Classes = _classRepository.GetAll().ToList();
            return View(booking);
        }

        [HttpGet("{id}")]
        public IActionResult BookClassFromDetails(int id)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            var cls = _classRepository.GetById(id);
            if (cls == null)
            {
                return NotFound("Class not found.");
            }

            // Create a new booking
            var booking = new Booking
            {
                MemberId = memberId.Value,
                ClassId = id,
                BookingDate = DateTime.Now.Date, // Example booking date
                BookingTime = DateTime.Now.TimeOfDay // Example booking time
            };

            _bookingRepository.Add(booking);

            // Redirect to the "Schedule" action
            return RedirectToAction("Schedule", "Bookings");
        }

    }
}
