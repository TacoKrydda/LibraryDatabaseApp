using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class BookLibrarySeedData
    {
        public static void SeedBookLibraries(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLibrary>().HasData(
                new BookLibrary { BookId = 1, LibraryId = 1 },
                new BookLibrary { BookId = 2, LibraryId = 1 },
                new BookLibrary { BookId = 3, LibraryId = 2 },
                new BookLibrary { BookId = 4, LibraryId = 1 },
                new BookLibrary { BookId = 5, LibraryId = 2 },
                new BookLibrary { BookId = 6, LibraryId = 1 },
                new BookLibrary { BookId = 7, LibraryId = 2 },
                new BookLibrary { BookId = 8, LibraryId = 1 },
                new BookLibrary { BookId = 9, LibraryId = 1 },
                new BookLibrary { BookId = 10, LibraryId = 2 },
                new BookLibrary { BookId = 11, LibraryId = 1 },
                new BookLibrary { BookId = 12, LibraryId = 1 },
                new BookLibrary { BookId = 13, LibraryId = 2 },
                new BookLibrary { BookId = 14, LibraryId = 1 },
                new BookLibrary { BookId = 15, LibraryId = 2 },
                new BookLibrary { BookId = 16, LibraryId = 1 },
                new BookLibrary { BookId = 17, LibraryId = 2 },
                new BookLibrary { BookId = 18, LibraryId = 1 },
                new BookLibrary { BookId = 19, LibraryId = 1 },
                new BookLibrary { BookId = 20, LibraryId = 2 },
                new BookLibrary { BookId = 21, LibraryId = 2 }
            );
        }
    }
}
