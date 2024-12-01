// FlexiFit.Entities/Workout.cs
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities.Models
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Represents a workout exercise.
    /// </summary>
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MuscleGroup { get; set; } // E.g., Chest, Biceps

        [Required]
        public string ExerciseName { get; set; }

        public string Description { get; set; }

        [RegularExpression(@"^\d+x\d+$", ErrorMessage = "Repetitions format should be 'sets x reps' (e.g., 3x10).")]
        public string Repetitions { get; set; }

        public string ImageUrl { get; set; }
    }
}
