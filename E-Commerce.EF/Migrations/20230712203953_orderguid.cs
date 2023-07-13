using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.EF.Migrations
{
    /// <inheritdoc />
    public partial class orderguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_orders_OrderId1",
                table: "orderItem");

            migrationBuilder.DropIndex(
                name: "IX_orderItem_OrderId1",
                table: "orderItem");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "orderItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "orderItem",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_OrderId",
                table: "orderItem",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_orders_OrderId",
                table: "orderItem",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_orders_OrderId",
                table: "orderItem");

            migrationBuilder.DropIndex(
                name: "IX_orderItem_OrderId",
                table: "orderItem");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "orderItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "orderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_OrderId1",
                table: "orderItem",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_orders_OrderId1",
                table: "orderItem",
                column: "OrderId1",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
