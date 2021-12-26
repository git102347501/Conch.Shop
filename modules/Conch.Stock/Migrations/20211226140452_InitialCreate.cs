using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conch.Stock.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockGoods",
                columns: table => new
                {
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodsName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    FreezeNum = table.Column<int>(type: "int", nullable: false),
                    StockMasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockGoods", x => new { x.StockId, x.GoodsId });
                    table.ForeignKey(
                        name: "FK_StockGoods_StockMaster_StockMasterId",
                        column: x => x.StockMasterId,
                        principalTable: "StockMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockGoods_StockMasterId",
                table: "StockGoods",
                column: "StockMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockGoods");

            migrationBuilder.DropTable(
                name: "StockMaster");
        }
    }
}
