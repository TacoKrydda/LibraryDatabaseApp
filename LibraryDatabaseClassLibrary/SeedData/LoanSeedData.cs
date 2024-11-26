using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class LoanSeedData
    {
        public static void Seedloans(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasData(
                new Loan { LoanId = 1, UserId = 1, BookId = 1, LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-15)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(15)), ReturnDate = null, LoanStatus = "On Loan" },
                new Loan { LoanId = 2, UserId = 2, BookId = 8, LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(20)), ReturnDate = DateOnly.FromDateTime(DateTime.Today), LoanStatus = "Returned" },
                new Loan { LoanId = 3, UserId = 3, BookId = 13, LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(25)), ReturnDate = null, LoanStatus = "On Loan" },
                new Loan { LoanId = 4, UserId = 4, BookId = 16, LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-20)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(10)), ReturnDate = null, LoanStatus = "Overdue" },
                new Loan { LoanId = 5, UserId = 5, BookId = 19, LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(23)), ReturnDate = null, LoanStatus = "On Loan" }
            );
        }
    }
}
