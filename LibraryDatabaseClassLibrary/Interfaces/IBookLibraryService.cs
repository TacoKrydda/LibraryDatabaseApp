using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IBookLibraryService
    {
        Task AddBookLibraryRelationAsync(int bookId, int libraryId);
        Task RemoveBookLibraryRelationAsync(int bookId, int libraryId);
        Task<ICollection<LibraryDTO>> GetLibrariesByBookAsync(int bookId);
        Task<ICollection<BookDTO>> GetBooksByLibraryAsync(int libraryId);
    }
}
