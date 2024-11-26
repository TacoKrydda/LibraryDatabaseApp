using LibraryDatabaseClassLibrary.DTOs;

namespace LibraryDatabaseClassLibrary.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDTO> CreateLoanAsync(LoanDTO loanDTO);
        Task DeleteLoanAsync(int id);
        Task<ICollection<LoanDTO>> GetAllLoansAsync();
        Task<LoanDTO> GetLoanByIdAsync(int id);
        Task<LoanDTO> UpdateLoanAsync(int id, LoanDTO loanDTO);
        Task<ICollection<LoanDTO>> GetLoansByUserAsync(int userId);
        Task<ICollection<LoanDTO>> GetLoansByBookAsync(int bookId);
    }
}
