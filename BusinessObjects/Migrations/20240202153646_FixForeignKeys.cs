using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class FixForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_RealEstates_AuctionId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_AuctionId",
                table: "Auctions");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AdminId",
                table: "Auctions",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_RealEstateId",
                table: "Auctions",
                column: "RealEstateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_RealEstates_RealEstateId",
                table: "Auctions",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "RealEstateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_AdminId",
                table: "Auctions",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_RealEstates_RealEstateId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_AdminId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_AdminId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_RealEstateId",
                table: "Auctions");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_RealEstates_AuctionId",
                table: "Auctions",
                column: "AuctionId",
                principalTable: "RealEstates",
                principalColumn: "RealEstateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_AuctionId",
                table: "Auctions",
                column: "AuctionId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
