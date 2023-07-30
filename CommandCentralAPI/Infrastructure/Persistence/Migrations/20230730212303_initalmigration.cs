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
                name: "BaseEntity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.id);
                });

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
                name: "members",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    authority = table.Column<int>(type: "integer", nullable: false),
                    household_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.id);
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
                name: "meal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    meal_name = table.Column<string>(type: "text", nullable: false),
                    meal_description = table.Column<string>(type: "text", nullable: false),
                    meal_direction = table.Column<string>(type: "text", nullable: false),
                    tags = table.Column<string>(type: "text", nullable: false),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    household = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_BaseEntity_id",
                        column: x => x.id,
                        principalTable: "BaseEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_household_household",
                        column: x => x.household,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "week_plan",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    day_1 = table.Column<int>(type: "integer", nullable: false),
                    day_2 = table.Column<int>(type: "integer", nullable: false),
                    day_3 = table.Column<int>(type: "integer", nullable: false),
                    day_4 = table.Column<int>(type: "integer", nullable: false),
                    day_5 = table.Column<int>(type: "integer", nullable: false),
                    day_6 = table.Column<int>(type: "integer", nullable: false),
                    day_7 = table.Column<int>(type: "integer", nullable: false),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    household = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_week_plan", x => x.id);
                    table.ForeignKey(
                        name: "FK_week_plan_BaseEntity_id",
                        column: x => x.id,
                        principalTable: "BaseEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_week_plan_household_household",
                        column: x => x.household,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_week_plan_household_household_id",
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

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    ingredient_name = table.Column<string>(type: "text", nullable: false),
                    ingredient_amount = table.Column<string>(type: "text", nullable: false),
                    meal_id = table.Column<int>(type: "integer", nullable: false),
                    meal = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.id);
                    table.ForeignKey(
                        name: "FK_ingredient_BaseEntity_id",
                        column: x => x.id,
                        principalTable: "BaseEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingredient_meal_meal",
                        column: x => x.meal,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingredient_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<string>(type: "text", nullable: false),
                    meal_id = table.Column<int>(type: "integer", nullable: false),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    household = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.id);
                    table.ForeignKey(
                        name: "FK_tag_household_household",
                        column: x => x.household,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
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
                column: "household_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_item_grocerylist",
                table: "grocerylist_item",
                column: "grocerylist");

            migrationBuilder.CreateIndex(
                name: "IX_grocerylist_item_grocerylist_id",
                table: "grocerylist_item",
                column: "grocerylist_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_meal",
                table: "ingredient",
                column: "meal");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_meal_id",
                table: "ingredient",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_household",
                table: "meal",
                column: "household");

            migrationBuilder.CreateIndex(
                name: "IX_meal_household_id",
                table: "meal",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_household",
                table: "tag",
                column: "household");

            migrationBuilder.CreateIndex(
                name: "IX_tag_household_id",
                table: "tag",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_meal_id",
                table: "tag",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_week_plan_household",
                table: "week_plan",
                column: "household");

            migrationBuilder.CreateIndex(
                name: "IX_week_plan_household_id",
                table: "week_plan",
                column: "household_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grocerylist_item");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "todo_item");

            migrationBuilder.DropTable(
                name: "week_plan");

            migrationBuilder.DropTable(
                name: "grocerylist");

            migrationBuilder.DropTable(
                name: "meal");

            migrationBuilder.DropTable(
                name: "BaseEntity");

            migrationBuilder.DropTable(
                name: "household");
        }
    }
}
