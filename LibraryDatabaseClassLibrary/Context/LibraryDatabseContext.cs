using LibraryDatabaseClassLibrary.Models;
using LibraryDatabaseClassLibrary.SeedData;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseClassLibrary.Context
{
    public class LibraryDatabseContext : DbContext
    {
        public LibraryDatabseContext(DbContextOptions<LibraryDatabseContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Hold> Holds { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<BookLibrary> BookLibraries { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationer mellan Author och AuthorBook (Många-till-många)
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);

            // Relationer mellan Book och BookGenre (Många-till-många)
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });
            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);
            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId);

            // Relationer mellan Book och BookLibrary (Många-till-många)
            modelBuilder.Entity<BookLibrary>()
                .HasKey(bl => new { bl.BookId, bl.LibraryId });
            modelBuilder.Entity<BookLibrary>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLibraries)
                .HasForeignKey(bl => bl.BookId);
            modelBuilder.Entity<BookLibrary>()
                .HasOne(bl => bl.Library)
                .WithMany(l => l.BookLibraries)
                .HasForeignKey(bl => bl.LibraryId);

            // Relationer mellan Book och BookPublisher (Många-till-många)
            modelBuilder.Entity<BookPublisher>()
                .HasKey(bp => new { bp.BookId, bp.PublisherId });
            modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Book)
                .WithMany(b => b.BookPublishers)
                .HasForeignKey(bp => bp.BookId);
            modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Publisher)
                .WithMany(p => p.BookPublishers)
                .HasForeignKey(bp => bp.PublisherId);

            // Relationer mellan Hold och User/Book (En-till-många)
            modelBuilder.Entity<Hold>()
                .HasOne(h => h.User)
                .WithMany(u => u.Holds)
                .HasForeignKey(h => h.UserId);
            modelBuilder.Entity<Hold>()
                .HasOne(h => h.Book)
                .WithMany(b => b.Holds)
                .HasForeignKey(h => h.BookId);

            // Relationer mellan Loan och User/Book (En-till-många)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId);
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);

            // Lägg till unika constraints. visa property i modell måste vara unikt
            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.GenreName)
                .IsUnique();
            modelBuilder.Entity<Library>()
                .HasIndex(l => l.LibraryName)
                .IsUnique();
            modelBuilder.Entity<Publisher>()
                .HasIndex(p => p.PublisherName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seed data
            modelBuilder.ApplySeedData();
        }
    }
}
