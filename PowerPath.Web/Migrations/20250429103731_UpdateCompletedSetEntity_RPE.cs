using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerPath.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompletedSetEntity_RPE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rate_of_perceived_exertion",
                table: "completed_sets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rate_of_perceived_exertion",
                table: "completed_sets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
