using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class ReviseDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_LegalDocuments_RealEstates_RealEstateId",
                table: "LegalDocuments");

            migrationBuilder.DropTable(
                name: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_AdminId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_RealEstateId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartingBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinningBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "RealEstateId",
                table: "LegalDocuments",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_LegalDocuments_RealEstateId",
                table: "LegalDocuments",
                newName: "IX_LegalDocuments_AuctionId");

            migrationBuilder.RenameColumn(
                name: "RealEstateId",
                table: "Auctions",
                newName: "OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdminUserId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentBidBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealEstateCode",
                table: "Auctions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RealEstateOwnerId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StaffUserId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartingBidBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WinningBidBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuctionMedias",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionMedias", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_AuctionMedias_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AdminId",
                table: "Auctions",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AdminUserId",
                table: "Auctions",
                column: "AdminUserId",
                unique: true,
                filter: "[AdminUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CurrentBidBidId",
                table: "Auctions",
                column: "CurrentBidBidId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_OwnerId",
                table: "Auctions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_RealEstateOwnerId",
                table: "Auctions",
                column: "RealEstateOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StaffUserId",
                table: "Auctions",
                column: "StaffUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartingBidBidId",
                table: "Auctions",
                column: "StartingBidBidId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_WinningBidBidId",
                table: "Auctions",
                column: "WinningBidBidId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionMedias_AuctionId",
                table: "AuctionMedias",
                column: "AuctionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bids_WinningBidBidId",
                table: "Auctions",
                column: "WinningBidBidId",
                principalTable: "Bids",
                principalColumn: "BidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_RealEstateOwners_OwnerId",
                table: "Auctions",
                column: "OwnerId",
                principalTable: "RealEstateOwners",
                principalColumn: "RealEstateOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_RealEstateOwners_RealEstateOwnerId",
                table: "Auctions",
                column: "RealEstateOwnerId",
                principalTable: "RealEstateOwners",
                principalColumn: "RealEstateOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_AdminUserId",
                table: "Auctions",
                column: "AdminUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_StaffUserId",
                table: "Auctions",
                column: "StaffUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalDocuments_Auctions_AuctionId",
                table: "LegalDocuments",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_CurrentBidBidId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_StartingBidBidId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bids_WinningBidBidId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_RealEstateOwners_OwnerId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_RealEstateOwners_RealEstateOwnerId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_AdminUserId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_StaffUserId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalDocuments_Auctions_AuctionId",
                table: "LegalDocuments");

            migrationBuilder.DropTable(
                name: "AuctionMedias");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_AdminId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_AdminUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CurrentBidBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_OwnerId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_RealEstateOwnerId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StaffUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartingBidBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinningBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CurrentBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "RealEstateCode",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "RealEstateOwnerId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StaffUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartingBidBidId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinningBidBidId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "LegalDocuments",
                newName: "RealEstateId");

            migrationBuilder.RenameIndex(
                name: "IX_LegalDocuments_AuctionId",
                table: "LegalDocuments",
                newName: "IX_LegalDocuments_RealEstateId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Auctions",
                newName: "RealEstateId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Transactions",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Auctions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "RealEstates",
                columns: table => new
                {
                    RealEstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    RealEstateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstates", x => x.RealEstateId);
                    table.ForeignKey(
                        name: "FK_RealEstates_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId");
                    table.ForeignKey(
                        name: "FK_RealEstates_RealEstateOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "RealEstateOwners",
                        principalColumn: "RealEstateOwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AdminId",
                table: "Auctions",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions",
                column: "CurrentBidId",
                unique: true,
                filter: "[CurrentBidId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_RealEstateId",
                table: "Auctions",
                column: "RealEstateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartingBidId",
                table: "Auctions",
                column: "StartingBidId",
                unique: true,
                filter: "[StartingBidId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_WinningBidId",
                table: "Auctions",
                column: "WinningBidId",
                unique: true,
                filter: "[WinningBidId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates",
                column: "AuctionId",
                unique: true,
                filter: "[AuctionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_OwnerId",
                table: "RealEstates",
                column: "OwnerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LegalDocuments_RealEstates_RealEstateId",
                table: "LegalDocuments",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "RealEstateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
