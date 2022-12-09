using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenDiscussion.Data.Migrations
{
    public partial class TopicDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Topics");
        }
    }
}
