namespace LibraryDatabaseClassLibrary.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = string.Empty;

        public ICollection<BookPublisher>? BookPublishers { get; set; } = [];
    }
}
