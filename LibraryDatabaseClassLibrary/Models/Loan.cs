namespace LibraryDatabaseClassLibrary.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateOnly LoanDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string LoanStatus { get; set; } = string.Empty;
    }
}
