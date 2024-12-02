using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StajTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedUserProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParticipantId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Users",
                type: "uuid",
                nullable: true);
        }
    }
}
