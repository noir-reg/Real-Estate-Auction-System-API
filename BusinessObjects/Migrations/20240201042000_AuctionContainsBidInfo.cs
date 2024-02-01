using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AuctionContainsBidInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBid",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartingBid",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinningBid",
                table: "Auctions");

            migrationBuilder.AlterColumn<string>(
                name: "CitizenId",
                table: "Users",
                type: "nvarchar(12)",
                fixedLength: true,
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(12)",
                oldFixedLength: true,
                oldMaxLength: 12);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StartingBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WinningBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions",
                column: "CurrentBidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartingBidId",
                table: "Auctions",
                column: "StartingBidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_WinningBidId",
                table: "Auctions",
                column: "WinningBidId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_CurrentBidId",
                table: "Auctions",
                column: "CurrentBidId",
                principalTable: "Bids",
                principalColumn: "BidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_StartingBidId",
                table: "Auctions",
                column: "StartingBidId",
                principalTable: "Bids",
                principalColumn: "BidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_WinningBidId",
                table: "Auctions",
                column: "WinningBidId",
                principalTable: "Bids",
                principalColumn: "BidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_StartingBidId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_WinningBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartingBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinningBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartingBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinningBidId",
                table: "Auctions");

            migrationBuilder.AlterColumn<string>(
                name: "CitizenId",
                table: "Users",
                type: "nchar(12)",
                fixedLength: true,
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldFixedLength: true,
                oldMaxLength: 12);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBid",
                table: "Auctions",
                type: "decimal(18,0)",
                precision: 18,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StartingBid",
                table: "Auctions",
                type: "decimal(18,0)",
                precision: 18,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WinningBid",
                table: "Auctions",
                type: "decimal(18,0)",
                precision: 18,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
