using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conch.Order.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderGoods",
                columns: table => new
                {
                    OrderMasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodsName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGoods", x => new { x.OrderMasterId, x.GoodsId });
                    table.ForeignKey(
                        name: "FK_OrderGoods_OrderMaster_OrderMasterId",
                        column: x => x.OrderMasterId,
                        principalTable: "OrderMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderGoods");

            migrationBuilder.DropTable(
                name: "OrderMaster");
        }
    }
}
