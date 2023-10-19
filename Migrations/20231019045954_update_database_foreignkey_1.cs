using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clone_Main_Project_0710.Migrations
{
    /// <inheritdoc />
    public partial class update_database_foreignkey_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_ViewerFeed_Likes_ViewerId",
                table: "UserComments",
                column: "ViewerId",
                principalTable: "ViewerFeed_Likes",
                principalColumn: "ViewerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_Users_TargetId",
                table: "UserFollowers",
                column: "TargetId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_Users_UserId",
                table: "UserFollowers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_SourceId",
                table: "UserFriends",
                column: "SourceId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_TargetId",
                table: "UserFriends",
                column: "TargetId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_UserPosts_UserPostId",
                table: "UserImages",
                column: "UserPostId",
                principalTable: "UserPosts",
                principalColumn: "UserPostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_Users_UserId",
                table: "UserImages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_Users_UserId",
                table: "UserPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewerFeed_Likes_UserPosts_UserPostId",
                table: "ViewerFeed_Likes",
                column: "UserPostId",
                principalTable: "UserPosts",
                principalColumn: "UserPostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewerFeed_Likes_Users_SenderId",
                table: "ViewerFeed_Likes",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
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
    }
}
