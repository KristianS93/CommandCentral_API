using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mealplan10 : Migration
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
                    last_modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    day_1 = table.Column<int>(type: "integer", nullable: true),
                    day_2 = table.Column<int>(type: "integer", nullable: true),
                    day_3 = table.Column<int>(type: "integer", nullable: true),
                    day_4 = table.Column<int>(type: "integer", nullable: true),
                    day_5 = table.Column<int>(type: "integer", nullable: true),
                    day_6 = table.Column<int>(type: "integer", nullable: true),
                    day_7 = table.Column<int>(type: "integer", nullable: true),
                    household_id = table.Column<int>(type: "integer", nullable: true),
                    household = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.id);
                    table.ForeignKey(
                        name: "FK_BaseEntity_household_household",
                        column: x => x.household,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_household",
                table: "BaseEntity",
                column: "household");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_household_id",
                table: "BaseEntity",
                column: "household_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
