using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class HoldSeedData
    {
        public static void SeedHolds(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hold>().HasData(
                new Hold { HoldId = 1, UserId = 6, BookId = 2, Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)), ReleaseDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7)), Status = "Active" },
                new Hold { HoldId = 2, UserId = 7, BookId = 9, Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-4)), ReleaseDate = DateOnly.FromDateTime(DateTime.Today.AddDays(6)), Status = "Active" },
                new Hold { HoldId = 3, UserId = 8, BookId = 14, Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)), ReleaseDate = DateOnly.FromDateTime(DateTime.Today.AddDays(8)), Status = "Active" },
                new Hold { HoldId = 4, UserId = 9, BookId = 17, Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), ReleaseDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)), Status = "Active" },
                new Hold { HoldId = 5, UserId = 10, BookId = 20, Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-6)), ReleaseDate = DateOnly.FromDateTime(DateTime.Today.AddDays(4)), Status = "Active" }
            );
        }
    }
}
