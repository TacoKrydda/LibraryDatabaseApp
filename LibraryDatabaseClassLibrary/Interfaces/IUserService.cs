using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(int id);
        Task<ICollection<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> UpdateUserAsync(int id, UserDTO userDTO);
        Task<bool> ExistsUserAsync(string email);
    }
}
