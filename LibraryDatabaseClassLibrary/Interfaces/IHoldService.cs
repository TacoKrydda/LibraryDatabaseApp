using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface IHoldService
    {
        Task<HoldDTO> CreateHoldAsync(HoldDTO holdDTO);
        Task DeleteHoldAsync(int id);
        Task<ICollection<HoldDTO>> GetAllHoldsAsync();
        Task<HoldDTO> GetHoldByIdAsync(int id);
        Task<HoldDTO> UpdateHoldAsync(int id, HoldDTO holdDTO);
        Task<ICollection<HoldDTO>> GetHoldsByUserAsync(int userId);
        Task<ICollection<HoldDTO>> GetHoldsByBookAsync(int bookId);
    }
}
