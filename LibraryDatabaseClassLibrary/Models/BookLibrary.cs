namespace LibraryDatabaseClassLibrary.Models
{
    public class BookLibrary
    {
        public int LibraryId { get; set; }
        public Library? Library { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
