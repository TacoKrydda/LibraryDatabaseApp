using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IPublisherService
    {
        Task<PublisherDTO> CreatePublisherAsync(PublisherDTO publisherDTO);
        Task DeletePublisherAsync(int id);
        Task<ICollection<PublisherDTO>> GetAllPublishersAsync();
        Task<PublisherDTO> GetPublisherByIdAsync(int id);
        Task<PublisherDTO> UpdatePublisherAsync(int id, PublisherDTO publisherDTO);
        Task<bool> ExistsPublisherAsync(string publisher);
    }
}
