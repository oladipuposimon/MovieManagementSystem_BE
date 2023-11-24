using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesManagement.Data.Migrations
{
    public partial class MovieRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MovieRatingId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRatings_Movies_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MovieRatingId",
                table: "Users",
                column: "MovieRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRatings_MoveId",
                table: "MovieRatings",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRatings_UserId",
                table: "MovieRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_MovieRatings_MovieRatingId",
                table: "Users",
                column: "MovieRatingId",
                principalTable: "MovieRatings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_MovieRatings_MovieRatingId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "MovieRatings");

            migrationBuilder.DropIndex(
                name: "IX_Users_MovieRatingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MovieRatingId",
                table: "Users");
        }
    }
}
