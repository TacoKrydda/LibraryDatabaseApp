using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class BookSeedData
    {
        public static void SeedBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                // J.K. Rowling - Harry Potter
                new Book { BookId = 1, BookTitle = "Harry Potter and the Sorcerer's Stone", BookDescription = "First book in the Harry Potter series", BookQuantity = 10 },
                new Book { BookId = 2, BookTitle = "Harry Potter and the Chamber of Secrets", BookDescription = "Second book in the Harry Potter series", BookQuantity = 10 },
                new Book { BookId = 3, BookTitle = "Harry Potter and the Prisoner of Azkaban", BookDescription = "Third book in the Harry Potter series", BookQuantity = 10 },
                new Book { BookId = 4, BookTitle = "Harry Potter and the Goblet of Fire", BookDescription = "Fourth book in the Harry Potter series", BookQuantity = 10 },
                new Book { BookId = 5, BookTitle = "Harry Potter and the Order of the Phoenix", BookDescription = "Fifth book in the Harry Potter series", BookQuantity = 10 },
                new Book { BookId = 6, BookTitle = "Harry Potter and the Half-Blood Prince", BookDescription = "Sixth book in the Harry Potter series", BookQuantity = 10 },
                new Book { BookId = 7, BookTitle = "Harry Potter and the Deathly Hallows", BookDescription = "Seventh book in the Harry Potter series", BookQuantity = 10 },

                // George R.R. Martin - A Song of Ice and Fire
                new Book { BookId = 8, BookTitle = "A Game of Thrones", BookDescription = "First book in A Song of Ice and Fire", BookQuantity = 5 },
                new Book { BookId = 9, BookTitle = "A Clash of Kings", BookDescription = "Second book in A Song of Ice and Fire", BookQuantity = 5 },
                new Book { BookId = 10, BookTitle = "A Storm of Swords", BookDescription = "Third book in A Song of Ice and Fire", BookQuantity = 5 },
                new Book { BookId = 11, BookTitle = "A Feast for Crows", BookDescription = "Fourth book in A Song of Ice and Fire", BookQuantity = 5 },
                new Book { BookId = 12, BookTitle = "A Dance with Dragons", BookDescription = "Fifth book in A Song of Ice and Fire", BookQuantity = 5 },

                // J.R.R. Tolkien - The Lord of the Rings
                new Book { BookId = 13, BookTitle = "The Fellowship of the Ring", BookDescription = "First book in The Lord of the Rings series", BookQuantity = 6 },
                new Book { BookId = 14, BookTitle = "The Two Towers", BookDescription = "Second book in The Lord of the Rings series", BookQuantity = 6 },
                new Book { BookId = 15, BookTitle = "The Return of the King", BookDescription = "Third book in The Lord of the Rings series", BookQuantity = 6 },

                // Isaac Asimov - Foundation
                new Book { BookId = 16, BookTitle = "Foundation", BookDescription = "First book in the Foundation series", BookQuantity = 7 },
                new Book { BookId = 17, BookTitle = "Foundation and Empire", BookDescription = "Second book in the Foundation series", BookQuantity = 7 },
                new Book { BookId = 18, BookTitle = "Second Foundation", BookDescription = "Third book in the Foundation series", BookQuantity = 7 },

                // Suzanne Collins - The Hunger Games
                new Book { BookId = 19, BookTitle = "The Hunger Games", BookDescription = "First book in The Hunger Games series", BookQuantity = 9 },
                new Book { BookId = 20, BookTitle = "Catching Fire", BookDescription = "Second book in The Hunger Games series", BookQuantity = 9 },
                new Book { BookId = 21, BookTitle = "Mockingjay", BookDescription = "Third book in The Hunger Games series", BookQuantity = 9 }
            );
        }
    }
}
