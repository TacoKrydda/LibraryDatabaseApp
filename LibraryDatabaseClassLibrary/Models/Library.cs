namespace LibraryDatabaseClassLibrary.Models
{
    public class Library
    {
        public int LibraryId { get; set; }
        public string LibraryName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<BookLibrary>? BookLibraries { get; set; } = [];
    }
}
