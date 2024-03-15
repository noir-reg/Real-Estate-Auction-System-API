using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class EnableSomeNullableFieldsInRealEstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuctionId",
                table: "RealEstates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates",
                column: "AuctionId",
                unique: true,
                filter: "[AuctionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuctionId",
                table: "RealEstates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
