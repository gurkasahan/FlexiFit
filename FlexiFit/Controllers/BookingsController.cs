// FlexiFit/Controllers/BookingsController.cs
using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http; // For session management

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Manages booking-related operations.
    /// </summary>
    [Route("[controller]/[action]")]
    public class BookingsController : Controller
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Member> _memberRepository;

        public BookingsController(
            IRepository<Booking> bookingRepository,
            IRepository<Class> classRepository,
            IRepository<Member> memberRepository)
        {
            _bookingRepository = bookingRepository;
            _classRepository = classRepository;
            _memberRepository = memberRepository;
        }

        // GET: Bookings/Schedule
        [HttpGet]
        public IActionResult Schedule()
        {
            try
            {
                // Retrieve MemberId from session
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    // User is not signed in, redirect to SignUp page
                    return RedirectToAction("Login", "Members");
                }

                var bookings = _bookingRepository.GetAll()
                    .Where(b => b.MemberId == memberId.Value)
                    .Select(b => new Booking
                    {
                        BookingId = b.BookingId,
                        ClassId = b.ClassId,
                        BookingDate = b.BookingDate,
                        BookingTime = b.BookingTime,
                        Class = _classRepository.GetById(b.ClassId)
                    })
                    .ToList();

                // Populate ViewBag.Classes with available classes
                ViewBag.Classes = _classRepository.GetAll()
                    .Select(c => new { c.ClassId, c.ClassName })
                    .ToList();

                return View(bookings);
            }
            catch (Exception ex)
            {
                return View("Error", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Bookings/Delete/{id}
        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Retrieve MemberId from session
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    return RedirectToAction("SignUp", "Members");
                }

                var booking = _bookingRepository.GetById(id);
                if (booking == null || booking.MemberId != memberId.Value)
                {
                    return NotFound();
                }

                // Load related class information
                booking.Class = _classRepository.GetById(booking.ClassId);

                return View(booking);
            }
            catch (Exception ex)
            {
                return View("Error", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Bookings/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int bookingId)
        {
            try
            {
                // Retrieve MemberId from session
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    return RedirectToAction("SignUp", "Members");
                }

                var booking = _bookingRepository.GetById(bookingId);
                if (booking == null || booking.MemberId != memberId.Value)
                {
                    return NotFound();
                }

                _bookingRepository.Delete(bookingId);
                return RedirectToAction("Schedule");
            }
            catch (Exception ex)
            {
                return View("Error", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookClasses(int classId, DateTime bookingDate, TimeSpan bookingTime)
        {
            try
            {
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    return RedirectToAction("Login", "Members");
                }

                // Create a new booking for the logged-in user
                var booking = new Booking
                {
                    MemberId = memberId.Value,
                    ClassId = classId,
                    BookingDate = bookingDate,
                    BookingTime = bookingTime
                };

                _bookingRepository.Add(booking);
                return RedirectToAction("Schedule"); // Redirect to the My Schedule page
            }
            catch (Exception ex)
            {
                return View("Error", new Models.ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
