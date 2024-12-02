using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Controller to manage booking-related operations such as viewing, creating, and deleting bookings.
    /// </summary>
    [Route("[controller]/[action]")]
    public class BookingsController : Controller
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Member> _memberRepository;

        /// <summary>
        /// Constructor for dependency injection of repositories.
        /// </summary>
        public BookingsController(
            IRepository<Booking> bookingRepository,
            IRepository<Class> classRepository,
            IRepository<Member> memberRepository)
        {
            _bookingRepository = bookingRepository;
            _classRepository = classRepository;
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Retrieves the schedule of the logged-in user.
        /// </summary>
        /// <returns>View displaying the user's booking schedule.</returns>
        [HttpGet]
        public IActionResult Schedule()
        {
            try
            {
                // Retrieve MemberId from session
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
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

                // Populate ViewBag.Classes with available classes for the dropdown
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

        /// <summary>
        /// Deletes a booking by its ID.
        /// </summary>
        /// <param name="id">The ID of the booking to delete.</param>
        /// <returns>View to confirm the deletion of the booking.</returns>
        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Retrieve MemberId from session
                int? memberId = HttpContext.Session.GetInt32("MemberId");
                if (memberId == null)
                {
                    return RedirectToAction("Login", "Members");
                }

                var booking = _bookingRepository.GetById(id);
                if (booking == null || booking.MemberId != memberId.Value)
                {
                    return NotFound();
                }

                booking.Class = _classRepository.GetById(booking.ClassId);

                return View(booking);
            }
            catch (Exception ex)
            {
                return View("Error", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        /// <summary>
        /// Confirms and deletes a booking.
        /// </summary>
        /// <param name="bookingId">The ID of the booking to delete.</param>
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
                    return RedirectToAction("Login", "Members");
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

        /// <summary>
        /// Books a new class for the logged-in member.
        /// </summary>
        /// <param name="classId">ID of the class to book.</param>
        /// <param name="bookingDate">Date of the booking.</param>
        /// <param name="bookingTime">Time of the booking.</param>
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

                // Create a new booking
                var booking = new Booking
                {
                    MemberId = memberId.Value,
                    ClassId = classId,
                    BookingDate = bookingDate,
                    BookingTime = bookingTime
                };

                _bookingRepository.Add(booking);
                return RedirectToAction("Schedule");
            }
            catch (Exception ex)
            {
                return View("Error", new Models.ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
