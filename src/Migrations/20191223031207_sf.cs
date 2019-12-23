using Microsoft.EntityFrameworkCore.Migrations;

namespace RelativeRank.Migrations
{
    public partial class sf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shows",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    username = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    password = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_to_show_mapping",
                columns: table => new
                {
                    username = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    showname = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Id = table.Column<string>(nullable: true),
                    rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_to_show_mapping", x => new { x.username, x.showname });
                    table.ForeignKey(
                        name: "FK__user_to_s__shown__0D7A0286",
                        column: x => x.showname,
                        principalTable: "shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__user_to_s__usern__0C85DE4D",
                        column: x => x.username,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
