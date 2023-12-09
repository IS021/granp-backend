using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GranpAPI.Migrations
{
    /// <inheritdoc />
    public partial class Gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Professionals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");
        }
    }
}
