using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class GenreSeedData
    {
        public static void SeedGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, GenreName = "Fantasy" },
                new Genre { GenreId = 2, GenreName = "Science Fiction" },
                new Genre { GenreId = 3, GenreName = "Adventure" },
                new Genre { GenreId = 4, GenreName = "Dystopian" }
            );
        }
    }
}
