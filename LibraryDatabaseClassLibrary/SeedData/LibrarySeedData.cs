using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class LibrarySeedData
    {
        public static void SeedLibraries(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>().HasData(
                new Library { LibraryId = 1, LibraryName = "Central Library", Location = "Main Street 123" },
                new Library { LibraryId = 2, LibraryName = "West Side Library", Location = "West Avenue 45" }
            );
        }
    }
}
