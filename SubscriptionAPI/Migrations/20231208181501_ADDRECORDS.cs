using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubscriptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class ADDRECORDS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "subscriptionId", "subscriptionDays", "subscriptionPrice", "subscriptionTitle" },
                values: new object[,]
                {
                    { (ushort)1, 30, 9.99m, "Basic" },
                    { (ushort)2, 100, 19.99m, "Premium" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "subscriptionId",
                keyValue: (ushort)1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "subscriptionId",
                keyValue: (ushort)2);
        }
    }
}
