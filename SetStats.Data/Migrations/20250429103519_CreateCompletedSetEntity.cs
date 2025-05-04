using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetStats.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateCompletedSetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "completed_sets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    programmed_set_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actual_weight = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    actual_reps = table.Column<int>(type: "integer", nullable: false),
                    rate_of_perceived_exertion = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_completed_sets", x => x.id);
                    table.ForeignKey(
                        name: "fk_completed_sets_programmed_sets_programmed_set_id",
                        column: x => x.programmed_set_id,
                        principalTable: "programmed_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_completed_sets_programmed_set_id",
                table: "completed_sets",
                column: "programmed_set_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "completed_sets");
        }
    }
}
