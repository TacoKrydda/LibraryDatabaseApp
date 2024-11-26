namespace LibraryDatabaseClassLibrary.Models
{
    public class BookPublisher
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
