using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHSocialNetwork_Project.Migrations
{
    /// <inheritdoc />
    public partial class update12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblDisables",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Disable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LogTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDisables", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_tblDisables_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDisables");
        }
    }
}
