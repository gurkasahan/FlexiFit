//class controller

using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System.Linq;
using FlexiFit.Models;
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
            try
            {
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    // User is not signed in, redirect to SignUp page
                    return RedirectToAction("SignUp", "Members");
                }

                var classes = _classRepository.GetAll().ToList();
                ViewBag.Classes = classes;

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Classes/BookClasses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookClasses(Booking booking)
        {
            try
            {

                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    return RedirectToAction("SignUp", "Members");
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

                    // Add booking and save changes
                    _bookingRepository.Add(booking);
                    return RedirectToAction("Schedule", "Bookings");
                }
                else
                {
                    ViewBag.Classes = _classRepository.GetAll().ToList();
                    return View(booking);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while booking the class.");
                ViewBag.Classes = _classRepository.GetAll().ToList();
                return View(booking);
            }
        }
    }
}
