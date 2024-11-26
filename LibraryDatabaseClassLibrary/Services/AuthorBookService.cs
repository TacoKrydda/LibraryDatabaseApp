using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryDatabaseClassLibrary.Services
{
    public class AuthorBookService : IAuthorBookService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<AuthorBookService> _logger;

        public AuthorBookService(LibraryDatabseContext context, ILogger<AuthorBookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAuthorBookRelationAsync(int authorId, int bookId)
        {
            if (authorId < 0 || bookId < 0)
            {
                _logger.LogWarning("Invalid id data provided.");
                throw new ArgumentException("Id data is invalid.");
            }

            if (!await _context.Authors.AnyAsync(a => a.AuthorId == authorId))
            {
                _logger.LogWarning("Author with ID {AuthorId} not found.", authorId);
                throw new KeyNotFoundException($"Author with ID {authorId} not found.");
            }

            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            if (await _context.AuthorBooks.AnyAsync(ab => ab.AuthorId == authorId && ab.BookId == bookId))
            {
                _logger.LogWarning("Relation between AuthorId: {AuthorId} and BookId: {BookId} already exists.", authorId, bookId);
                throw new InvalidOperationException("Relation already exists.");
            }

            await _context.AuthorBooks.AddAsync(new AuthorBook
            {
                AuthorId = authorId,
                BookId = bookId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAuthorBookRelationAsync(int authorId, int bookId)
        {
            var relation = await _context.AuthorBooks
                .FirstOrDefaultAsync(ab => ab.AuthorId == authorId && ab.BookId == bookId);

            if (relation == null)
            {
                _logger.LogWarning("Relation between AuthorId: {AuthorId} and BookId: {BookId} not found.", authorId, bookId);
                throw new KeyNotFoundException("Relation not found.");
            }

            _context.AuthorBooks.Remove(relation);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<BookDTO>> GetBooksByAuthorAsync(int authorId)
        {
            if (!await _context.Authors.AnyAsync(a => a.AuthorId == authorId))
            {
                _logger.LogWarning("Author with ID {AuthorId} not found.", authorId);
                throw new KeyNotFoundException($"Author with ID {authorId} not found.");
            }

            var books = await _context.AuthorBooks
                .Where(ab => ab.AuthorId == authorId)
                .Include(ab => ab.Book)
                .ToListAsync();

            if (books.Any(b => b.Book == null))
            {
                _logger.LogError("Some books could not be found for AuthorId: {AuthorId}.", authorId);
                throw new InvalidOperationException("Book properties could not be found.");
            }

            return books.Select(b => new BookDTO
            {
                BookId = b.Book!.BookId, // Null-forgiving operator!
                BookTitle = b.Book.BookTitle,
                BookDescription = b.Book.BookDescription,
                BookQuantity = b.Book.BookQuantity,
            }).ToList();
        }

        public async Task<ICollection<AuthorDTO>> GetAuthorsByBookAsync(int bookId)
        {
            if (!await _context.Books.AnyAsync(b => b.BookId == bookId))
            {
                _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            var authors = await _context.AuthorBooks
                .Where(ab => ab.BookId == bookId)
                .Include(ab => ab.Author)
                .ToListAsync();

            if (authors.Any(a => a.Author == null))
            {
                _logger.LogError("Some authors could not be found for BookId: {BookId}.", bookId);
                throw new InvalidOperationException("Author properties could not be found.");
            }

            return authors.Select(a => new AuthorDTO
            {
                AuthorId = a.Author!.AuthorId, // Null-forgiving operator!
                AuthorName = a.Author.AuthorName,
                AuthorDescription = a.Author.AuthorDescription,
            }).ToList();
        }
    }

}
