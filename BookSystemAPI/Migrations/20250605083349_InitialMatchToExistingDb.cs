using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMatchToExistingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "ratings");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "users",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Antal",
                table: "users",
                newName: "antal");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ratings",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ratings",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "ratings",
                newName: "book_id");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_UserId",
                table: "ratings",
                newName: "IX_ratings_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_BookId",
                table: "ratings",
                newName: "IX_ratings_book_id");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "books",
                newName: "version");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Isbn",
                table: "books",
                newName: "isbn");

            migrationBuilder.RenameColumn(
                name: "Authors",
                table: "books",
                newName: "authors");

            migrationBuilder.RenameColumn(
                name: "WorkTextReviewsCount",
                table: "books",
                newName: "work_text_reviews_count");

            migrationBuilder.RenameColumn(
                name: "RatingsCount",
                table: "books",
                newName: "ratings_count");

            migrationBuilder.RenameColumn(
                name: "OriginalPublicationYear",
                table: "books",
                newName: "original_publication_year");

            migrationBuilder.RenameColumn(
                name: "LanguageCode",
                table: "books",
                newName: "language_code");

            migrationBuilder.RenameColumn(
                name: "BooksCount",
                table: "books",
                newName: "books_count");

            migrationBuilder.RenameColumn(
                name: "AverageRating",
                table: "books",
                newName: "average_rating");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "books",
                newName: "book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ratings",
                table: "ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_books_book_id",
                table: "ratings",
                column: "book_id",
                principalTable: "books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_users_user_id",
                table: "ratings",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ratings_books_book_id",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_users_user_id",
                table: "ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ratings",
                table: "ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ratings",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Users",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "antal",
                table: "Users",
                newName: "Antal");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Ratings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Ratings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "Ratings",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_user_id",
                table: "Ratings",
                newName: "IX_Ratings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_book_id",
                table: "Ratings",
                newName: "IX_Ratings_BookId");

            migrationBuilder.RenameColumn(
                name: "version",
                table: "Books",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "isbn",
                table: "Books",
                newName: "Isbn");

            migrationBuilder.RenameColumn(
                name: "authors",
                table: "Books",
                newName: "Authors");

            migrationBuilder.RenameColumn(
                name: "work_text_reviews_count",
                table: "Books",
                newName: "WorkTextReviewsCount");

            migrationBuilder.RenameColumn(
                name: "ratings_count",
                table: "Books",
                newName: "RatingsCount");

            migrationBuilder.RenameColumn(
                name: "original_publication_year",
                table: "Books",
                newName: "OriginalPublicationYear");

            migrationBuilder.RenameColumn(
                name: "language_code",
                table: "Books",
                newName: "LanguageCode");

            migrationBuilder.RenameColumn(
                name: "books_count",
                table: "Books",
                newName: "BooksCount");

            migrationBuilder.RenameColumn(
                name: "average_rating",
                table: "Books",
                newName: "AverageRating");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "Books",
                newName: "BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
