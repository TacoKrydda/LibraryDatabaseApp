using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class AuthorBookSeedData
    {
        public static void SeedAuthorBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>().HasData(
                // J.K. Rowling - Harry Potter
                new AuthorBook { AuthorId = 1, BookId = 1 },
                new AuthorBook { AuthorId = 1, BookId = 2 },
                new AuthorBook { AuthorId = 1, BookId = 3 },
                new AuthorBook { AuthorId = 1, BookId = 4 },
                new AuthorBook { AuthorId = 1, BookId = 5 },
                new AuthorBook { AuthorId = 1, BookId = 6 },
                new AuthorBook { AuthorId = 1, BookId = 7 },

                // George R.R. Martin - A Song of Ice and Fire
                new AuthorBook { AuthorId = 2, BookId = 8 },
                new AuthorBook { AuthorId = 2, BookId = 9 },
                new AuthorBook { AuthorId = 2, BookId = 10 },
                new AuthorBook { AuthorId = 2, BookId = 11 },
                new AuthorBook { AuthorId = 2, BookId = 12 },

                // J.R.R. Tolkien - The Lord of the Rings
                new AuthorBook { AuthorId = 3, BookId = 13 },
                new AuthorBook { AuthorId = 3, BookId = 14 },
                new AuthorBook { AuthorId = 3, BookId = 15 },

                // Isaac Asimov - Foundation
                new AuthorBook { AuthorId = 4, BookId = 16 },
                new AuthorBook { AuthorId = 4, BookId = 17 },
                new AuthorBook { AuthorId = 4, BookId = 18 },

                // Suzanne Collins - The Hunger Games
                new AuthorBook { AuthorId = 5, BookId = 19 },
                new AuthorBook { AuthorId = 5, BookId = 20 },
                new AuthorBook { AuthorId = 5, BookId = 21 }
            );
        }
    }
}
