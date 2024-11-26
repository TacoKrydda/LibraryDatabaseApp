using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.SeedData
{
    public static class UserSeedData
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Phone = "123-456-7890" },
                new User { UserId = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", Phone = "987-654-3210" },
                new User { UserId = 3, FirstName = "Charlie", LastName = "Brown", Email = "charlie.brown@example.com", Phone = "456-789-1234" },
                new User { UserId = 4, FirstName = "Diana", LastName = "Prince", Email = "diana.prince@example.com", Phone = "654-321-9876" },
                new User { UserId = 5, FirstName = "Eve", LastName = "Taylor", Email = "eve.taylor@example.com", Phone = "321-654-7890" },
                new User { UserId = 6, FirstName = "Frank", LastName = "Castle", Email = "frank.castle@example.com", Phone = "789-123-4567" },
                new User { UserId = 7, FirstName = "Grace", LastName = "Hopper", Email = "grace.hopper@example.com", Phone = "567-890-1234" },
                new User { UserId = 8, FirstName = "Henry", LastName = "Ford", Email = "henry.ford@example.com", Phone = "890-123-5678" },
                new User { UserId = 9, FirstName = "Ivy", LastName = "Green", Email = "ivy.green@example.com", Phone = "432-876-0987" },
                new User { UserId = 10, FirstName = "Jack", LastName = "White", Email = "jack.white@example.com", Phone = "345-678-9012" }
            );
        }
    }
}
