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
    /// Handles member-related operations such as sign-up, login, membership management, and booking classes.
    /// </summary>
    [Route("[controller]/[action]")]
    public class MembersController : Controller
    {
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IRepository<Booking> _bookingRepository;

        /// <summary>
        /// Constructor for dependency injection of repositories.
        /// </summary>
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

        /// <summary>
        /// Displays the sign-up page for new members.
        /// </summary>
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// Handles new member sign-up.
        /// </summary>
        /// <param name="member">The member details entered by the user.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Member member)
        {
            if (ModelState.IsValid)
            {
                _memberRepository.Add(member); // Adds the new member to the repository
                HttpContext.Session.SetInt32("MemberId", member.MemberId); // Stores the member's ID in the session
                return RedirectToAction("MemberInfo", new { id = member.MemberId });
            }
            return View(member);
        }

        /// <summary>
        /// Displays the login page for members.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Authenticates a member's login credentials.
        /// </summary>
        /// <param name="model">Login credentials submitted by the user.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = _memberRepository.GetAll()
                    .FirstOrDefault(m => m.Email == model.Email && m.Password == model.Password);

                if (member != null)
                {
                    HttpContext.Session.SetInt32("MemberId", member.MemberId); // Store the member ID in session
                    return RedirectToAction("MemberInfo", new { id = member.MemberId });
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View(model);
        }

        /// <summary>
        /// Logs out the member and clears session data.
        /// </summary>
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Index", "Home"); // Redirect to Home page
        }

        /// <summary>
        /// Displays the member's information page.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        [HttpGet("{id}")]
        public IActionResult MemberInfo(int id)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members"); // Redirect if not logged in
            }

            var member = _memberRepository.GetById(memberId.Value);
            if (member == null)
            {
                return NotFound("Member not found.");
            }

            return View(member);
        }

        /// <summary>
        /// Displays the membership management page.
        /// </summary>
        /// <param name="id">The ID of the member whose membership is being managed.</param>
        [HttpGet("{id}")]
        public IActionResult ManageMembership(int id)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            var member = _memberRepository.GetById(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }

            return View(member);
        }

        /// <summary>
        /// Updates the member's membership status.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        /// <param name="isActive">The new membership status.</param>
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ManageMembership(int id, bool isActive)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            var member = _memberRepository.GetById(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }

            member.IsActive = isActive; // Update membership status
            _memberRepository.Update(member);

            return RedirectToAction("MemberInfo", new { id = member.MemberId });
        }

        /// <summary>
        /// Lists all distinct muscle groups for exercises.
        /// </summary>
        [HttpGet]
        public IActionResult FindExercise()
        {
            var muscleGroups = _workoutRepository.GetAll()
                .Select(w => w.MuscleGroup)
                .Distinct()
                .ToList();

            return View(muscleGroups);
        }

        /// <summary>
        /// Displays workouts related to a specific muscle group.
        /// </summary>
        /// <param name="muscleGroup">The muscle group to filter workouts by.</param>
        [HttpGet]
        public IActionResult WorkoutDetails(string muscleGroup)
        {
            var workouts = _workoutRepository.GetAll()
                .Where(w => w.MuscleGroup == muscleGroup)
                .ToList();

            return View(workouts);
        }

        /// <summary>
        /// Displays the page for booking classes.
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
        /// Books a class for a member.
        /// </summary>
        /// <param name="memberId">The ID of the member.</param>
        /// <param name="classId">The ID of the class to be booked.</param>
        /// <param name="bookingDate">The date of the booking.</param>
        /// <param name="bookingTime">The time of the booking.</param>
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
            return RedirectToAction("Schedule", "Bookings");
        }

        /// <summary>
        /// Displays the class schedule for a member.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
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
