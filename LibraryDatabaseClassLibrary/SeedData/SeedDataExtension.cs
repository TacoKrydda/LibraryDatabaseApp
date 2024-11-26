using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class SeedDataExtension
    {
        public static void ApplySeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedAuthors();
            modelBuilder.SeedBooks();
            modelBuilder.SeedGenres();
            modelBuilder.SeedLibraries();
            modelBuilder.SeedPublishers();
            modelBuilder.SeedUsers();
            modelBuilder.Seedloans();
            modelBuilder.SeedHolds();

            modelBuilder.SeedAuthorBooks();
            modelBuilder.SeedBookGenres();
            modelBuilder.SeedBookLibraries();
            modelBuilder.SeedBookPublishers();
        }
    }
}
