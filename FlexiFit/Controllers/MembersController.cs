using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FlexiFit.Controllers
{
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
            return View();
        }

        // POST: Members/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Member member)
        {
            if (ModelState.IsValid)
            {
                // Add the new member to the repository
                _memberRepository.Add(member);

                // Store MemberId in session
                HttpContext.Session.SetInt32("MemberId", member.MemberId);

                // Redirect to MemberInfo page
                return RedirectToAction("MemberInfo", new { id = member.MemberId });
            }
            return View(member);
        }

        // GET: Members/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Members/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                // Authenticate the user
                var member = _memberRepository.GetAll()
                    .FirstOrDefault(m => m.Email == model.Email && m.Password == model.Password);

                if (member != null)
                {
                    // Store MemberId in session
                    HttpContext.Session.SetInt32("MemberId", member.MemberId);

                    // Redirect to MemberInfo page
                    return RedirectToAction("MemberInfo", new { id = member.MemberId });
                }

                // If authentication fails
                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View(model);
        }

        // GET: Members/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Redirect to Home page
            return RedirectToAction("Index", "Home");
        }

        // GET: Members/MemberInfo/{id}
        [HttpGet("{id}")]
        public IActionResult MemberInfo(int id)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members"); // Redirect to login if not logged in
            }
            // Retrieve the member by ID
            var member = _memberRepository.GetById(memberId.Value);

            if (member == null)
            {
                return NotFound("Member not found.");
            }

            return View(member);
        }

        // GET: Members/ManageMembership/{id}
        [HttpGet("{id}")]
        public IActionResult ManageMembership(int id)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members"); // Redirect to login if not logged in
            }
            // Retrieve the member by ID
            var member = _memberRepository.GetById(id);

            if (member == null)
            {
                return NotFound("Member not found.");
            }

            return View(member);
        }

        // POST: Members/ManageMembership/{id}
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ManageMembership(int id, bool isActive)
        {
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members"); // Redirect to login if not logged in
            }

            var member = _memberRepository.GetById(id);

            if (member == null)
            {
                return NotFound("Member not found.");
            }

            // Update membership status
            member.IsActive = isActive;
            _memberRepository.Update(member);

            return RedirectToAction("MemberInfo", new { id = member.MemberId });
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
            int? memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            var classes = _classRepository.GetAll().ToList();
            ViewBag.Classes = classes;

            return View(new Booking());
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

            return RedirectToAction("Schedule", "Bookings");
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
