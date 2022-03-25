using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class UpdateSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Group for Noroff class of 2021 alumni", "Noroff 2021 Alumni" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Group for Noroff class of 2022 alumni", "Noroff 2022 Alumni" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A private group", "Test Private Group" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bio", "FunFact", "Name", "Status" },
                values: new object[] { "Patience is a virtue, and I'm learning patience. It's a tough lesson.", "When I was in college, I wanted to be involved in things that would change the world.", "John Doe", "Attending Experis Academy courses at Noroff" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bio", "FunFact", "Name", "Status" },
                values: new object[] { "I'd rather be optimistic and wrong than pessimistic and right.", "I would like to die on Mars. Just not on impact.", "Jane Doe", "Graduated from Experis Academy / Unemployed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "The first test group", "Test Group 1" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "The second test group", "Test Group 2" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "The third test group", "Test Group 3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bio", "FunFact", "Name", "Status" },
                values: new object[] { "Some say i might not be real at all", "What is life?", "Tester", "Online" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bio", "FunFact", "Name", "Status" },
                values: new object[] { "Nothing to see here.", "The brand name Spam is a combination of spice and ham", "RandomPerson", "Online" });
        }
    }
}
