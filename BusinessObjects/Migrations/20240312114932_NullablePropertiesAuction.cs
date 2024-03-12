using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class NullablePropertiesAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartingBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinningBidId",
                table: "Auctions");

            migrationBuilder.AlterColumn<Guid>(
                name: "WinningBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StartingBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StaffId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions",
                column: "CurrentBidId",
                unique: true,
                filter: "[CurrentBidId] IS NOT NULL");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Auctions_CurrentBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartingBidId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinningBidId",
                table: "Auctions");

            migrationBuilder.AlterColumn<Guid>(
                name: "WinningBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StartingBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StaffId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentBidId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
        }
    }
}
