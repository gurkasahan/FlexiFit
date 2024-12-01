using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlexiFit.Services.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    MembershipTier = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    BillingName = table.Column<string>(type: "TEXT", nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MuscleGroup = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ExerciseName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Repetitions = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.WorkoutId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BookingTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName" },
                values: new object[,]
                {
                    { 1, "Yoga" },
                    { 2, "Pilates" },
                    { 3, "Zumba" },
                    { 4, "Strength-Training" }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "WorkoutId", "Description", "ExerciseName", "ImageUrl", "MuscleGroup", "Repetitions" },
                values: new object[,]
                {
                    { 1, "Barbell bench press", "Bench Press", "/images/workouts/bench_press.jpg", "Chest", "5 reps x 5 sets" },
                    { 2, "Incline bench dumbbell press", "Incline Dumbbell Press", "/images/workouts/incline_dumbbell_press.jpg", "Chest", "3-4 sets x 6-8 reps" },
                    { 3, "Cable chest flys", "Cable Flys", "/images/workouts/cable_flys.jpg", "Chest", "3 sets x 8-10 reps" },
                    { 4, "Standard push-ups", "Push-ups", "/images/workouts/push_ups.jpg", "Chest", "Till failure" },
                    { 5, "Tricep cable extensions", "Cable with Rope Tricep Extensions", "/images/workouts/tricep_extensions.jpg", "Triceps", "4 sets x 10 reps" },
                    { 6, "Overhead tricep extensions", "Dumbbell Overhead Tricep Extension", "/images/workouts/overhead_tricep_extension.jpg", "Triceps", "3 sets x 10 reps" },
                    { 7, "Tricep dips", "Dips", "/images/workouts/dips.jpg", "Triceps", "3 sets till failure" },
                    { 8, "Standard dumbbell bicep curls", "Bicep Curls with Dumbbell", "/images/workouts/bicep_curls.jpg", "Biceps", "3 sets x 8-10 reps" },
                    { 9, "Hammer-style dumbbell curls", "Hammer Curls with Dumbbell", "/images/workouts/hammer_curls.jpg", "Biceps", "3 sets x 10 reps" },
                    { 10, "Wide-grip lat pull-downs", "Lat Pull Downs", "/images/workouts/lat_pull_downs.jpg", "Back", "4 sets x 8-10 reps" },
                    { 11, "Cable rows for back", "Seated Cable Rows", "/images/workouts/seated_cable_rows.jpg", "Back", "3-4 sets x 8-10 reps" },
                    { 12, "Barbell squats", "Squats", "/images/workouts/squats.jpg", "Legs", "3-4 sets x 6-10 reps" },
                    { 13, "Leg extension machine", "Leg Extensions", "/images/workouts/leg_extensions.jpg", "Legs", "3-4 sets x 10 reps" },
                    { 14, "Standard sit-ups", "Sit-ups", "/images/workouts/sit_ups.jpg", "Abs", "2 sets x 20-30 reps" },
                    { 15, "Hold plank position", "Planks", "/images/workouts/planks.jpg", "Abs", "2 sets x 1 minute" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClassId",
                table: "Bookings",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MemberId",
                table: "Bookings",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
