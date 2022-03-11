using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    public partial class AddPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Groups_TargetGroupId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Topics_TargetTopicId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Users_SenderId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Users_TargetUserId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_Post_TargetUserId",
                table: "Posts",
                newName: "IX_Posts_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_TargetTopicId",
                table: "Posts",
                newName: "IX_Posts_TargetTopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_TargetGroupId",
                table: "Posts",
                newName: "IX_Posts_TargetGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_SenderId",
                table: "Posts",
                newName: "IX_Posts_SenderId");

            migrationBuilder.AddColumn<int>(
                name: "ReplyParentId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "ReplyParentId", "SenderId", "TargetGroupId", "TargetTopicId", "TargetUserId", "Timestamp", "Title" },
                values: new object[] { 1, "Do you use Azure App Service? I have no clue.", null, 1, null, 1, null, new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5868), "Deploying ASP NET Core API to Azure" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "ReplyParentId", "SenderId", "TargetGroupId", "TargetTopicId", "TargetUserId", "Timestamp", "Title" },
                values: new object[] { 4, "I don't get it.", null, 1, 1, null, null, new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5909), "What is the purpose of this group?" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "FunFact", "Name", "PictureURL", "Status" },
                values: new object[] { 2, "Nothing to see here.", "The brand name Spam is a combination of spice and ham", "RandomPerson", "https://cdn.icon-icons.com/icons2/1378/PNG/512/avatardefault_92824.png", "Online" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "ReplyParentId", "SenderId", "TargetGroupId", "TargetTopicId", "TargetUserId", "Timestamp", "Title" },
                values: new object[] { 2, "Yes. Do not use API Management though.", 1, 2, null, null, null, new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5904), "Deploying ASP NET Core API to Azure" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "ReplyParentId", "SenderId", "TargetGroupId", "TargetTopicId", "TargetUserId", "Timestamp", "Title" },
                values: new object[] { 3, "I can answer your questions.", null, 2, null, null, 1, new DateTime(2022, 3, 11, 10, 11, 0, 843, DateTimeKind.Local).AddTicks(5906), "Do you need help with the deployment" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ReplyParentId",
                table: "Posts",
                column: "ReplyParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Groups_TargetGroupId",
                table: "Posts",
                column: "TargetGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ReplyParentId",
                table: "Posts",
                column: "ReplyParentId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Topics_TargetTopicId",
                table: "Posts",
                column: "TargetTopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_SenderId",
                table: "Posts",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_TargetUserId",
                table: "Posts",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Groups_TargetGroupId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ReplyParentId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Topics_TargetTopicId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_SenderId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_TargetUserId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ReplyParentId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ReplyParentId",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_TargetUserId",
                table: "Post",
                newName: "IX_Post_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_TargetTopicId",
                table: "Post",
                newName: "IX_Post_TargetTopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_TargetGroupId",
                table: "Post",
                newName: "IX_Post_TargetGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_SenderId",
                table: "Post",
                newName: "IX_Post_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Groups_TargetGroupId",
                table: "Post",
                column: "TargetGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Topics_TargetTopicId",
                table: "Post",
                column: "TargetTopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Users_SenderId",
                table: "Post",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Users_TargetUserId",
                table: "Post",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
