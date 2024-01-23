using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Devs_Orders_id_order",
                table: "Order_Devs");

            migrationBuilder.DropIndex(
                name: "IX_Order_Devs_id_order",
                table: "Order_Devs");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Order_Devs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id",
                table: "Order_Devs");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Devs_id_order",
                table: "Order_Devs",
                column: "id_order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Devs_Orders_id_order",
                table: "Order_Devs",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
