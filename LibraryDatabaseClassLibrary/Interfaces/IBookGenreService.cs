using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IBookGenreService
    {
        Task AddBookGenreRelationAsync(int bookId, int genreId);
        Task RemoveBookGenreRelationAsync(int bookId, int genreId);
        Task<ICollection<GenreDTO>> GetGenresByBookAsync(int bookId);
        Task<ICollection<BookDTO>> GetBooksByGenreAsync(int genreId);
    }
}
