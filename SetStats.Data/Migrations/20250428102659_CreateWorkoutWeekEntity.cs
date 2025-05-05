using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetStats.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateWorkoutWeekEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workout_weeks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_cycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    week_number = table.Column<int>(type: "integer", nullable: false),
                    week_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout_weeks", x => x.id);
                    table.ForeignKey(
                        name: "fk_workout_weeks_training_cycles_training_cycle_id",
                        column: x => x.training_cycle_id,
                        principalTable: "training_cycles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_workout_weeks_training_cycle_id_week_number",
                table: "workout_weeks",
                columns: ["training_cycle_id", "week_number"],
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workout_weeks");
        }
    }
}
