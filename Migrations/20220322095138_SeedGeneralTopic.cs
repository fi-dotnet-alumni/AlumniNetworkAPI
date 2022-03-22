using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class SeedGeneralTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 3, 22, 11, 51, 38, 449, DateTimeKind.Local).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 3, 22, 11, 51, 38, 449, DateTimeKind.Local).AddTicks(146));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 3, 22, 11, 51, 38, 449, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 3, 22, 11, 51, 38, 449, DateTimeKind.Local).AddTicks(151));

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "This topic is intended for general discussion", "General" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 3, 15, 10, 18, 32, 993, DateTimeKind.Local).AddTicks(8758));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 3, 15, 10, 18, 32, 993, DateTimeKind.Local).AddTicks(8799));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 3, 15, 10, 18, 32, 993, DateTimeKind.Local).AddTicks(8801));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 3, 15, 10, 18, 32, 993, DateTimeKind.Local).AddTicks(8804));
        }
    }
}
