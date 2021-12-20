using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conch.Order.Migrations
{
    public partial class Up1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderGoods",
                newName: "OrderMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGoods_OrderMaster_OrderMasterId",
                table: "OrderGoods",
                column: "OrderMasterId",
                principalTable: "OrderMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderGoods_OrderMaster_OrderMasterId",
                table: "OrderGoods");

            migrationBuilder.RenameColumn(
                name: "OrderMasterId",
                table: "OrderGoods",
                newName: "OrderId");
        }
    }
}
