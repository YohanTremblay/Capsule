using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapsuleIdentity.Data.Migrations
{
    public partial class bonjourcmoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Couleur",
                table: "Vetement",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Vetement",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Couleur",
                table: "Vetement");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Vetement");
        }
    }
}
