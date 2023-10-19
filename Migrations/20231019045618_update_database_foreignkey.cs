using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clone_Main_Project_0710.Migrations
{
    /// <inheritdoc />
    public partial class update_database_foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ViewerFeed_Likes_SenderId",
                table: "ViewerFeed_Likes",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewerFeed_Likes_UserPostId",
                table: "ViewerFeed_Likes",
                column: "UserPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPosts_UserId",
                table: "UserPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserPostId",
                table: "UserImages",
                column: "UserPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_SourceId",
                table: "UserFriends",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_TargetId",
                table: "UserFriends",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_TargetId",
                table: "UserFollowers",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_UserId",
                table: "UserFollowers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_ViewerId",
                table: "UserComments",
                column: "ViewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_ViewerFeed_Likes_ViewerId",
                table: "UserComments",
                column: "ViewerId",
                principalTable: "ViewerFeed_Likes",
                principalColumn: "ViewerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_Users_TargetId",
                table: "UserFollowers",
                column: "TargetId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_Users_UserId",
                table: "UserFollowers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_SourceId",
                table: "UserFriends",
                column: "SourceId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_TargetId",
                table: "UserFriends",
                column: "TargetId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_UserPosts_UserPostId",
                table: "UserImages",
                column: "UserPostId",
                principalTable: "UserPosts",
                principalColumn: "UserPostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_Users_UserId",
                table: "UserImages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_Users_UserId",
                table: "UserPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewerFeed_Likes_UserPosts_UserPostId",
                table: "ViewerFeed_Likes",
                column: "UserPostId",
                principalTable: "UserPosts",
                principalColumn: "UserPostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewerFeed_Likes_Users_SenderId",
                table: "ViewerFeed_Likes",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_ViewerFeed_Likes_ViewerId",
                table: "UserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowers_Users_TargetId",
                table: "UserFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowers_Users_UserId",
                table: "UserFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_SourceId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_TargetId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserImages_UserPosts_UserPostId",
                table: "UserImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserImages_Users_UserId",
                table: "UserImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPosts_Users_UserId",
                table: "UserPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewerFeed_Likes_UserPosts_UserPostId",
                table: "ViewerFeed_Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewerFeed_Likes_Users_SenderId",
                table: "ViewerFeed_Likes");

            migrationBuilder.DropIndex(
                name: "IX_ViewerFeed_Likes_SenderId",
                table: "ViewerFeed_Likes");

            migrationBuilder.DropIndex(
                name: "IX_ViewerFeed_Likes_UserPostId",
                table: "ViewerFeed_Likes");

            migrationBuilder.DropIndex(
                name: "IX_UserPosts_UserId",
                table: "UserPosts");

            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages");

            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserPostId",
                table: "UserImages");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_SourceId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_TargetId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_TargetId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_UserId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserComments_ViewerId",
                table: "UserComments");
        }
    }
}
