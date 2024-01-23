using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class OrderTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id_order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_user = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    genre = table.Column<int>(type: "int", nullable: false),
                    platform = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    budget = table.Column<double>(type: "double", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    count_devs = table.Column<short>(type: "smallint", nullable: false),
                    gameplay_time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    work_condition = table.Column<int>(type: "int", nullable: false),
                    salary = table.Column<double>(type: "double", nullable: false),
                    job_title = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id_order);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "id_order", "budget", "count_devs", "date_created", "deadline", "description", "gameplay_time", "genre", "id_user", "job_title", "platform", "salary", "state", "title", "work_condition" },
                values: new object[] { 1, 1000.0, (short)1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", new TimeSpan(0, 1, 0, 0, 0), 0, "40f04aab-4afc-4984-a4bf-3c75e57d715d", 2, 0, 1000.0, 0, "Test", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
