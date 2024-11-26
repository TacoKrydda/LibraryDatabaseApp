using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryDatabaseClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class inidbwithdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    LibraryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.LibraryId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookLibraries",
                columns: table => new
                {
                    LibraryId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLibraries", x => new { x.BookId, x.LibraryId });
                    table.ForeignKey(
                        name: "FK_BookLibraries_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLibraries_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "LibraryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookPublishers",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPublishers", x => new { x.BookId, x.PublisherId });
                    table.ForeignKey(
                        name: "FK_BookPublishers_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPublishers_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    HoldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.HoldId);
                    table.ForeignKey(
                        name: "FK_Holds_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LoanStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "AuthorDescription", "AuthorName" },
                values: new object[,]
                {
                    { 1, "Famous for Harry Potter series", "J.K. Rowling" },
                    { 2, "Author of A Song of Ice and Fire", "George R.R. Martin" },
                    { 3, "Author of The Lord of the Rings", "J.R.R. Tolkien" },
                    { 4, "Pioneer of Science Fiction", "Isaac Asimov" },
                    { 5, "Author of The Hunger Games", "Suzanne Collins" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "BookDescription", "BookQuantity", "BookTitle" },
                values: new object[,]
                {
                    { 1, "First book in the Harry Potter series", 10, "Harry Potter and the Sorcerer's Stone" },
                    { 2, "Second book in the Harry Potter series", 10, "Harry Potter and the Chamber of Secrets" },
                    { 3, "Third book in the Harry Potter series", 10, "Harry Potter and the Prisoner of Azkaban" },
                    { 4, "Fourth book in the Harry Potter series", 10, "Harry Potter and the Goblet of Fire" },
                    { 5, "Fifth book in the Harry Potter series", 10, "Harry Potter and the Order of the Phoenix" },
                    { 6, "Sixth book in the Harry Potter series", 10, "Harry Potter and the Half-Blood Prince" },
                    { 7, "Seventh book in the Harry Potter series", 10, "Harry Potter and the Deathly Hallows" },
                    { 8, "First book in A Song of Ice and Fire", 5, "A Game of Thrones" },
                    { 9, "Second book in A Song of Ice and Fire", 5, "A Clash of Kings" },
                    { 10, "Third book in A Song of Ice and Fire", 5, "A Storm of Swords" },
                    { 11, "Fourth book in A Song of Ice and Fire", 5, "A Feast for Crows" },
                    { 12, "Fifth book in A Song of Ice and Fire", 5, "A Dance with Dragons" },
                    { 13, "First book in The Lord of the Rings series", 6, "The Fellowship of the Ring" },
                    { 14, "Second book in The Lord of the Rings series", 6, "The Two Towers" },
                    { 15, "Third book in The Lord of the Rings series", 6, "The Return of the King" },
                    { 16, "First book in the Foundation series", 7, "Foundation" },
                    { 17, "Second book in the Foundation series", 7, "Foundation and Empire" },
                    { 18, "Third book in the Foundation series", 7, "Second Foundation" },
                    { 19, "First book in The Hunger Games series", 9, "The Hunger Games" },
                    { 20, "Second book in The Hunger Games series", 9, "Catching Fire" },
                    { 21, "Third book in The Hunger Games series", 9, "Mockingjay" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Science Fiction" },
                    { 3, "Adventure" },
                    { 4, "Dystopian" }
                });

            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "LibraryId", "LibraryName", "Location" },
                values: new object[,]
                {
                    { 1, "Central Library", "Main Street 123" },
                    { 2, "West Side Library", "West Avenue 45" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "PublisherName" },
                values: new object[,]
                {
                    { 1, "Penguin Books" },
                    { 2, "HarperCollins" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "alice.johnson@example.com", "Alice", "Johnson", "123-456-7890" },
                    { 2, "bob.smith@example.com", "Bob", "Smith", "987-654-3210" },
                    { 3, "charlie.brown@example.com", "Charlie", "Brown", "456-789-1234" },
                    { 4, "diana.prince@example.com", "Diana", "Prince", "654-321-9876" },
                    { 5, "eve.taylor@example.com", "Eve", "Taylor", "321-654-7890" },
                    { 6, "frank.castle@example.com", "Frank", "Castle", "789-123-4567" },
                    { 7, "grace.hopper@example.com", "Grace", "Hopper", "567-890-1234" },
                    { 8, "henry.ford@example.com", "Henry", "Ford", "890-123-5678" },
                    { 9, "ivy.green@example.com", "Ivy", "Green", "432-876-0987" },
                    { 10, "jack.white@example.com", "Jack", "White", "345-678-9012" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 2, 11 },
                    { 2, 12 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 18 },
                    { 5, 19 },
                    { 5, 20 },
                    { 5, 21 }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 3 },
                    { 5, 1 },
                    { 5, 3 },
                    { 6, 1 },
                    { 6, 3 },
                    { 7, 1 },
                    { 7, 3 },
                    { 8, 1 },
                    { 8, 3 },
                    { 9, 1 },
                    { 9, 3 },
                    { 10, 1 },
                    { 10, 3 },
                    { 11, 1 },
                    { 11, 3 },
                    { 12, 1 },
                    { 12, 3 },
                    { 13, 1 },
                    { 13, 3 },
                    { 14, 1 },
                    { 14, 3 },
                    { 15, 1 },
                    { 15, 3 },
                    { 16, 2 },
                    { 17, 2 },
                    { 18, 2 },
                    { 19, 2 },
                    { 19, 3 },
                    { 19, 4 },
                    { 20, 2 },
                    { 20, 3 },
                    { 20, 4 },
                    { 21, 2 },
                    { 21, 3 },
                    { 21, 4 }
                });

            migrationBuilder.InsertData(
                table: "BookLibraries",
                columns: new[] { "BookId", "LibraryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 1 },
                    { 5, 2 },
                    { 6, 1 },
                    { 7, 2 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 2 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 2 },
                    { 14, 1 },
                    { 15, 2 },
                    { 16, 1 },
                    { 17, 2 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 2 },
                    { 21, 2 }
                });

            migrationBuilder.InsertData(
                table: "BookPublishers",
                columns: new[] { "BookId", "PublisherId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 2 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 2 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 2 },
                    { 14, 2 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 2 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 2 },
                    { 21, 2 }
                });

            migrationBuilder.InsertData(
                table: "Holds",
                columns: new[] { "HoldId", "BookId", "Date", "ReleaseDate", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new DateOnly(2024, 11, 23), new DateOnly(2024, 12, 3), "Active", 6 },
                    { 2, 9, new DateOnly(2024, 11, 22), new DateOnly(2024, 12, 2), "Active", 7 },
                    { 3, 14, new DateOnly(2024, 11, 24), new DateOnly(2024, 12, 4), "Active", 8 },
                    { 4, 17, new DateOnly(2024, 11, 21), new DateOnly(2024, 12, 1), "Active", 9 },
                    { 5, 20, new DateOnly(2024, 11, 20), new DateOnly(2024, 11, 30), "Active", 10 }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "LoanId", "BookId", "DueDate", "LoanDate", "LoanStatus", "ReturnDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2024, 12, 11), new DateOnly(2024, 11, 11), "On Loan", null, 1 },
                    { 2, 8, new DateOnly(2024, 12, 16), new DateOnly(2024, 11, 16), "Returned", new DateOnly(2024, 11, 26), 2 },
                    { 3, 13, new DateOnly(2024, 12, 21), new DateOnly(2024, 11, 21), "On Loan", null, 3 },
                    { 4, 16, new DateOnly(2024, 12, 6), new DateOnly(2024, 11, 6), "Overdue", null, 4 },
                    { 5, 19, new DateOnly(2024, 12, 19), new DateOnly(2024, 11, 19), "On Loan", null, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLibraries_LibraryId",
                table: "BookLibraries",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPublishers_PublisherId",
                table: "BookPublishers",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_GenreName",
                table: "Genres",
                column: "GenreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holds_BookId",
                table: "Holds",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_UserId",
                table: "Holds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_LibraryName",
                table: "Libraries",
                column: "LibraryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_PublisherName",
                table: "Publishers",
                column: "PublisherName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "BookLibraries");

            migrationBuilder.DropTable(
                name: "BookPublishers");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
