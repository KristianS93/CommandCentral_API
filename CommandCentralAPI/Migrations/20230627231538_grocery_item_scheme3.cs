using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommandCentralAPI.Migrations
{
    /// <inheritdoc />
    public partial class grocery_item_scheme3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_grocery_list_item_grocery_list_grocery_list_idid",
                table: "grocery_list_item");

            migrationBuilder.RenameColumn(
                name: "grocery_list_idid",
                table: "grocery_list_item",
                newName: "grocery_list_id");

            migrationBuilder.RenameIndex(
                name: "IX_grocery_list_item_grocery_list_idid",
                table: "grocery_list_item",
                newName: "IX_grocery_list_item_grocery_list_id");

            migrationBuilder.AddForeignKey(
                name: "FK_grocery_list_item_grocery_list_grocery_list_id",
                table: "grocery_list_item",
                column: "grocery_list_id",
                principalTable: "grocery_list",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_grocery_list_item_grocery_list_grocery_list_id",
                table: "grocery_list_item");

            migrationBuilder.RenameColumn(
                name: "grocery_list_id",
                table: "grocery_list_item",
                newName: "grocery_list_idid");

            migrationBuilder.RenameIndex(
                name: "IX_grocery_list_item_grocery_list_id",
                table: "grocery_list_item",
                newName: "IX_grocery_list_item_grocery_list_idid");

            migrationBuilder.AddForeignKey(
                name: "FK_grocery_list_item_grocery_list_grocery_list_idid",
                table: "grocery_list_item",
                column: "grocery_list_idid",
                principalTable: "grocery_list",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
