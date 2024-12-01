// FlexiFit/Controllers/BookingsController.cs
using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities;
using FlexiFit.Services;
using System;
using System.Linq;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;

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

        // GET: Bookings/Create/{memberId}
        [HttpGet("{memberId}")]
        public IActionResult Create(int memberId)
        {
            var classes = _classRepository.GetAll();
            ViewBag.Classes = classes;
            ViewBag.MemberId = memberId;
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookingRepository.Add(booking);
                return RedirectToAction("Schedule", new { memberId = booking.MemberId });
            }

            var classes = _classRepository.GetAll();
            ViewBag.Classes = classes;
            ViewBag.MemberId = booking.MemberId;
            return View(booking);
        }

        // GET: Bookings/Schedule/{memberId}
        [HttpGet("{memberId}")]
        public IActionResult Schedule(int memberId)
        {
            var bookings = _bookingRepository.GetAll()
                .Where(b => b.MemberId == memberId)
                .ToList();

            return View(bookings);
        }

        // GET: Bookings/Delete/{id}
        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _bookingRepository.Get(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/{id}
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _bookingRepository.Get(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingRepository.Delete(id);
            return RedirectToAction("Schedule", new { memberId = booking.MemberId });
        }
    }
}
