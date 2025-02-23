using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsUserToList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ToDoLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_UserId",
                table: "ToDoLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoLists_AspNetUsers_UserId",
                table: "ToDoLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoLists_AspNetUsers_UserId",
                table: "ToDoLists");

            migrationBuilder.DropIndex(
                name: "IX_ToDoLists_UserId",
                table: "ToDoLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoLists");
        }
    }
}
