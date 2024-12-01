// FlexiFit/Controllers/MembersController.cs
using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities;
using FlexiFit.Services;
using System;
using System.Linq;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using FlexiFit.Entities.ValidationAttributes;
using FlexiFit.Models;


namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Manages member-related operations.
    /// </summary>
    [Route("[controller]/[action]")]
    public class MembersController : Controller
    {
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public MembersController(
            IRepository<Member> memberRepository,
            IRepository<Class> classRepository,
            IRepository<Workout> workoutRepository,
            IRepository<Booking> bookingRepository)
        {
            _memberRepository = memberRepository;
            _classRepository = classRepository;
            _workoutRepository = workoutRepository;
            _bookingRepository = bookingRepository;
        }

        // GET: Members/SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // Return an error view or message
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Members/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _memberRepository.Add(member);
                    return RedirectToAction("MemberInfo", new { id = member.MemberId });
                }
                else
                {
                    // If validation fails, return the view with validation messages
                    return View(member);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");


                // Return the view with the current member model and validation messages
                return View(member);
            }
        }

        // GET: Members/MemberInfo/{id}
        [HttpGet("{id}")]
        public IActionResult MemberInfo(int id)
        {
            try
            {
                var member = _memberRepository.GetById(id);
                if (member == null)
                {
                    return NotFound();
                }
                return View(member);
            }
            catch (Exception ex)
            {
                // Log the exception
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Members/ManageMembership/{id}
        [HttpGet("{id}")]
        public IActionResult ManageMembership(int id)
        {
            try
            {
                var member = _memberRepository.GetById(id);
                if (member == null)
                {
                    return NotFound();
                }
                return View(member);
            }
            catch (Exception ex)
            {
                // Log the exception
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Members/ManageMembership/{id}
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ManageMembership(int id, bool isActive)
        {
            try
            {
                var member = _memberRepository.GetById(id);
                if (member == null)
                {
                    return NotFound();
                }

                member.IsActive = isActive;
                _memberRepository.Update(member);
                return RedirectToAction("MemberInfo", new { id = member.MemberId });
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An error occurred while updating your membership status.");
                return View();
            }
        }

        // GET: Members/FindExercise
        [HttpGet]
        public IActionResult FindExercise()
        {
            var muscleGroups = _workoutRepository.GetAll()
                .Select(w => w.MuscleGroup)
                .Distinct()
                .ToList();
            return View(muscleGroups);
        }

        // GET: Members/WorkoutDetails?muscleGroup=Chest
        [HttpGet]
        public IActionResult WorkoutDetails(string muscleGroup)
        {
            var workouts = _workoutRepository.GetAll()
                .Where(w => w.MuscleGroup == muscleGroup)
                .ToList();
            return View(workouts);
        }

        // GET: Members/BookClasses
        [HttpGet]
        public IActionResult BookClasses()
        {
            var classes = _classRepository.GetAll();
            return View(classes);
        }

        // POST: Members/BookClasses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookClasses(int memberId, int classId, DateTime bookingDate, TimeSpan bookingTime)
        {
            var booking = new Booking
            {
                MemberId = memberId,
                ClassId = classId,
                BookingDate = bookingDate,
                BookingTime = bookingTime
            };
            _bookingRepository.Add(booking);
            return RedirectToAction("ClassSchedule", new { id = memberId });
        }

        // GET: Members/ClassSchedule/{id}
        [HttpGet("{id}")]
        public IActionResult ClassSchedule(int id)
        {
            var bookings = _bookingRepository.GetAll()
                .Where(b => b.MemberId == id)
                .ToList();
            return View(bookings);
        }
    }
}
