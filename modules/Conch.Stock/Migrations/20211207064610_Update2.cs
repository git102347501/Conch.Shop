using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conch.Stock.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreezeNum",
                table: "StockGoods",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreezeNum",
                table: "StockGoods");
        }
    }
}
