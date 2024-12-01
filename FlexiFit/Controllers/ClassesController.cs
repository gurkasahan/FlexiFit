// FlexiFit/Controllers/ClassesController.cs
using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System;
using System.Linq;
using FlexiFit.Models;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Your Name
    /// Manages class-related operations.
    /// </summary>
    [Route("[controller]/[action]")]
    public class ClassesController : Controller
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Member> _memberRepository;

        public ClassesController(
            IRepository<Class> classRepository,
            IRepository<Booking> bookingRepository,
            IRepository<Member> memberRepository)
        {
            _classRepository = classRepository;
            _bookingRepository = bookingRepository;
            _memberRepository = memberRepository;
        }

        // GET: Classes/List
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var classes = _classRepository.GetAll().ToList();
                return View(classes);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, return an error view
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Classes/Details/{id}
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var cls = _classRepository.GetById(id);
                if (cls == null)
                {
                    return NotFound();
                }
                return View(cls);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Classes/BookClasses/{memberId}
        [HttpGet("{memberId}")]
        public IActionResult BookClasses(int memberId)
        {
            try
            {
                var classes = _classRepository.GetAll().ToList();
                ViewBag.Classes = classes;
                ViewBag.MemberId = memberId;

                // Verify member exists
                var member = _memberRepository.GetById(memberId);
                if (member == null)
                {
                    return NotFound("Member not found.");
                }

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
                if (ModelState.IsValid)
                {
                    // Check if member and class exist
                    var member = _memberRepository.GetById(booking.MemberId);
                    var cls = _classRepository.GetById(booking.ClassId);

                    if (member == null || cls == null)
                    {
                        ModelState.AddModelError("", "Invalid member or class.");
                        // Reload classes for the view
                        ViewBag.Classes = _classRepository.GetAll().ToList();
                        return View(booking);
                    }

                    // Add booking
                    _bookingRepository.Add(booking);
                    return RedirectToAction("Schedule", "Bookings", new { memberId = booking.MemberId });
                }
                else
                {
                    // Reload classes for the view
                    ViewBag.Classes = _classRepository.GetAll().ToList();
                    return View(booking);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while booking the class.");
                // Optionally include the exception message
                // ModelState.AddModelError("", ex.Message);

                // Reload classes for the view
                ViewBag.Classes = _classRepository.GetAll().ToList();
                return View(booking);
            }
        }

    }
}
