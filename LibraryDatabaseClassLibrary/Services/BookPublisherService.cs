using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryDatabaseClassLibrary.Services
{
    public class BookPublisherService : IBookPublisherService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<BookPublisherService> _logger;

        public BookPublisherService(LibraryDatabseContext context, ILogger<BookPublisherService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddBookPublisherRelationAsync(int bookId, int publisherId)
        {
            if (bookId < 0 || publisherId < 0)
            {
                _logger.LogWarning("Invalid id data provided. BookId: {BookId}, PublisherId: {PublisherId}", bookId, publisherId);
                throw new ArgumentException("Id data is invalid.");
            }

            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            if (!await _context.Publishers.AnyAsync(p => p.PublisherId == publisherId))
            {
                _logger.LogWarning("Publisher with ID {PublisherId} not found.", publisherId);
                throw new KeyNotFoundException($"Publisher with ID {publisherId} not found.");
            }

            var relationExists = await _context.BookPublishers
                .AnyAsync(bp => bp.BookId == bookId && bp.PublisherId == publisherId);

            if (relationExists)
            {
                _logger.LogWarning("Relation between BookId: {BookId} and PublisherId: {PublisherId} already exists.", bookId, publisherId);
                throw new InvalidOperationException("Relation already exists.");
            }

            await _context.BookPublishers.AddAsync(new BookPublisher
            {
                BookId = bookId,
                PublisherId = publisherId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookPublisherRelationAsync(int bookId, int publisherId)
        {
            var relation = await _context.BookPublishers
                .FirstOrDefaultAsync(bp => bp.BookId == bookId && bp.PublisherId == publisherId);

            if (relation == null)
            {
                _logger.LogWarning("Relation between BookId: {BookId} and PublisherId: {PublisherId} not found.", bookId, publisherId);
                throw new KeyNotFoundException("Relation not found.");
            }
            _context.BookPublishers.Remove(relation);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<PublisherDTO>> GetPublishersByBookAsync(int bookId)
        {
            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            var publishers = await _context.BookPublishers
                .Where(bp => bp.BookId == bookId)
                .Include(bp => bp.Publisher)
                .ToListAsync();

            if (publishers.Any(p => p.Publisher == null))
            {
                _logger.LogError("Some publishers could not be found for BookId: {BookId}.", bookId);
                throw new InvalidOperationException("Publisher properties could not be found.");
            }

            return publishers.Select(p => new PublisherDTO
            {
                PublisherId = p.Publisher!.PublisherId,
                PublisherName = p.Publisher.PublisherName,
            }).ToList();
        }

        public async Task<ICollection<BookDTO>> GetBooksByPublisherAsync(int publisherId)
        {
            if (!await _context.Publishers.AnyAsync(p => p.PublisherId == publisherId))
            {
                _logger.LogWarning("Publisher with ID {PublisherId} not found.", publisherId);
                throw new KeyNotFoundException($"Publisher with ID {publisherId} not found.");
            }

            var books = await _context.BookPublishers
                .Where(bp => bp.PublisherId == publisherId)
                .Include(bp => bp.Book)
                .ToListAsync();

            if (books.Any(b => b.Book == null))
            {
                _logger.LogError("Some books could not be found for PublisherId: {PublisherId}.", publisherId);
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
