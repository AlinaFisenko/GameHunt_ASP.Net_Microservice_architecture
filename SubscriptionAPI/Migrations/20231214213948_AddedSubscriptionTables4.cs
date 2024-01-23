using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubscriptionTables4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ushort>(
                name: "id_subscription",
                table: "User_Subscriptions",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<ushort>(
                name: "id_subscription",
                table: "Payment_Historys",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id_subscription",
                table: "User_Subscriptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "id_subscription",
                table: "Payment_Historys",
                type: "int",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");
        }
    }
}
