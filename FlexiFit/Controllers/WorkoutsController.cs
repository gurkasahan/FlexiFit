using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;
using System.Linq;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Handles workout-related operations such as viewing exercises and their details.
    /// </summary>
    [Route("[controller]/[action]")]
    public class WorkoutsController : Controller
    {
        private readonly IRepository<Workout> _workoutRepository;

        /// <summary>
        /// Constructor for dependency injection of workout repository.
        /// </summary>
        public WorkoutsController(IRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        /// <summary>
        /// Lists distinct muscle groups available for workouts.
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
        /// Displays details of workouts for a specific muscle group.
        /// </summary>
        /// <param name="muscleGroup">The muscle group to filter workouts by.</param>
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

            ViewBag.MuscleGroup = muscleGroup;
            return View(workouts);
        }

        /// <summary>
        /// Displays details of a specific workout.
        /// </summary>
        /// <param name="id">The ID of the workout.</param>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var workout = _workoutRepository.GetById(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }
    }
}
