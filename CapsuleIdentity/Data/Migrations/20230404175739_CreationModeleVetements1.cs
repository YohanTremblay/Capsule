﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapsuleIdentity.Data.Migrations
{
    public partial class CreationModeleVetements1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProprietaireId",
                table: "Vetement",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProprietaireId",
                table: "Vetement");
        }
    }
}
