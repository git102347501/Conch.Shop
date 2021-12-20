using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conch.Stock.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StockMaster",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<Guid>(
                name: "StockMasterId",
                table: "StockGoods",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockGoods_StockMasterId",
                table: "StockGoods",
                column: "StockMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockGoods_StockMaster_StockMasterId",
                table: "StockGoods",
                column: "StockMasterId",
                principalTable: "StockMaster",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockGoods_StockMaster_StockMasterId",
                table: "StockGoods");

            migrationBuilder.DropIndex(
                name: "IX_StockGoods_StockMasterId",
                table: "StockGoods");

            migrationBuilder.DropColumn(
                name: "StockMasterId",
                table: "StockGoods");

            migrationBuilder.AlterColumn<Guid>(
                name: "Name",
                table: "StockMaster",
                type: "uuid",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);
        }
    }
}
