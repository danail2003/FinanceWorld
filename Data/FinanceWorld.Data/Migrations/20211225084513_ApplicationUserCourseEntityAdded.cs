using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceWorld.Data.Migrations
{
    public partial class ApplicationUserCourseEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Course",
                type: "float",
                maxLength: 6,
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
