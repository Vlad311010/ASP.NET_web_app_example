using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class dbModelsRefactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Handler",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Page",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Inventories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HeroInstances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_UserId",
                table: "Inventories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroInstances_UserId",
                table: "HeroInstances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroInstances_Users_UserId",
                table: "HeroInstances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Users_UserId",
                table: "Inventories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroInstances_Users_UserId",
                table: "HeroInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Users_UserId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_UserId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_HeroInstances_UserId",
                table: "HeroInstances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HeroInstances");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Handler",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Page",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
