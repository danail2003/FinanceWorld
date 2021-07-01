using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceWorld.Data.Migrations
{
    public partial class ChangeNameOfColumnInLikeModelFromUserIdToAddedByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Likes",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                newName: "IX_Likes_AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AddedByUserId",
                table: "Likes",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AddedByUserId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "Likes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_AddedByUserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
