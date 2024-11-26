using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IBookPublisherService
    {
        Task AddBookPublisherRelationAsync(int bookId, int publisherId);
        Task RemoveBookPublisherRelationAsync(int bookId, int publisherId);
        Task<ICollection<PublisherDTO>> GetPublishersByBookAsync(int bookId);
        Task<ICollection<BookDTO>> GetBooksByPublisherAsync(int publisherId);
    }
}
