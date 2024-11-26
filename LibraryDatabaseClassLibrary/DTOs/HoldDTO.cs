namespace LibraryDatabaseClassLibrary.DTOs
{
    public class HoldDTO
    {
        public int HoldId { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public int BookId { get; set; }
        public BookDTO? Book { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
