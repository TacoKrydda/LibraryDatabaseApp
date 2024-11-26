namespace LibraryDatabaseClassLibrary.Models
{
    public class Hold
    {
        public int HoldId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
