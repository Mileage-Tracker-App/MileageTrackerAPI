using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MileageTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogItems_Logs_LogId",
                table: "LogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Sessions_SessionId",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LogId",
                table: "LogItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LogItems_Logs_LogId",
                table: "LogItems",
                column: "LogId",
                principalTable: "Logs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Sessions_SessionId",
                table: "Logs",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogItems_Logs_LogId",
                table: "LogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Sessions_SessionId",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Logs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LogId",
                table: "LogItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LogItems_Logs_LogId",
                table: "LogItems",
                column: "LogId",
                principalTable: "Logs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Sessions_SessionId",
                table: "Logs",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
