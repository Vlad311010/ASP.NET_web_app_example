using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class heroInstanceModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Handler",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HeroInstances_HeroId",
                table: "HeroInstances",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroInstances_Heroes_HeroId",
                table: "HeroInstances",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroInstances_Users_OwnerId",
                table: "HeroInstances",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroInstances_Heroes_HeroId",
                table: "HeroInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroInstances_Users_OwnerId",
                table: "HeroInstances");

            migrationBuilder.DropIndex(
                name: "IX_HeroInstances_HeroId",
                table: "HeroInstances");

            migrationBuilder.DropColumn(
                name: "Handler",
                table: "Items");

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
    }
}
