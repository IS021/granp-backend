using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GranpAPI.Migrations
{
    /// <inheritdoc />
    public partial class UniqueUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Professionals_UserId",
                table: "Professionals",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Professionals_UserId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");
        }
    }
}
