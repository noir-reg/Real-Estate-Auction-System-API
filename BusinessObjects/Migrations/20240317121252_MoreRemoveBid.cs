using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class MoreRemoveBid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_WinningBidBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinningBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinningBidBidId",
                table: "Auctions");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                column: "AuctionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids");

            migrationBuilder.AddColumn<Guid>(
                name: "WinningBidBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_WinningBidBidId",
                table: "Auctions",
                column: "WinningBidBidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_WinningBidBidId",
                table: "Auctions",
                column: "WinningBidBidId",
                principalTable: "Bids",
                principalColumn: "BidId");
        }
    }
}
