using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetStats.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateTrainingProgramCycleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "training_program_cycles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_program_id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_cycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    sequence = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_program_cycles", x => x.id);
                    table.ForeignKey(
                        name: "fk_training_program_cycles_training_cycles_training_cycle_id",
                        column: x => x.training_cycle_id,
                        principalTable: "training_cycles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_training_program_cycles_training_programs_training_program_~",
                        column: x => x.training_program_id,
                        principalTable: "training_programs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_training_program_cycles_training_cycle_id",
                table: "training_program_cycles",
                column: "training_cycle_id");

            migrationBuilder.CreateIndex(
                name: "ix_training_program_cycles_training_program_id_training_cycle_~",
                table: "training_program_cycles",
                columns: new[] { "training_program_id", "training_cycle_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "training_program_cycles");
        }
    }
}
