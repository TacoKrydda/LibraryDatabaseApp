using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(LibraryDatabseContext context, ILogger<AuthorService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<AuthorDTO> CreateAuthorAsync(AuthorDTO authorDTO)
        {
            if (authorDTO == null || string.IsNullOrWhiteSpace(authorDTO.AuthorName))
            {
                _logger.LogWarning("Invalid AuthorName data provided.");
                throw new ArgumentException("Author data is invalid.");
            }

            var author = new Author
            {
                AuthorName = authorDTO.AuthorName,
                AuthorDescription = authorDTO.AuthorDescription,
            };

            try
            {
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
                return new AuthorDTO
                {
                    AuthorId = author.AuthorId,
                    AuthorName = author.AuthorName,
                    AuthorDescription = author.AuthorDescription
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Author.");
                throw new InvalidOperationException("An error occurred while adding the Author.", ex);
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("Author with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Author with ID {id} not found.");
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Book.");
                throw new Exception("An error occurred while removing the Book.", ex);
            }
        }

        public async Task<ICollection<AuthorDTO>> GetAllAuthorsAsync()
        {
            try
            {
                var author = await _context.Authors.ToListAsync();
                return author.Select(a => new AuthorDTO
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.AuthorName,
                    AuthorDescription = a.AuthorDescription
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Authors.");
                throw new InvalidOperationException("An error occurred while retrieving the Authors.", ex);
            }
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning("Author with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Author with ID {id} not found.");
                }
                return new AuthorDTO
                {
                    AuthorId = author.AuthorId,
                    AuthorName = author.AuthorName,
                    AuthorDescription = author.AuthorDescription,
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Author.");
                throw new InvalidOperationException("An error occurred while retrieving the Author.", ex);
            }
        }

        public async Task<AuthorDTO> UpdateAuthorAsync(int id, AuthorDTO authorDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingAuthor = await _context.Authors.FindAsync(id);
                if (existingAuthor == null)
                {
                    _logger.LogWarning("Author with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Author with ID {id} not found.");
                }
                existingAuthor.AuthorId = id;
                existingAuthor.AuthorName = authorDTO.AuthorName;
                existingAuthor.AuthorDescription = authorDTO.AuthorDescription;

                await _context.SaveChangesAsync();
                return new AuthorDTO
                {
                    AuthorId = existingAuthor.AuthorId,
                    AuthorName = existingAuthor.AuthorName,
                    AuthorDescription = existingAuthor.AuthorDescription,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Author.");
                throw new InvalidOperationException("An error occurred while updating the Author.", ex);
            }
        }
    }
}
