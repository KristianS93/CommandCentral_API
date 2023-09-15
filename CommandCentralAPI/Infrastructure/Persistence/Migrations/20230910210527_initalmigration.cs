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
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_household", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grocerylist",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    household = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocerylist", x => x.id);
                    table.ForeignKey(
                        name: "fk_grocerylist_household_household_entity_id",
                        column: x => x.household,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grocerylist_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grocerylist_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_name = table.Column<string>(type: "text", nullable: false),
                    item_amount = table.Column<int>(type: "integer", nullable: false),
                    grocery_list_id = table.Column<int>(type: "integer", nullable: false),
                    grocerylist = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocerylist_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_grocerylist_item_grocerylist_grocery_list_entity_id",
                        column: x => x.grocerylist,
                        principalTable: "grocerylist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grocerylist_item_grocerylist_grocery_list_id",
                        column: x => x.grocery_list_id,
                        principalTable: "grocerylist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_grocerylist_household",
                table: "grocerylist",
                column: "household",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_grocerylist_household_id",
                table: "grocerylist",
                column: "household_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_grocerylist_item_grocery_list_id",
                table: "grocerylist_item",
                column: "grocery_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_grocerylist_item_grocerylist",
                table: "grocerylist_item",
                column: "grocerylist");
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
