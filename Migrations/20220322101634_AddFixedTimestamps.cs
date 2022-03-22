using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class AddFixedTimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 3, 10, 11, 51, 38, 449, DateTimeKind.Unspecified).AddTicks(151));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 3, 10, 11, 53, 38, 449, DateTimeKind.Unspecified).AddTicks(151));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 3, 10, 11, 54, 38, 449, DateTimeKind.Unspecified).AddTicks(151));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 3, 10, 11, 57, 38, 449, DateTimeKind.Unspecified).AddTicks(151));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
