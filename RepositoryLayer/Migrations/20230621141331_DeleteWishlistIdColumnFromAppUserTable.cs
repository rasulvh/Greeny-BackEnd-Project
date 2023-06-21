using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class DeleteWishlistIdColumnFromAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wishlists_WishlistId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WishlistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishlistId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishlistId",
                table: "AspNetUsers",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wishlists_WishlistId",
                table: "AspNetUsers",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id");
        }
    }
}
