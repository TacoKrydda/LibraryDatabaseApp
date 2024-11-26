namespace LibraryDatabaseClassLibrary.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookDescription { get; set; } = string.Empty;
        public int BookQuantity { get; set; }
    }
}
