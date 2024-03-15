using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddAuctionIdToRealEsate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_RealEstates_RealEstateId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_RealEstateId",
                table: "Auctions");

            migrationBuilder.AddColumn<Guid>(
                name: "AuctionId",
                table: "RealEstates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates",
                column: "AuctionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "RealEstates");

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
        }
    }
}
