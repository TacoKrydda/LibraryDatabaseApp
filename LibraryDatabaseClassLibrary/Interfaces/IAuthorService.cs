using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDTO> CreateAuthorAsync(AuthorDTO authorDTO);
        Task DeleteAuthorAsync(int id);
        Task<ICollection<AuthorDTO>> GetAllAuthorsAsync();
        Task<AuthorDTO> GetAuthorByIdAsync(int id);
        Task<AuthorDTO> UpdateAuthorAsync(int id, AuthorDTO authorDTO);
    }
}
