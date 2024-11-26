using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<LoanService> _logger;

        public LoanService(LibraryDatabseContext context, ILogger<LoanService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<LoanDTO> CreateLoanAsync(LoanDTO loanDTO)
        {
            if (loanDTO == null)
            {
                _logger.LogWarning("Invalid Loan data provided.");
                throw new ArgumentException("loan data is invalid.");
            }

            var loan = new Loan
            {
                UserId = loanDTO.UserId,
                BookId = loanDTO.BookId,
                LoanDate = loanDTO.LoanDate,
                DueDate = loanDTO.DueDate,
                ReturnDate = loanDTO.ReturnDate,
                LoanStatus = loanDTO.LoanStatus,
            };

            try
            {
                await _context.Loans.AddAsync(loan);
                await _context.SaveChangesAsync();

                var loanFromDb = await _context.Loans
                    .Include(l => l.User)
                    .Include(l => l.Book)
                    .FirstOrDefaultAsync(l => l.LoanId == loan.LoanId);

                if (loanFromDb == null || loanFromDb.User == null || loanFromDb.Book == null)
                {
                    throw new InvalidOperationException("Loan or it's properties could not be found after creation.");
                }

                return new LoanDTO
                {
                    LoanId = loanFromDb.LoanId,
                    UserId = loanFromDb.UserId,
                    BookId = loanFromDb.BookId,
                    LoanDate = loanFromDb.LoanDate,
                    DueDate = loanFromDb.DueDate,
                    ReturnDate = loanFromDb.ReturnDate,
                    LoanStatus = loanFromDb.LoanStatus,
                    User = new UserDTO
                    {
                        UserId = loanFromDb.User.UserId,
                        FirstName = loanFromDb.User.FirstName,
                        LastName = loanFromDb.User.LastName,
                        Email = loanFromDb.User.Email,
                        Phone = loanFromDb.User.Phone
                    },
                    Book = new BookDTO
                    {
                        BookId = loanFromDb.Book.BookId,
                        BookTitle = loanFromDb.Book.BookTitle,
                        BookDescription = loanFromDb.Book.BookDescription,
                        BookQuantity = loanFromDb.Book.BookQuantity
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Loan.");
                throw new InvalidOperationException("An error occurred while adding the Loan.", ex);
            }
        }

        public async Task DeleteLoanAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var loan = await _context.Loans.FindAsync(id);
                if (loan == null)
                {
                    _logger.LogWarning("Loan with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Loan with ID {id} not found.");
                }

                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Loan.");
                throw new Exception("An error occurred while removing the Loan.", ex);
            }
        }

        public async Task<ICollection<LoanDTO>> GetAllLoansAsync()
        {
            try
            {
                var loans = await _context.Loans
                    .Include(l => l.User)
                    .Include(l => l.Book)
                    .ToListAsync();

                if (loans.Any(l => l.User == null || l.Book == null))
                {
                    throw new InvalidOperationException("Loan properties could not be found.");
                }


                return loans.Select(l => new LoanDTO
                {
                    LoanId = l.LoanId,
                    UserId = l.UserId,
                    BookId = l.BookId,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    ReturnDate = l.ReturnDate,
                    LoanStatus = l.LoanStatus,
                    User = new UserDTO
                    {
                        UserId = l.User!.UserId, // Null-forgiving operator!
                        FirstName = l.User.FirstName,
                        LastName = l.User.LastName,
                        Email = l.User.Email,
                        Phone = l.User.Phone,
                    },
                    Book = new BookDTO
                    {
                        BookId = l.Book!.BookId, // Null-forgiving operator!
                        BookTitle = l.Book.BookTitle,
                        BookDescription = l.Book.BookDescription,
                        BookQuantity = l.Book.BookQuantity,
                    }
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Loans.");
                throw new InvalidOperationException("An error occurred while retrieving the Loans.", ex);
            }
        }

        public async Task<LoanDTO> GetLoanByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var loan = await _context.Loans
                    .Include(l => l.User)
                    .Include(l => l.Book)
                    .FirstOrDefaultAsync(l => l.LoanId == id);

                if (loan == null || loan.User == null || loan.Book == null)
                {
                    _logger.LogWarning("Loan or it's properties with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Loan or it's properties with ID {id} not found.");
                }
                return new LoanDTO
                {
                    LoanId = loan.LoanId,
                    UserId = loan.UserId,
                    BookId = loan.BookId,
                    LoanDate = loan.LoanDate,
                    DueDate = loan.DueDate,
                    ReturnDate = loan.ReturnDate,
                    LoanStatus = loan.LoanStatus,
                    User = new UserDTO
                    {
                        UserId = loan.User.UserId,
                        FirstName = loan.User.FirstName,
                        LastName = loan.User.LastName,
                        Email = loan.User.Email,
                        Phone = loan.User.Phone,
                    },
                    Book = new BookDTO
                    {
                        BookId = loan.Book.BookId,
                        BookTitle = loan.Book.BookTitle,
                        BookDescription = loan.Book.BookDescription,
                        BookQuantity = loan.Book.BookQuantity,
                    }
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Loan.");
                throw new InvalidOperationException("An error occurred while retrieving the Loan.", ex);
            }
        }

        public async Task<LoanDTO> UpdateLoanAsync(int id, LoanDTO loanDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingLoan = await _context.Loans.FindAsync(id);
                if (existingLoan == null)
                {
                    _logger.LogWarning("Loan with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Loan with ID {id} not found.");
                }
                existingLoan.LoanId = loanDTO.LoanId;
                existingLoan.UserId = loanDTO.UserId;
                existingLoan.BookId = loanDTO.BookId;
                existingLoan.LoanDate = loanDTO.LoanDate;
                existingLoan.DueDate = loanDTO.DueDate;
                existingLoan.ReturnDate = loanDTO.ReturnDate;
                existingLoan.LoanStatus = loanDTO.LoanStatus;

                await _context.SaveChangesAsync();

                var updateLoanFromDb = await _context.Loans
                    .Include(l => l.User)
                    .Include(l => l.Book)
                    .FirstOrDefaultAsync(l => l.LoanId == existingLoan.LoanId);

                if (updateLoanFromDb == null || updateLoanFromDb.User == null || updateLoanFromDb.Book == null)
                {
                    _logger.LogWarning("Loan or it's properties with ID {Id} not found after update.", id);
                    throw new KeyNotFoundException($"Loan or it's properties with ID {id} not found after update.");
                }

                return new LoanDTO
                {
                    LoanId = updateLoanFromDb.LoanId,
                    UserId = updateLoanFromDb.UserId,
                    BookId = updateLoanFromDb.BookId,
                    LoanDate = updateLoanFromDb.LoanDate,
                    DueDate = updateLoanFromDb.DueDate,
                    ReturnDate = updateLoanFromDb.ReturnDate,
                    LoanStatus = updateLoanFromDb.LoanStatus,
                    User = new UserDTO
                    {
                        UserId = updateLoanFromDb.User.UserId,
                        FirstName = updateLoanFromDb.User.FirstName,
                        LastName = updateLoanFromDb.User.LastName,
                        Email = updateLoanFromDb.User.Email,
                        Phone = updateLoanFromDb.User.Phone,
                    },
                    Book = new BookDTO
                    {
                        BookId = updateLoanFromDb.Book.BookId,
                        BookTitle = updateLoanFromDb.Book.BookTitle,
                        BookDescription = updateLoanFromDb.Book.BookDescription,
                        BookQuantity = updateLoanFromDb.Book.BookQuantity,
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Loan.");
                throw new InvalidOperationException("An error occurred while updating the Loan.", ex);
            }
        }
        public async Task<ICollection<LoanDTO>> GetLoansByUserAsync(int userId)
        {
            var loans = await _context.Loans
                .Where(l => l.UserId == userId)
                .Include(l => l.User)
                .Include(l => l.Book)
                .ToListAsync();

            return loans.Select(l => new LoanDTO
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                BookId = l.BookId,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                LoanStatus = l.LoanStatus,
                User = l.User != null ? new UserDTO
                {
                    UserId = l.User.UserId,
                    FirstName = l.User.FirstName,
                    LastName = l.User.LastName,
                    Email = l.User.Email,
                    Phone = l.User.Phone
                } : null,
                Book = l.Book != null ? new BookDTO
                {
                    BookId = l.Book.BookId,
                    BookTitle = l.Book.BookTitle,
                    BookDescription = l.Book.BookDescription,
                    BookQuantity = l.Book.BookQuantity
                } : null
            }).ToList();
        }

        public async Task<ICollection<LoanDTO>> GetLoansByBookAsync(int bookId)
        {
            var loans = await _context.Loans
                .Where(l => l.BookId == bookId)
                .Include(l => l.User)
                .Include(l => l.Book)
                .ToListAsync();

            return loans.Select(l => new LoanDTO
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                BookId = l.BookId,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                LoanStatus = l.LoanStatus,
                User = l.User != null ? new UserDTO
                {
                    UserId = l.User.UserId,
                    FirstName = l.User.FirstName,
                    LastName = l.User.LastName,
                    Email = l.User.Email,
                    Phone = l.User.Phone
                } : null,
                Book = l.Book != null ? new BookDTO
                {
                    BookId = l.Book.BookId,
                    BookTitle = l.Book.BookTitle,
                    BookDescription = l.Book.BookDescription,
                    BookQuantity = l.Book.BookQuantity
                } : null
            }).ToList();
        }
    }
}
