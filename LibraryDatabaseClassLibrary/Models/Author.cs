namespace LibraryDatabaseClassLibrary.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorDescription { get; set; } = string.Empty;

        public ICollection<AuthorBook>? AuthorBooks { get; set; } = [];
    }
}
