using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clone_Main_Project_0710.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase_image_iformfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceImage",
                table: "UserImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SourceImage",
                table: "UserImages",
                type: "longblob",
                nullable: false);
        }
    }
}
