using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class BookGenreSeedData
    {
        public static void SeedBookGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>().HasData(
                new BookGenre { BookId = 1, GenreId = 1 },
                new BookGenre { BookId = 2, GenreId = 1 },
                new BookGenre { BookId = 3, GenreId = 1 },
                new BookGenre { BookId = 4, GenreId = 1 },
                new BookGenre { BookId = 5, GenreId = 1 },
                new BookGenre { BookId = 6, GenreId = 1 },
                new BookGenre { BookId = 7, GenreId = 1 },
                new BookGenre { BookId = 8, GenreId = 1 },
                new BookGenre { BookId = 9, GenreId = 1 },
                new BookGenre { BookId = 10, GenreId = 1 },
                new BookGenre { BookId = 11, GenreId = 1 },
                new BookGenre { BookId = 12, GenreId = 1 },
                new BookGenre { BookId = 13, GenreId = 1 },
                new BookGenre { BookId = 14, GenreId = 1 },
                new BookGenre { BookId = 15, GenreId = 1 },

                // Science Fiction
                new BookGenre { BookId = 16, GenreId = 2 },
                new BookGenre { BookId = 17, GenreId = 2 },
                new BookGenre { BookId = 18, GenreId = 2 },
                new BookGenre { BookId = 19, GenreId = 2 },
                new BookGenre { BookId = 20, GenreId = 2 },
                new BookGenre { BookId = 21, GenreId = 2 },

                // Adventure
                new BookGenre { BookId = 1, GenreId = 3 },
                new BookGenre { BookId = 2, GenreId = 3 },
                new BookGenre { BookId = 3, GenreId = 3 },
                new BookGenre { BookId = 4, GenreId = 3 },
                new BookGenre { BookId = 5, GenreId = 3 },
                new BookGenre { BookId = 6, GenreId = 3 },
                new BookGenre { BookId = 7, GenreId = 3 },
                new BookGenre { BookId = 8, GenreId = 3 },
                new BookGenre { BookId = 9, GenreId = 3 },
                new BookGenre { BookId = 10, GenreId = 3 },
                new BookGenre { BookId = 11, GenreId = 3 },
                new BookGenre { BookId = 12, GenreId = 3 },
                new BookGenre { BookId = 13, GenreId = 3 },
                new BookGenre { BookId = 14, GenreId = 3 },
                new BookGenre { BookId = 15, GenreId = 3 },
                new BookGenre { BookId = 19, GenreId = 3 },
                new BookGenre { BookId = 20, GenreId = 3 },
                new BookGenre { BookId = 21, GenreId = 3 },

                // Dystopian
                new BookGenre { BookId = 19, GenreId = 4 },
                new BookGenre { BookId = 20, GenreId = 4 },
                new BookGenre { BookId = 21, GenreId = 4 }
            );
        }
    }
}
