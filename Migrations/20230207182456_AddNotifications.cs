using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserListsMVC.Migrations
{
    public partial class AddNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowlistItem_UserList<FollowlistItem>_UserListId",
                table: "FollowlistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList<CustomListItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<CustomListItem>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList<FollowlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<FollowlistItem>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList<WishlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<WishlistItem>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowlistItem",
                table: "FollowlistItem");

            migrationBuilder.DropColumn(
                name: "ViewCounter",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "FollowlistItem",
                newName: "FollowlistItems");

            migrationBuilder.RenameIndex(
                name: "IX_FollowlistItem_UserListId",
                table: "FollowlistItems",
                newName: "IX_FollowlistItems_UserListId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowlistItem_ItemId",
                table: "FollowlistItems",
                newName: "IX_FollowlistItems_ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserList<WishlistItem>",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserList<FollowlistItem>",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserList<CustomListItem>",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowlistItems",
                table: "FollowlistItems",
                column: "ListItemId");

            migrationBuilder.CreateTable(
                name: "FollowedNotif",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserIdFrom = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<string>(type: "text", nullable: false),
                    ItemTitle = table.Column<string>(type: "text", nullable: false),
                    ItemContentType = table.Column<int>(type: "integer", nullable: false),
                    CommentId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "text", nullable: false),
                    CommentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true),
                    SentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedNotif", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowedNotif_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlannedDateNotif",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<string>(type: "text", nullable: false),
                    ItemTitle = table.Column<string>(type: "text", nullable: false),
                    ListName = table.Column<string>(type: "text", nullable: false),
                    ListType = table.Column<int>(type: "integer", nullable: false),
                    ListContentType = table.Column<int>(type: "integer", nullable: false),
                    PlannedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true),
                    SentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedDateNotif", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedDateNotif_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RepliedNotif",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserIdFrom = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<string>(type: "text", nullable: false),
                    ItemTitle = table.Column<string>(type: "text", nullable: false),
                    ItemContentType = table.Column<int>(type: "integer", nullable: false),
                    ReplyId = table.Column<int>(type: "integer", nullable: false),
                    ReplyText = table.Column<string>(type: "text", nullable: false),
                    ReplyTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true),
                    SentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepliedNotif", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepliedNotif_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowedNotif_ApplicationUserId",
                table: "FollowedNotif",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedDateNotif_ApplicationUserId",
                table: "PlannedDateNotif",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepliedNotif_ApplicationUserId",
                table: "RepliedNotif",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowlistItems_UserList<FollowlistItem>_UserListId",
                table: "FollowlistItems",
                column: "UserListId",
                principalTable: "UserList<FollowlistItem>",
                principalColumn: "UserListId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserList<CustomListItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<CustomListItem>",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserList<FollowlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<FollowlistItem>",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserList<WishlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<WishlistItem>",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowlistItems_UserList<FollowlistItem>_UserListId",
                table: "FollowlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList<CustomListItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<CustomListItem>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList<FollowlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<FollowlistItem>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList<WishlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<WishlistItem>");

            migrationBuilder.DropTable(
                name: "FollowedNotif");

            migrationBuilder.DropTable(
                name: "PlannedDateNotif");

            migrationBuilder.DropTable(
                name: "RepliedNotif");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowlistItems",
                table: "FollowlistItems");

            migrationBuilder.RenameTable(
                name: "FollowlistItems",
                newName: "FollowlistItem");

            migrationBuilder.RenameIndex(
                name: "IX_FollowlistItems_UserListId",
                table: "FollowlistItem",
                newName: "IX_FollowlistItem_UserListId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowlistItems_ItemId",
                table: "FollowlistItem",
                newName: "IX_FollowlistItem_ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserList<WishlistItem>",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserList<FollowlistItem>",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserList<CustomListItem>",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ViewCounter",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowlistItem",
                table: "FollowlistItem",
                column: "ListItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowlistItem_UserList<FollowlistItem>_UserListId",
                table: "FollowlistItem",
                column: "UserListId",
                principalTable: "UserList<FollowlistItem>",
                principalColumn: "UserListId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserList<CustomListItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<CustomListItem>",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserList<FollowlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<FollowlistItem>",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserList<WishlistItem>_AspNetUsers_ApplicationUserId",
                table: "UserList<WishlistItem>",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
