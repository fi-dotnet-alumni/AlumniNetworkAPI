using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class AddUserKeycloakId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeycloakId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeycloakId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5868));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5904));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5906));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5909));
        }
    }
}
