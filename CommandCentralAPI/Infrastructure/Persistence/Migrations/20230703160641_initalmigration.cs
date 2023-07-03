using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "household",
                columns: table => new
                {
                    household_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household", x => x.household_id);
                });

            migrationBuilder.CreateTable(
                name: "grocerylist",
                columns: table => new
                {
                    grocerylist_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grocerylist", x => x.grocerylist_id);
                    table.ForeignKey(
                        name: "FK_grocerylist_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "household_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grocerylist_item",
                columns: table => new
                {
                    grocerylistitem_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    grocerylist_id = table.Column<int>(type: "integer", nullable: false),
                    item_name = table.Column<string>(type: "text", nullable: false),
                    item_amount = table.Column<int>(type: "integer", nullable: false),
                    GroceryListEntityGroceryListID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grocerylist_item", x => x.grocerylistitem_id);
                    table.ForeignKey(
                        name: "FK_grocerylist_item_grocerylist_GroceryListEntityGroceryListID",
                        column: x => x.GroceryListEntityGroceryListID,
                        principalTable: "grocerylist",
                        principalColumn: "grocerylist_id");
                    table.ForeignKey(
                        name: "FK_grocerylist_item_grocerylist_grocerylist_id",
                        column: x => x.grocerylist_id,
                        principalTable: "grocerylist",
                        principalColumn: "grocerylist_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_household_id",
                table: "grocerylist",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_item_grocerylist_id",
                table: "grocerylist_item",
                column: "grocerylist_id");

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_item_GroceryListEntityGroceryListID",
                table: "grocerylist_item",
                column: "GroceryListEntityGroceryListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grocerylist_item");

            migrationBuilder.DropTable(
                name: "grocerylist");

            migrationBuilder.DropTable(
                name: "household");
        }
    }
}
