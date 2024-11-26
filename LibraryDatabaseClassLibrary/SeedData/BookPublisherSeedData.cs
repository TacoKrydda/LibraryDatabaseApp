using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class BookPublisherSeedData
    {
        public static void SeedBookPublishers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookPublisher>().HasData(
                new BookPublisher { BookId = 1, PublisherId = 1 },
                new BookPublisher { BookId = 2, PublisherId = 1 },
                new BookPublisher { BookId = 3, PublisherId = 2 },
                new BookPublisher { BookId = 4, PublisherId = 2 },
                new BookPublisher { BookId = 5, PublisherId = 1 },
                new BookPublisher { BookId = 6, PublisherId = 1 },
                new BookPublisher { BookId = 7, PublisherId = 2 },
                new BookPublisher { BookId = 8, PublisherId = 1 },
                new BookPublisher { BookId = 9, PublisherId = 1 },
                new BookPublisher { BookId = 10, PublisherId = 2 },
                new BookPublisher { BookId = 11, PublisherId = 1 },
                new BookPublisher { BookId = 12, PublisherId = 1 },
                new BookPublisher { BookId = 13, PublisherId = 2 },
                new BookPublisher { BookId = 14, PublisherId = 2 },
                new BookPublisher { BookId = 15, PublisherId = 1 },
                new BookPublisher { BookId = 16, PublisherId = 1 },
                new BookPublisher { BookId = 17, PublisherId = 2 },
                new BookPublisher { BookId = 18, PublisherId = 1 },
                new BookPublisher { BookId = 19, PublisherId = 1 },
                new BookPublisher { BookId = 20, PublisherId = 2 },
                new BookPublisher { BookId = 21, PublisherId = 2 }
            );
        }
    }
}
