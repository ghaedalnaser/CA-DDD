using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Movers_MoverId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_MoverId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "MoverId",
                table: "ActivityLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "MoverId",
                table: "Missions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MoverId",
                table: "Missions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MoverId",
                table: "ActivityLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_MoverId",
                table: "ActivityLogs",
                column: "MoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Movers_MoverId",
                table: "ActivityLogs",
                column: "MoverId",
                principalTable: "Movers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
