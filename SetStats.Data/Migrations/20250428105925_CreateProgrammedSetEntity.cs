using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetStats.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateProgrammedSetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "programmed_sets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workout_day_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exercise_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    percentage_of_training_max = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    target_reps = table.Column<int>(type: "integer", nullable: false),
                    set_order = table.Column<int>(type: "integer", nullable: false),
                    is_a_m_r_a_p = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_programmed_sets", x => x.id);
                    table.ForeignKey(
                        name: "fk_programmed_sets_exercise_types_exercise_type_id",
                        column: x => x.exercise_type_id,
                        principalTable: "exercise_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_programmed_sets_workout_days_workout_day_id",
                        column: x => x.workout_day_id,
                        principalTable: "workout_days",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_programmed_sets_exercise_type_id",
                table: "programmed_sets",
                column: "exercise_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_programmed_sets_workout_day_id_exercise_type_id_set_order",
                table: "programmed_sets",
                columns: new[] { "workout_day_id", "exercise_type_id", "set_order" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "programmed_sets");
        }
    }
}
