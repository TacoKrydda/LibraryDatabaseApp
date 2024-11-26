using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IAuthorBookService
    {
        Task AddAuthorBookRelationAsync(int authorId, int bookId);
        Task RemoveAuthorBookRelationAsync(int authorId, int bookId);
        Task<ICollection<BookDTO>> GetBooksByAuthorAsync(int authorId);
        Task<ICollection<AuthorDTO>> GetAuthorsByBookAsync(int bookId);
    }
}
