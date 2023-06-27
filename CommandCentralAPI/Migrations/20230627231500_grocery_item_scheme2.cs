using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommandCentralAPI.Migrations
{
    /// <inheritdoc />
    public partial class grocery_item_scheme2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_grocery_list_household_household_idid",
                table: "grocery_list");

            migrationBuilder.RenameColumn(
                name: "household_idid",
                table: "grocery_list",
                newName: "household_id");

            migrationBuilder.RenameIndex(
                name: "IX_grocery_list_household_idid",
                table: "grocery_list",
                newName: "IX_grocery_list_household_id");

            migrationBuilder.AddForeignKey(
                name: "FK_grocery_list_household_household_id",
                table: "grocery_list",
                column: "household_id",
                principalTable: "household",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_grocery_list_household_household_id",
                table: "grocery_list");

            migrationBuilder.RenameColumn(
                name: "household_id",
                table: "grocery_list",
                newName: "household_idid");

            migrationBuilder.RenameIndex(
                name: "IX_grocery_list_household_id",
                table: "grocery_list",
                newName: "IX_grocery_list_household_idid");

            migrationBuilder.AddForeignKey(
                name: "FK_grocery_list_household_household_idid",
                table: "grocery_list",
                column: "household_idid",
                principalTable: "household",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
