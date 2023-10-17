using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clone_Main_Project_0710.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase_image_imagedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageData",
                table: "UserImages",
                type: "longtext",
                maxLength: 2147483647,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "UserImages");
        }
    }
}
