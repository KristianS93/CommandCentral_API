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
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "todo_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grocerylist",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    household = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grocerylist", x => x.id);
                    table.ForeignKey(
                        name: "FK_grocerylist_household_household",
                        column: x => x.household,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grocerylist_household_household_id",
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
                    grocerylist_id = table.Column<int>(type: "integer", nullable: false),
                    grocerylist = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grocerylist_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_grocerylist_item_grocerylist_grocerylist",
                        column: x => x.grocerylist,
                        principalTable: "grocerylist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grocerylist_item_grocerylist_grocerylist_id",
                        column: x => x.grocerylist_id,
                        principalTable: "grocerylist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_household",
                table: "grocerylist",
                column: "household",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_household_id",
                table: "grocerylist",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_item_grocerylist",
                table: "grocerylist_item",
                column: "grocerylist");

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_item_grocerylist_id",
                table: "grocerylist_item",
                column: "grocerylist_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grocerylist_item");

            migrationBuilder.DropTable(
                name: "todo_item");

            migrationBuilder.DropTable(
                name: "grocerylist");

            migrationBuilder.DropTable(
                name: "household");
        }
    }
}
