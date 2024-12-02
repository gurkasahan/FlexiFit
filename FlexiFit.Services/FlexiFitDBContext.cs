using FlexiFit.Entities;
using FlexiFit.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexiFit.Services
{
    /// <summary>
    /// Principal Author: [Your Name]
    /// This class represents the DbContext for the FlexiFit application. It defines the database schema,
    /// relationships, and seed data for the application's database.
    /// </summary>
    public class FlexiFitDBContext : DbContext
    {
        /// <summary>
        /// Constructor for initializing the FlexiFitDBContext with the provided DbContext options.
        /// </summary>
        /// <param name="options">Options for configuring the DbContext.</param>
        public FlexiFitDBContext(DbContextOptions<FlexiFitDBContext> options) : base(options) { }

        /// <summary>
        /// Represents the Members table in the database.
        /// </summary>
        public DbSet<Member> Members { get; set; }

        /// <summary>
        /// Represents the Classes table in the database.
        /// </summary>
        public DbSet<Class> Classes { get; set; }

        /// <summary>
        /// Represents the Bookings table in the database.
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }

        /// <summary>
        /// Represents the Workouts table in the database.
        /// </summary>
        public DbSet<Workout> Workouts { get; set; }

        /// <summary>
        /// Configures relationships between entities and seeds initial data for the database.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance for configuring the entity framework model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships between Booking, Member, and Class entities.
            // Cascade deletion ensures that related bookings are removed when a member or class is deleted.
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Class)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data for the Classes table.
            // This ensures the application starts with pre-defined classes like Yoga, Pilates, etc.
            modelBuilder.Entity<Class>().HasData(
                new Class { ClassId = 1, ClassName = "Yoga" },
                new Class { ClassId = 2, ClassName = "Pilates" },
                new Class { ClassId = 3, ClassName = "Zumba" },
                new Class { ClassId = 4, ClassName = "Strength-Training" }
            );

            // Seed initial data for the Workouts table.
            // This provides predefined workout options categorized by muscle group for users.
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
                // Triceps workouts
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
                // Biceps workouts
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
                // Back workouts
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
                // Legs workouts
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
                // Abs workouts
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
