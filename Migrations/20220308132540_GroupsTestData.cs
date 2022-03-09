using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class GroupsTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupsId", "UsersId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupsId", "UsersId" },
                values: new object[] { 3, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupUser",
                keyColumns: new[] { "GroupsId", "UsersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GroupUser",
                keyColumns: new[] { "GroupsId", "UsersId" },
                keyValues: new object[] { 3, 1 });
        }
    }
}
