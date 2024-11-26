using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> CreateBookAsync(BookDTO bookDTO);
        Task DeleteBookAsync(int id);
        Task<ICollection<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<BookDTO> UpdateBookAsync(int id, BookDTO bookDTO);
    }
}
