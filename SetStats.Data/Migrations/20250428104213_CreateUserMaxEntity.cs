using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetStats.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserMaxEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "rounding_factor",
                table: "training_cycles",
                type: "numeric(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateTable(
                name: "user_maxes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exercise_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_cycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actual_one_rep_max = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    estimated_one_rep_max = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    rounding_factor = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    date_recorded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_maxes", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_maxes_exercise_types_exercise_type_id",
                        column: x => x.exercise_type_id,
                        principalTable: "exercise_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_maxes_training_cycles_training_cycle_id",
                        column: x => x.training_cycle_id,
                        principalTable: "training_cycles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_maxes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_maxes_exercise_type_id",
                table: "user_maxes",
                column: "exercise_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_maxes_training_cycle_id",
                table: "user_maxes",
                column: "training_cycle_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_maxes_user_id_exercise_type_id_training_cycle_id",
                table: "user_maxes",
                columns: ["user_id", "exercise_type_id", "training_cycle_id"],
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_maxes");

            migrationBuilder.AlterColumn<decimal>(
                name: "rounding_factor",
                table: "training_cycles",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,2)",
                oldPrecision: 4,
                oldScale: 2);
        }
    }
}
