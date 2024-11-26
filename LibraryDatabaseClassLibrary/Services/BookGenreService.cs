using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryDatabaseClassLibrary.Services
{
    public class BookGenreService : IBookGenreService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<BookGenreService> _logger;

        public BookGenreService(LibraryDatabseContext context, ILogger<BookGenreService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddBookGenreRelationAsync(int bookId, int genreId)
        {
            if (bookId < 0 || genreId < 0)
            {
                _logger.LogWarning("Invalid id data provided. BookId: {BookId}, GenreId: {GenreId}", bookId, genreId);
                throw new ArgumentException("Id data is invalid.");
            }

            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            if (!await _context.Genres.AnyAsync(g => g.GenreId == genreId))
            {
                _logger.LogWarning("Genre with ID {GenreId} not found.", genreId);
                throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
            }

            var relationExists = await _context.BookGenres
                .AnyAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);

            if (relationExists)
            {
                _logger.LogWarning("Relation between BookId: {BookId} and GenreId: {GenreId} already exists.", bookId, genreId);
                throw new InvalidOperationException("Relation already exists.");
            }

            await _context.BookGenres.AddAsync(new BookGenre
            {
                BookId = bookId,
                GenreId = genreId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookGenreRelationAsync(int bookId, int genreId)
        {
            var relation = await _context.BookGenres
                .FirstOrDefaultAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);

            if (relation == null)
            {
                _logger.LogWarning("Relation between BookId: {BookId} and GenreId: {GenreId} not found.", bookId, genreId);
                throw new KeyNotFoundException("Relation not found.");
            }

            _context.BookGenres.Remove(relation);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<GenreDTO>> GetGenresByBookAsync(int bookId)
        {
            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            var genres = await _context.BookGenres
                .Where(bg => bg.BookId == bookId)
                .Include(bg => bg.Genre)
                .ToListAsync();

            if (genres.Any(g => g.Genre == null))
            {
                _logger.LogError("Some genres could not be found for BookId: {BookId}.", bookId);
                throw new InvalidOperationException("Genre properties could not be found.");
            }

            return genres.Select(g => new GenreDTO
            {
                GenreId = g.Genre!.GenreId,
                GenreName = g.Genre.GenreName
            }).ToList();
        }

        public async Task<ICollection<BookDTO>> GetBooksByGenreAsync(int genreId)
        {
            if (!await _context.Genres.AnyAsync(g => g.GenreId == genreId))
            {
                _logger.LogWarning("Genre with ID {GenreId} not found.", genreId);
                throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
            }

            var books = await _context.BookGenres
                .Where(bg => bg.GenreId == genreId)
                .Include(bg => bg.Book)
                .ToListAsync();

            if (books.Any(b => b.Book == null))
            {
                _logger.LogError("Some books could not be found for GenreId: {GenreId}.", genreId);
                throw new InvalidOperationException("Book properties could not be found.");
            }

            return books.Select(b => new BookDTO
            {
                BookId = b.Book!.BookId,
                BookTitle = b.Book.BookTitle,
                BookDescription = b.Book.BookDescription,
                BookQuantity = b.Book.BookQuantity
            }).ToList();
        }
    }
}
