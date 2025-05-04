using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetStats.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateWorkoutDayEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workout_days",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    workout_week_id = table.Column<Guid>(type: "uuid", nullable: false),
                    workout_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout_days", x => x.id);
                    table.ForeignKey(
                        name: "fk_workout_days_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workout_days_workout_weeks_workout_week_id",
                        column: x => x.workout_week_id,
                        principalTable: "workout_weeks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_workout_days_user_id_workout_week_id_workout_date",
                table: "workout_days",
                columns: ["user_id", "workout_week_id", "workout_date"],
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_workout_days_workout_week_id",
                table: "workout_days",
                column: "workout_week_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workout_days");
        }
    }
}
