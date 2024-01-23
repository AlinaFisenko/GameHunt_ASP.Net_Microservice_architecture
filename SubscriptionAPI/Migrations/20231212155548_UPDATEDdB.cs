using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UPDATEDdB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "subscriptionPrice",
                table: "Subscriptions",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "subscriptionId",
                keyValue: (ushort)1,
                column: "subscriptionPrice",
                value: 9.9900000000000002);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "subscriptionId",
                keyValue: (ushort)2,
                column: "subscriptionPrice",
                value: 19.989999999999998);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "subscriptionPrice",
                table: "Subscriptions",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "subscriptionId",
                keyValue: (ushort)1,
                column: "subscriptionPrice",
                value: 9.99m);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "subscriptionId",
                keyValue: (ushort)2,
                column: "subscriptionPrice",
                value: 19.99m);
        }
    }
}
