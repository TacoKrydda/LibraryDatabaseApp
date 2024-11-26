namespace LibraryDatabaseClassLibrary.DTOs
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public int BookId { get; set; }
        public BookDTO? Book { get; set; }
        public DateOnly LoanDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string LoanStatus { get; set; } = string.Empty;
    }
}
