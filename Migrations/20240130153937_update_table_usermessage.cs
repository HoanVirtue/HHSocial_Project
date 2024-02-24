using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHSocialNetwork_Project.Migrations
{
    /// <inheritdoc />
    public partial class update_table_usermessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "UserMessages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "UserMessages");
        }
    }
}
