using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clone_Main_Project_0710.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase_column_null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageData",
                table: "UserImages",
                type: "longtext",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 2147483647);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageData",
                table: "UserImages",
                type: "longtext",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 2147483647,
                oldNullable: true);
        }
    }
}
