using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapsuleIdentity.Data.Migrations
{
    public partial class AjoutRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Vetement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Vetement");
        }
    }
}
