using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHSocialNetwork_Project.Migrations
{
    /// <inheritdoc />
    public partial class update_database_final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.DropTable(
                name: "ViewerFeed_Likes");

            migrationBuilder.DropColumn(
                name: "ImagePost",
                table: "UserPosts");

            migrationBuilder.CreateTable(
                name: "ViewerComments",
                columns: table => new
                {
                    ViewerCommentId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SenderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserPostId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewerComments", x => x.ViewerCommentId);
                    table.ForeignKey(
                        name: "FK_ViewerComments_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "UserPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewerComments_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ViewerLikes",
                columns: table => new
                {
                    ViewerLikeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SenderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserPostId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LikePost = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewerLikes", x => x.ViewerLikeId);
                    table.ForeignKey(
                        name: "FK_ViewerLikes_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "UserPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewerLikes_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommentDetails",
                columns: table => new
                {
                    ViewerCommentId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Content = table.Column<string>(type: "longtext", maxLength: 2147483647, nullable: true),
                    ImageComment = table.Column<string>(type: "longtext", maxLength: 2147483647, nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentDetails", x => x.ViewerCommentId);
                    table.ForeignKey(
                        name: "FK_CommentDetails_ViewerComments_ViewerCommentId",
                        column: x => x.ViewerCommentId,
                        principalTable: "ViewerComments",
                        principalColumn: "ViewerCommentId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerComments_SenderId",
                table: "ViewerComments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerComments_UserPostId",
                table: "ViewerComments",
                column: "UserPostId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerLikes_SenderId",
                table: "ViewerLikes",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerLikes_UserPostId",
                table: "ViewerLikes",
                column: "UserPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentDetails");

            migrationBuilder.DropTable(
                name: "ViewerLikes");

            migrationBuilder.DropTable(
                name: "ViewerComments");

            migrationBuilder.AddColumn<string>(
                name: "ImagePost",
                table: "UserPosts",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ViewerFeed_Likes",
                columns: table => new
                {
                    ViewerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SenderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserPostId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LikePost = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewerFeed_Likes", x => x.ViewerId);
                    table.ForeignKey(
                        name: "FK_ViewerFeed_Likes_UserPosts_UserPostId",
                        column: x => x.UserPostId,
                        principalTable: "UserPosts",
                        principalColumn: "UserPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewerFeed_Likes_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ViewerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ImageComment = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_UserComments_ViewerFeed_Likes_ViewerId",
                        column: x => x.ViewerId,
                        principalTable: "ViewerFeed_Likes",
                        principalColumn: "ViewerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_ViewerId",
                table: "UserComments",
                column: "ViewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerFeed_Likes_SenderId",
                table: "ViewerFeed_Likes",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerFeed_Likes_UserPostId",
                table: "ViewerFeed_Likes",
                column: "UserPostId");
        }
    }
}
