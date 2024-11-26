namespace LibraryDatabaseClassLibrary.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public ICollection<Loan>? Loans { get; set; } = [];
        public ICollection<Hold>? Holds { get; set; } = [];
    }
}
