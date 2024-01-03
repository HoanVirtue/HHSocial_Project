using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHSocialNetwork_Project.Migrations
{
    /// <inheritdoc />
    public partial class add_notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SourceId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TargetId = table.Column<Guid>(type: "char(36)", nullable: true),
                    UserPostId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "UserPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SourceId",
                table: "Notifications",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TargetId",
                table: "Notifications",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserPostId",
                table: "Notifications",
                column: "UserPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
