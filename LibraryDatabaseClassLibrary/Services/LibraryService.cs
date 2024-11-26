using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<LibraryService> _logger;

        public LibraryService(LibraryDatabseContext context, ILogger<LibraryService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<LibraryDTO> CreateLibraryAsync(LibraryDTO libraryDTO)
        {
            if (libraryDTO == null || string.IsNullOrWhiteSpace(libraryDTO.LibraryName))
            {
                _logger.LogWarning("Invalid LibraryTitle data provided.");
                throw new ArgumentException("Library data is invalid.");
            }

            if (await ExistsLibraryAsync(libraryDTO.LibraryName))
            {
                _logger.LogWarning("An Library with the same name already exists: {LibraryName}", libraryDTO.LibraryName);
                throw new InvalidOperationException("An library with the same name already exists.");
            }

            var library = new Library
            {
                LibraryName = libraryDTO.LibraryName,
                Location = libraryDTO.Location,
            };

            try
            {
                await _context.Libraries.AddAsync(library);
                await _context.SaveChangesAsync();
                return new LibraryDTO
                {
                    LibraryId = library.LibraryId,
                    LibraryName = library.LibraryName,
                    Location = library.Location,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Library.");
                throw new InvalidOperationException("An error occurred while adding the Library.", ex);
            }
        }

        public async Task DeleteLibraryDeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var library = await _context.Libraries.FindAsync(id);
                if (library == null)
                {
                    _logger.LogWarning("Library with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Library with ID {id} not found.");
                }

                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Library.");
                throw new Exception("An error occurred while removing the Library.", ex);
            }
        }

        public async Task<bool> ExistsLibraryAsync(string library)
        {
            return await _context.Libraries.AnyAsync(l => l.LibraryName == library);
        }
        public async Task<ICollection<LibraryDTO>> GetAllLibrariesAsync()
        {
            try
            {
                var libraries = await _context.Libraries.ToListAsync();
                return libraries.Select(l => new LibraryDTO
                {
                    LibraryId = l.LibraryId,
                    LibraryName = l.LibraryName,
                    Location = l.Location,
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Libraries.");
                throw new InvalidOperationException("An error occurred while retrieving the Libraries.", ex);
            }
        }

        public async Task<LibraryDTO> GetLibraryByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var library = await _context.Libraries.FindAsync(id);

                if (library == null)
                {
                    _logger.LogWarning("Library with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Library with ID {id} not found.");
                }
                return new LibraryDTO
                {
                    LibraryId = library.LibraryId,
                    LibraryName = library.LibraryName,
                    Location = library.Location,
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Library.");
                throw new InvalidOperationException("An error occurred while retrieving the Library.", ex);
            }
        }

        public async Task<LibraryDTO> UpdateLibraryAsync(int id, LibraryDTO libraryDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingLibrary = await _context.Libraries.FindAsync(id);
                if (existingLibrary == null)
                {
                    _logger.LogWarning("Library with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Library with ID {id} not found.");
                }
                existingLibrary.LibraryId = libraryDTO.LibraryId;
                existingLibrary.LibraryName = libraryDTO.LibraryName;
                existingLibrary.Location = libraryDTO.Location;

                await _context.SaveChangesAsync();
                return new LibraryDTO
                {
                    LibraryId = existingLibrary.LibraryId,
                    LibraryName = existingLibrary.LibraryName,
                    Location = existingLibrary.Location,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Library.");
                throw new InvalidOperationException("An error occurred while updating the Library.", ex);
            }
        }
    }
}
