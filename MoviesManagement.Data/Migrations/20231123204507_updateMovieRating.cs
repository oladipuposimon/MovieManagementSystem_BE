using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesManagement.Data.Migrations
{
    public partial class updateMovieRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRatings_Movies_MoveId",
                table: "MovieRatings");

            migrationBuilder.RenameColumn(
                name: "MoveId",
                table: "MovieRatings",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieRatings_MoveId",
                table: "MovieRatings",
                newName: "IX_MovieRatings_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRatings_Movies_MovieId",
                table: "MovieRatings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRatings_Movies_MovieId",
                table: "MovieRatings");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MovieRatings",
                newName: "MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieRatings_MovieId",
                table: "MovieRatings",
                newName: "IX_MovieRatings_MoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRatings_Movies_MoveId",
                table: "MovieRatings",
                column: "MoveId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
