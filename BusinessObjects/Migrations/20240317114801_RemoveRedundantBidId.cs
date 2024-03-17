using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class RemoveRedundantBidId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_CurrentBidBidId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_StartingBidBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CurrentBidBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartingBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CurrentBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartingBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartingBidId",
                table: "Auctions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentBidBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartingBidBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartingBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CurrentBidBidId",
                table: "Auctions",
                column: "CurrentBidBidId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartingBidBidId",
                table: "Auctions",
                column: "StartingBidBidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_CurrentBidBidId",
                table: "Auctions",
                column: "CurrentBidBidId",
                principalTable: "Bids",
                principalColumn: "BidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_StartingBidBidId",
                table: "Auctions",
                column: "StartingBidBidId",
                principalTable: "Bids",
                principalColumn: "BidId");
        }
    }
}
