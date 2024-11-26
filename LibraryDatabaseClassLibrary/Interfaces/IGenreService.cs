using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IGenreService
    {
        Task<GenreDTO> CreateGenreAsync(GenreDTO genreDTO);
        Task DeleteGenreAsync(int id);
        Task<ICollection<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO> GetGenreByIdAsync(int id);
        Task<GenreDTO> UpdateGenreAsync(int id, GenreDTO genreDTO);
        Task<bool> ExistsGenreAsync(string genre);
    }
}
