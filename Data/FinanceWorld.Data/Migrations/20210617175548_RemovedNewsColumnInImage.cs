using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceWorld.Data.Migrations
{
    public partial class RemovedNewsColumnInImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_News_NewsId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_NewsId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsId",
                table: "Images",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_News_NewsId",
                table: "Images",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
