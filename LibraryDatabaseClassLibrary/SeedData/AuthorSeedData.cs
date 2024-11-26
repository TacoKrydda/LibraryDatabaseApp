using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class AuthorSeedData
    {
        public static void SeedAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, AuthorName = "J.K. Rowling", AuthorDescription = "Famous for Harry Potter series" },
                new Author { AuthorId = 2, AuthorName = "George R.R. Martin", AuthorDescription = "Author of A Song of Ice and Fire" },
                new Author { AuthorId = 3, AuthorName = "J.R.R. Tolkien", AuthorDescription = "Author of The Lord of the Rings" },
                new Author { AuthorId = 4, AuthorName = "Isaac Asimov", AuthorDescription = "Pioneer of Science Fiction" },
                new Author { AuthorId = 5, AuthorName = "Suzanne Collins", AuthorDescription = "Author of The Hunger Games" }
            );
        }
    }
}
