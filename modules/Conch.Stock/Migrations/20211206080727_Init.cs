using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conch.Stock.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockGoods",
                columns: table => new
                {
                    StockId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoodsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoodsName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Num = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockGoods", x => new { x.StockId, x.GoodsId });
                });

            migrationBuilder.CreateTable(
                name: "StockMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<Guid>(type: "uuid", maxLength: 64, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMaster", x => x.Id);
                });
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
