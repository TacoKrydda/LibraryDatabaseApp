using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryDatabaseClassLibrary.Services
{
    public class BookLibraryService : IBookLibraryService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<BookLibraryService> _logger;

        public BookLibraryService(LibraryDatabseContext context, ILogger<BookLibraryService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddBookLibraryRelationAsync(int bookId, int libraryId)
        {
            if (bookId < 0 || libraryId < 0)
            {
                _logger.LogWarning("Invalid id data provided. BookId: {BookId}, LibraryId: {LibraryId}", bookId, libraryId);
                throw new ArgumentException("Id data is invalid.");
            }

            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            if (!await _context.Libraries.AnyAsync(l => l.LibraryId == libraryId))
            {
                _logger.LogWarning("Library with ID {LibraryId} not found.", libraryId);
                throw new KeyNotFoundException($"Library with ID {libraryId} not found.");
            }

            var relationExists = await _context.BookLibraries
                .AnyAsync(bl => bl.BookId == bookId && bl.LibraryId == libraryId);

            if (relationExists)
            {
                _logger.LogWarning("Relation between BookId: {BookId} and LibraryId: {LibraryId} already exists.", bookId, libraryId);
                throw new InvalidOperationException("Relation already exists.");
            }

            await _context.BookLibraries.AddAsync(new BookLibrary
            {
                BookId = bookId,
                LibraryId = libraryId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookLibraryRelationAsync(int bookId, int libraryId)
        {
            var relation = await _context.BookLibraries
                .FirstOrDefaultAsync(bl => bl.BookId == bookId && bl.LibraryId == libraryId);

            if (relation == null)
            {
                _logger.LogWarning("Relation between BookId: {BookId} and LibraryId: {LibraryId} not found.", bookId, libraryId);
                throw new KeyNotFoundException("Relation not found.");
            }

            _context.BookLibraries.Remove(relation);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<LibraryDTO>> GetLibrariesByBookAsync(int bookId)
        {
            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            var libraries = await _context.BookLibraries
                .Where(bl => bl.BookId == bookId)
                .Include(bl => bl.Library)
                .ToListAsync();

            if (libraries.Any(l => l.Library == null))
            {
                _logger.LogError("Some libraries could not be found for BookId: {BookId}.", bookId);
                throw new InvalidOperationException("Library properties could not be found.");
            }

            return libraries.Select(l => new LibraryDTO
            {
                LibraryId = l.Library!.LibraryId,
                LibraryName = l.Library.LibraryName,
                Location = l.Library.Location
            }).ToList();
        }

        public async Task<ICollection<BookDTO>> GetBooksByLibraryAsync(int libraryId)
        {
            if (!await _context.Libraries.AnyAsync(l => l.LibraryId == libraryId))
            {
                _logger.LogWarning("Library with ID {LibraryId} not found.", libraryId);
                throw new KeyNotFoundException($"Library with ID {libraryId} not found.");
            }

            var books = await _context.BookLibraries
                .Where(bl => bl.LibraryId == libraryId)
                .Include(bl => bl.Book)
                .ToListAsync();

            if (books.Any(b => b.Book == null))
            {
                _logger.LogError("Some books could not be found for LibraryId: {LibraryId}.", libraryId);
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
