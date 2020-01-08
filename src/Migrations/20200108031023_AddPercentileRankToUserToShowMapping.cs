using Microsoft.EntityFrameworkCore.Migrations;

namespace RelativeRank.Migrations
{
    public partial class AddPercentileRankToUserToShowMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "percentile_rank",
                table: "user_to_show_mapping",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "percentile_rank",
                table: "user_to_show_mapping");
        }
    }
}
