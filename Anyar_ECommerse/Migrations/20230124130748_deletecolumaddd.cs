using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anyar_ECommerse.Migrations
{
    public partial class deletecolumaddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Positions");
        }
    }
}
