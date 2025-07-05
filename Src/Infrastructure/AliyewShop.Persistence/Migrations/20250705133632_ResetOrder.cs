using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AliyewShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ResetOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "OrderProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId1",
                table: "OrderProducts",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId1",
                table: "OrderProducts",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId1",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderId1",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderProducts");
        }
    }
}
