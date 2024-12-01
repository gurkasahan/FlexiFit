// FlexiFit/Controllers/WorkoutsController.cs
using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities;
using FlexiFit.Services;
using System.Linq;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Your Name
    /// Manages workout-related operations.
    /// </summary>
    [Route("[controller]/[action]")]
    public class WorkoutsController : Controller
    {
        private readonly IRepository<Workout> _workoutRepository;

        public WorkoutsController(IRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        // GET: Workouts/FindExercise
        [HttpGet]
        public IActionResult FindExercise()
        {
            var muscleGroups = _workoutRepository.GetAll()
                .Select(w => w.MuscleGroup)
                .Distinct()
                .ToList();
            return View(muscleGroups);
        }

        // GET: Workouts/WorkoutDetails?muscleGroup=Chest
        [HttpGet]
        public IActionResult WorkoutDetails(string muscleGroup)
        {
            if (string.IsNullOrEmpty(muscleGroup))
            {
                return RedirectToAction("FindExercise");
            }

            var workouts = _workoutRepository.GetAll()
                .Where(w => w.MuscleGroup == muscleGroup)
                .ToList();

            if (!workouts.Any())
            {
                ViewBag.Message = $"No workouts found for {muscleGroup}.";
            }

            return View(workouts);
        }

        // GET: Workouts/Details/{id}
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var workout = _workoutRepository.Get(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }
    }
}
