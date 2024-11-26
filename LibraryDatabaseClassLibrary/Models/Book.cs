namespace LibraryDatabaseClassLibrary.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookDescription { get; set; } = string.Empty;
        public int BookQuantity { get; set; }

        public ICollection<AuthorBook>? AuthorBooks { get; set; } = [];
        public ICollection<BookGenre>? BookGenres { get; set; } = [];
        public ICollection<BookLibrary>? BookLibraries { get; set; } = [];
        public ICollection<BookPublisher>? BookPublishers { get; set; } = [];
        public ICollection<Hold>? Holds { get; set; } = [];
        public ICollection<Loan>? Loans { get; set; } = [];
    }
}
