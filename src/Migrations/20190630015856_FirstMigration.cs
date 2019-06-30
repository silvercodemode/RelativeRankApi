using Microsoft.EntityFrameworkCore.Migrations;

namespace RelativeRank.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shows",
                columns: table => new
                {
                    name = table.Column<string>(unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shows", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    username = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "user_to_show_mapping",
                columns: table => new
                {
                    username = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    showname = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    rank = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_to_show_mapping", x => new { x.username, x.showname });
                    table.ForeignKey(
                        name: "FK__user_to_s__shown__0D7A0286",
                        column: x => x.showname,
                        principalTable: "shows",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__user_to_s__usern__0C85DE4D",
                        column: x => x.username,
                        principalTable: "users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_to_show_mapping_showname",
                table: "user_to_show_mapping",
                column: "showname");

            migrationBuilder.CreateIndex(
                name: "uq_rank_unique_to_user",
                table: "user_to_show_mapping",
                columns: new[] { "username", "rank" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_to_show_mapping");

            migrationBuilder.DropTable(
                name: "shows");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
