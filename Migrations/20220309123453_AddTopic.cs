using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class AddTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Topic_TargeTopicId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicUser_Topic_TopicsId",
                table: "TopicUser");

            migrationBuilder.DropIndex(
                name: "IX_Post_TargeTopicId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "TargeTopicId",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "This topic covers everything .NET", ".NET" });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "This topic has everything JavaScript related", "JavaScript" });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Everything React related", "React" });

            migrationBuilder.InsertData(
                table: "TopicUser",
                columns: new[] { "TopicsId", "UsersId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "TopicUser",
                columns: new[] { "TopicsId", "UsersId" },
                values: new object[] { 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Post_TargetTopicId",
                table: "Post",
                column: "TargetTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Topics_TargetTopicId",
                table: "Post",
                column: "TargetTopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUser_Topics_TopicsId",
                table: "TopicUser",
                column: "TopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Topics_TargetTopicId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicUser_Topics_TopicsId",
                table: "TopicUser");

            migrationBuilder.DropIndex(
                name: "IX_Post_TargetTopicId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DeleteData(
                table: "TopicUser",
                keyColumns: new[] { "TopicsId", "UsersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TopicUser",
                keyColumns: new[] { "TopicsId", "UsersId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.AddColumn<int>(
                name: "TargeTopicId",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_TargeTopicId",
                table: "Post",
                column: "TargeTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Topic_TargeTopicId",
                table: "Post",
                column: "TargeTopicId",
                principalTable: "Topic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUser_Topic_TopicsId",
                table: "TopicUser",
                column: "TopicsId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
