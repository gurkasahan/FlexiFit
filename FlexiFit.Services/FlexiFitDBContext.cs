using FlexiFit.Entities;
using FlexiFit.Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace FlexiFit.Services
{
    public class FlexiFitDBContext : DbContext
    {
        public FlexiFitDBContext(DbContextOptions<FlexiFitDBContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data
            modelBuilder.Entity<Class>().HasData(
                new Class { ClassId = 1, ClassName = "Yoga" },
                new Class { ClassId = 2, ClassName = "Pilates" },
                new Class { ClassId = 3, ClassName = "Zumba" },
                new Class { ClassId = 4, ClassName = "Strength-Training" }
            );

            // Seed Workouts
            modelBuilder.Entity<Workout>().HasData(
                new Workout
                {
                    WorkoutId = 1,
                    MuscleGroup = "Chest",
                    ExerciseName = "Bench Press",
                    Description = "Barbell bench press",
                    Repetitions = "5 reps x 5 sets",
                    ImageUrl = "/images/workouts/bench_press.gif"
                },
                new Workout
                {
                    WorkoutId = 2,
                    MuscleGroup = "Chest",
                    ExerciseName = "Incline Dumbbell Press",
                    Description = "Incline bench dumbbell press",
                    Repetitions = "3-4 sets x 6-8 reps",
                    ImageUrl = "/images/workouts/incline_dumbbell_press.gif"
                },
                new Workout
                {
                    WorkoutId = 3,
                    MuscleGroup = "Chest",
                    ExerciseName = "Cable Flys",
                    Description = "Cable chest flys",
                    Repetitions = "3 sets x 8-10 reps",
                    ImageUrl = "/images/workouts/cable_flys.gif"
                },
                new Workout
                {
                    WorkoutId = 4,
                    MuscleGroup = "Chest",
                    ExerciseName = "Push-ups",
                    Description = "Standard push-ups",
                    Repetitions = "Till failure",
                    ImageUrl = "/images/workouts/push_ups.gif"
                },
                // Triceps
                new Workout
                {
                    WorkoutId = 5,
                    MuscleGroup = "Triceps",
                    ExerciseName = "Cable with Rope Tricep Extensions",
                    Description = "Tricep cable extensions",
                    Repetitions = "4 sets x 10 reps",
                    ImageUrl = "/images/workouts/tricep_extensions.gif"
                },
                new Workout
                {
                    WorkoutId = 6,
                    MuscleGroup = "Triceps",
                    ExerciseName = "Dumbbell Overhead Tricep Extension",
                    Description = "Overhead tricep extensions",
                    Repetitions = "3 sets x 10 reps",
                    ImageUrl = "/images/workouts/overhead_tricep_extension.gif"
                },
                new Workout
                {
                    WorkoutId = 7,
                    MuscleGroup = "Triceps",
                    ExerciseName = "Dips",
                    Description = "Tricep dips",
                    Repetitions = "3 sets till failure",
                    ImageUrl = "/images/workouts/dips.gif"
                },
                // Biceps
                new Workout
                {
                    WorkoutId = 8,
                    MuscleGroup = "Biceps",
                    ExerciseName = "Bicep Curls with Dumbbell",
                    Description = "Standard dumbbell bicep curls",
                    Repetitions = "3 sets x 8-10 reps",
                    ImageUrl = "/images/workouts/bicep_curls.gif"
                },
                new Workout
                {
                    WorkoutId = 9,
                    MuscleGroup = "Biceps",
                    ExerciseName = "Hammer Curls with Dumbbell",
                    Description = "Hammer-style dumbbell curls",
                    Repetitions = "3 sets x 10 reps",
                    ImageUrl = "/images/workouts/hammer_curls.gif"
                },
                // Back
                new Workout
                {
                    WorkoutId = 10,
                    MuscleGroup = "Back",
                    ExerciseName = "Lat Pull Downs",
                    Description = "Wide-grip lat pull-downs",
                    Repetitions = "4 sets x 8-10 reps",
                    ImageUrl = "/images/workouts/lat_pull_downs.gif"
                },
                new Workout
                {
                    WorkoutId = 11,
                    MuscleGroup = "Back",
                    ExerciseName = "Seated Cable Rows",
                    Description = "Cable rows for back",
                    Repetitions = "3-4 sets x 8-10 reps",
                    ImageUrl = "/images/workouts/seated_cable_rows.gif"
                },
                // Legs
                new Workout
                {
                    WorkoutId = 12,
                    MuscleGroup = "Legs",
                    ExerciseName = "Squats",
                    Description = "Barbell squats",
                    Repetitions = "3-4 sets x 6-10 reps",
                    ImageUrl = "/images/workouts/squats.gif"
                },
                new Workout
                {
                    WorkoutId = 13,
                    MuscleGroup = "Legs",
                    ExerciseName = "Leg Extensions",
                    Description = "Leg extension machine",
                    Repetitions = "3-4 sets x 10 reps",
                    ImageUrl = "/images/workouts/leg_extensions.gif"
                },
                // Abs
                new Workout
                {
                    WorkoutId = 14,
                    MuscleGroup = "Abs",
                    ExerciseName = "Sit-ups",
                    Description = "Standard sit-ups",
                    Repetitions = "2 sets x 20-30 reps",
                    ImageUrl = "/images/workouts/sit_ups.gif"
                },
                new Workout
                {
                    WorkoutId = 15,
                    MuscleGroup = "Abs",
                    ExerciseName = "Planks",
                    Description = "Hold plank position",
                    Repetitions = "2 sets x 1 minute",
                    ImageUrl = "/images/workouts/planks.gif"
                }

            );
             
        }

    }
}
