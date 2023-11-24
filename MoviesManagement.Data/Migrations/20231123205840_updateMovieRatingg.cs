using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesManagement.Data.Migrations
{
    public partial class updateMovieRatingg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_MovieRatings_MovieRatingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MovieRatingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MovieRatingId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MovieRatingId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MovieRatingId",
                table: "Users",
                column: "MovieRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_MovieRatings_MovieRatingId",
                table: "Users",
                column: "MovieRatingId",
                principalTable: "MovieRatings",
                principalColumn: "Id");
        }
    }
}
