using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class SolveRelationProblemBetweenProductAndRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Ratings_RatingId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Ratings_RatingId",
                table: "Products",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Ratings_RatingId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Ratings_RatingId",
                table: "Products",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
