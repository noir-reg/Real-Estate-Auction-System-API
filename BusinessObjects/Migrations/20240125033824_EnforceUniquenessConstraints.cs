using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class EnforceUniquenessConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_CitizenId_Email_Username",
                table: "Users",
                columns: new[] { "CitizenId", "Email", "Username" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CitizenId_Email_Username",
                table: "Users");
        }
    }
}
