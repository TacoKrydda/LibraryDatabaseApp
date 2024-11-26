using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class HoldService : IHoldService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<HoldService> _logger;

        public HoldService(LibraryDatabseContext context, ILogger<HoldService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<HoldDTO> CreateHoldAsync(HoldDTO holdDTO)
        {
            if (holdDTO == null)
            {
                _logger.LogWarning("Invalid Hold data provided.");
                throw new ArgumentException("Hold data is invalid.");
            }

            var hold = new Hold
            {
                UserId = holdDTO.UserId,
                BookId = holdDTO.BookId,
                Date = holdDTO.Date,
                ReleaseDate = holdDTO.ReleaseDate,
                Status = holdDTO.Status,
            };

            try
            {
                await _context.Holds.AddAsync(hold);
                await _context.SaveChangesAsync();

                var holdFromDb = await _context.Holds
                    .Include(h => h.User)
                    .Include(h => h.Book)
                    .FirstOrDefaultAsync(h => h.HoldId == hold.HoldId);

                if (holdFromDb == null || holdFromDb.User == null || holdFromDb.Book == null)
                {
                    throw new InvalidOperationException("Hold or it's properties could not be found after creation.");
                }

                return new HoldDTO
                {
                    HoldId = hold.HoldId,
                    UserId = hold.UserId,
                    BookId = hold.BookId,
                    Date = hold.Date,
                    ReleaseDate = hold.ReleaseDate,
                    Status = hold.Status,
                    User = new UserDTO
                    {
                        UserId = holdFromDb.User.UserId,
                        FirstName = holdFromDb.User.FirstName,
                        LastName = holdFromDb.User.LastName,
                        Email = holdFromDb.User.Email,
                        Phone = holdFromDb.User.Phone
                    },
                    Book = new BookDTO
                    {
                        BookId = holdFromDb.Book.BookId,
                        BookTitle = holdFromDb.Book.BookTitle,
                        BookDescription = holdFromDb.Book.BookDescription,
                        BookQuantity = holdFromDb.Book.BookQuantity
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Hold.");
                throw new InvalidOperationException("An error occurred while adding the Hold.", ex);
            }
        }

        public async Task DeleteHoldAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var hold = await _context.Holds.FindAsync(id);
                if (hold == null)
                {
                    _logger.LogWarning("Hold with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Hold with ID {id} not found.");
                }

                _context.Holds.Remove(hold);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Hold.");
                throw new Exception("An error occurred while removing the Hold.", ex);
            }
        }

        public async Task<ICollection<HoldDTO>> GetAllHoldsAsync()
        {
            try
            {
                var holds = await _context.Holds
                    .Include(h => h.User)
                    .Include(h => h.Book)
                    .ToListAsync();

                if (holds.Any(h => h.User == null || h.Book == null))
                {
                    throw new InvalidOperationException("Hold properties could not be found.");
                }


                return holds.Select(h => new HoldDTO
                {
                    HoldId = h.HoldId,
                    UserId = h.UserId,
                    BookId = h.BookId,
                    Date = h.Date,
                    ReleaseDate = h.ReleaseDate,
                    Status = h.Status,
                    User = new UserDTO
                    {
                        UserId = h.User!.UserId, // Null-forgiving operator!
                        FirstName = h.User.FirstName,
                        LastName = h.User.LastName,
                        Email = h.User.Email,
                        Phone = h.User.Phone,
                    },
                    Book = new BookDTO
                    {
                        BookId = h.Book!.BookId, // Null-forgiving operator!
                        BookTitle = h.Book.BookTitle,
                        BookDescription = h.Book.BookDescription,
                        BookQuantity = h.Book.BookQuantity,
                    }
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Holds.");
                throw new InvalidOperationException("An error occurred while retrieving the Holds.", ex);
            }
        }

        public async Task<HoldDTO> GetHoldByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var hold = await _context.Holds
                    .Include(h => h.User)
                    .Include(h => h.Book)
                    .FirstOrDefaultAsync(h => h.HoldId == id);

                if (hold == null || hold.User == null || hold.Book == null)
                {
                    _logger.LogWarning("Hold or it's properties with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Hold or it's properties with ID {id} not found.");
                }
                return new HoldDTO
                {
                    HoldId = hold.HoldId,
                    UserId = hold.UserId,
                    BookId = hold.BookId,
                    Date = hold.Date,
                    ReleaseDate = hold.ReleaseDate,
                    Status = hold.Status,
                    User = new UserDTO
                    {
                        UserId = hold.User.UserId,
                        FirstName = hold.User.FirstName,
                        LastName = hold.User.LastName,
                        Email = hold.User.Email,
                        Phone = hold.User.Phone,
                    },
                    Book = new BookDTO
                    {
                        BookId = hold.Book.BookId,
                        BookTitle = hold.Book.BookTitle,
                        BookDescription = hold.Book.BookDescription,
                        BookQuantity = hold.Book.BookQuantity,
                    }
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Hold.");
                throw new InvalidOperationException("An error occurred while retrieving the Hold.", ex);
            }
        }

        public async Task<HoldDTO> UpdateHoldAsync(int id, HoldDTO holdDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingHold = await _context.Holds.FindAsync(id);
                if (existingHold == null)
                {
                    _logger.LogWarning("Hold with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Hold with ID {id} not found.");
                }
                existingHold.BookId = holdDTO.HoldId;
                existingHold.UserId = holdDTO.UserId;
                existingHold.BookId = holdDTO.BookId;
                existingHold.Date = holdDTO.Date;
                existingHold.ReleaseDate = holdDTO.ReleaseDate;
                existingHold.Status = holdDTO.Status;

                await _context.SaveChangesAsync();

                var updateHoldFromDb = await _context.Holds
                    .Include(h => h.User)
                    .Include(h => h.Book)
                    .FirstOrDefaultAsync(h => h.HoldId == existingHold.HoldId);

                if (updateHoldFromDb == null || updateHoldFromDb.User == null || updateHoldFromDb.Book == null)
                {
                    _logger.LogWarning("Hold or it's properties with ID {Id} not found after update.", id);
                    throw new KeyNotFoundException($"Hold or it's properties with ID {id} not found after update.");
                }

                return new HoldDTO
                {
                    HoldId = updateHoldFromDb.HoldId,
                    UserId = updateHoldFromDb.UserId,
                    BookId = updateHoldFromDb.BookId,
                    Date = updateHoldFromDb.Date,
                    ReleaseDate = updateHoldFromDb.ReleaseDate,
                    Status = updateHoldFromDb.Status,
                    User = new UserDTO
                    {
                        UserId = updateHoldFromDb.User.UserId,
                        FirstName = updateHoldFromDb.User.FirstName,
                        LastName = updateHoldFromDb.User.LastName,
                        Email = updateHoldFromDb.User.Email,
                        Phone = updateHoldFromDb.User.Phone,
                    },
                    Book = new BookDTO
                    {
                        BookId = updateHoldFromDb.Book.BookId,
                        BookTitle = updateHoldFromDb.Book.BookTitle,
                        BookDescription = updateHoldFromDb.Book.BookDescription,
                        BookQuantity = updateHoldFromDb.Book.BookQuantity,
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Hold.");
                throw new InvalidOperationException("An error occurred while updating the Hold.", ex);
            }
        }
        public async Task<ICollection<HoldDTO>> GetHoldsByUserAsync(int userId)
        {
            var holds = await _context.Holds
                .Where(h => h.UserId == userId)
                .Include(h => h.User)
                .Include(h => h.Book)
                .ToListAsync();

            return holds.Select(h => new HoldDTO
            {
                HoldId = h.HoldId,
                UserId = h.UserId,
                BookId = h.BookId,
                Date = h.Date,
                ReleaseDate = h.ReleaseDate,
                Status = h.Status,
                User = h.User != null ? new UserDTO
                {
                    UserId = h.User.UserId,
                    FirstName = h.User.FirstName,
                    LastName = h.User.LastName,
                    Email = h.User.Email,
                    Phone = h.User.Phone,
                } : null,
                Book = h.Book != null ? new BookDTO
                {
                    BookId = h.Book.BookId,
                    BookTitle = h.Book.BookTitle,
                    BookDescription = h.Book.BookDescription,
                    BookQuantity = h.Book.BookQuantity,
                } : null
            }).ToList();
        }

        public async Task<ICollection<HoldDTO>> GetHoldsByBookAsync(int bookId)
        {
            var holds = await _context.Holds
                .Where(h => h.BookId == bookId)
                .Include(h => h.User)
                .Include(h => h.Book)
                .ToListAsync();

            return holds.Select(h => new HoldDTO
            {
                HoldId = h.HoldId,
                UserId = h.UserId,
                BookId = h.BookId,
                Date = h.Date,
                ReleaseDate = h.ReleaseDate,
                Status = h.Status,
                User = h.User != null ? new UserDTO
                {
                    UserId = h.User.UserId,
                    FirstName = h.User.FirstName,
                    LastName = h.User.LastName,
                    Email = h.User.Email,
                    Phone = h.User.Phone,
                } : null,
                Book = h.Book != null ? new BookDTO
                {
                    BookId = h.Book.BookId,
                    BookTitle = h.Book.BookTitle,
                    BookDescription = h.Book.BookDescription,
                    BookQuantity = h.Book.BookQuantity,
                } : null
            }).ToList();
        }
    }
}
