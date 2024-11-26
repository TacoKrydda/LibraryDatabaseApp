using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class GenreService : IGenreService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<GenreService> _logger;

        public GenreService(LibraryDatabseContext context, ILogger<GenreService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenreDTO> CreateGenreAsync(GenreDTO genreDTO)
        {
            if (genreDTO == null || string.IsNullOrWhiteSpace(genreDTO.GenreName))
            {
                _logger.LogWarning("Invalid GenreTitle data provided.");
                throw new ArgumentException("Genre data is invalid.");
            }

            if (await ExistsGenreAsync(genreDTO.GenreName))
            {
                _logger.LogWarning("An Genre with the same name already exists: {GenreName}", genreDTO.GenreName);
                throw new InvalidOperationException("An Genre with the same name already exists.");
            }

            var genre = new Genre
            {
                GenreName = genreDTO.GenreName,
            };

            try
            {
                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();
                return new GenreDTO
                {
                    GenreId = genre.GenreId,
                    GenreName = genre.GenreName,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Genre.");
                throw new InvalidOperationException("An error occurred while adding the Genre.", ex);
            }
        }

        public async Task DeleteGenreAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var genre = await _context.Genres.FindAsync(id);
                if (genre == null)
                {
                    _logger.LogWarning("Genre with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Genre with ID {id} not found.");
                }

                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Genre.");
                throw new Exception("An error occurred while removing the Genre.", ex);
            }
        }

        public async Task<bool> ExistsGenreAsync(string genre)
        {
            return await _context.Genres.AnyAsync(g => g.GenreName == genre);
        }

        public async Task<ICollection<GenreDTO>> GetAllGenresAsync()
        {
            try
            {
                var book = await _context.Genres.ToListAsync();
                return book.Select(g => new GenreDTO
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName,
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Genres.");
                throw new InvalidOperationException("An error occurred while retrieving the Genres.", ex);
            }
        }

        public async Task<GenreDTO> GetGenreByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var genre = await _context.Genres.FindAsync(id);

                if (genre == null)
                {
                    _logger.LogWarning("Genre with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Genre with ID {id} not found.");
                }
                return new GenreDTO
                {
                    GenreId = genre.GenreId,
                    GenreName = genre.GenreName,
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Genre.");
                throw new InvalidOperationException("An error occurred while retrieving the Genre.", ex);
            }
        }

        public async Task<GenreDTO> UpdateGenreAsync(int id, GenreDTO genreDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingGenre = await _context.Genres.FindAsync(id);
                if (existingGenre == null)
                {
                    _logger.LogWarning("Genre with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Genre with ID {id} not found.");
                }
                existingGenre.GenreId = id;
                existingGenre.GenreName = genreDTO.GenreName;

                await _context.SaveChangesAsync();
                return new GenreDTO
                {
                    GenreId = existingGenre.GenreId,
                    GenreName = existingGenre.GenreName,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Genre.");
                throw new InvalidOperationException("An error occurred while updating the Genre.", ex);
            }
        }
    }
}
