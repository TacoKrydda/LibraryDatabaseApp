using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface ILibraryService
    {
        Task<LibraryDTO> CreateLibraryAsync(LibraryDTO libraryDTO);
        Task DeleteLibraryDeleteAsync(int id);
        Task<ICollection<LibraryDTO>> GetAllLibrariesAsync();
        Task<LibraryDTO> GetLibraryByIdAsync(int id);
        Task<LibraryDTO> UpdateLibraryAsync(int id, LibraryDTO libraryDTO);
        Task<bool> ExistsLibraryAsync(string library);
    }
}
