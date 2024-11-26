using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class PublisherSeedData
    {
        public static void SeedPublishers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, PublisherName = "Penguin Books" },
                new Publisher { PublisherId = 2, PublisherName = "HarperCollins" }
            );
        }
    }
}
