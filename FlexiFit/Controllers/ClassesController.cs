using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Manages operations related to gym classes, including listing, viewing, and booking classes.
    /// </summary>
    [Route("[controller]/[action]")]
    public class ClassesController : Controller
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Booking> _bookingRepository;

        /// <summary>
        /// Constructor to inject class and booking repositories.
        /// </summary>
        public ClassesController(IRepository<Class> classRepository, IRepository<Booking> bookingRepository)
        {
            _classRepository = classRepository;
            _bookingRepository = bookingRepository;
        }

        /// <summary>
        /// Lists all available gym classes.
        /// </summary>
        [HttpGet]
        public IActionResult List()
        {
            var classes = _classRepository.GetAll().ToList();
            return View(classes);
        }

        /// <summary>
        /// Provides details of a specific gym class.
        /// </summary>
        /// <param name="id">The ID of the class.</param>
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

        /// <summary>
        /// Displays the page to book a new class.
        /// </summary>
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

        /// <summary>
        /// Books a new class for the logged-in user.
        /// </summary>
        /// <param name="booking">Booking object containing class details.</param>
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

        /// <summary>
        /// Books a class directly from the class details page.
        /// </summary>
        /// <param name="id">The ID of the class to book.</param>
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

            var booking = new Booking
            {
                MemberId = memberId.Value,
                ClassId = id,
                BookingDate = DateTime.Now.Date,
                BookingTime = DateTime.Now.TimeOfDay
            };

            _bookingRepository.Add(booking);
            return RedirectToAction("Schedule", "Bookings");
        }
    }
}
