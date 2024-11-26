using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(LibraryDatabseContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BookDTO> CreateBookAsync(BookDTO bookDTO)
        {
            if (bookDTO == null || string.IsNullOrWhiteSpace(bookDTO.BookTitle))
            {
                _logger.LogWarning("Invalid BookTitle data provided.");
                throw new ArgumentException("Book data is invalid.");
            }

            var book = new Book
            {
                BookTitle = bookDTO.BookTitle,
                BookDescription = bookDTO.BookDescription,
                BookQuantity = bookDTO.BookQuantity,
            };

            try
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return new BookDTO
                {
                    BookId = book.BookId,
                    BookTitle = book.BookTitle,
                    BookDescription = book.BookDescription,
                    BookQuantity = bookDTO.BookQuantity,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Book.");
                throw new InvalidOperationException("An error occurred while adding the Book.", ex);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning("Book with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Book with ID {id} not found.");
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Book.");
                throw new Exception("An error occurred while removing the Book.", ex);
            }
        }

        public async Task<ICollection<BookDTO>> GetAllBooksAsync()
        {
            try
            {
                var book = await _context.Books.ToListAsync();
                return book.Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    BookTitle = b.BookTitle,
                    BookDescription = b.BookDescription,
                    BookQuantity = b.BookQuantity,
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Books.");
                throw new InvalidOperationException("An error occurred while retrieving the Books.", ex);
            }
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                {
                    _logger.LogWarning("Book with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Book with ID {id} not found.");
                }
                return new BookDTO
                {
                    BookId = book.BookId,
                    BookTitle = book.BookTitle,
                    BookDescription = book.BookDescription,
                    BookQuantity = book.BookQuantity,
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Book.");
                throw new InvalidOperationException("An error occurred while retrieving the Book.", ex);
            }
        }

        public async Task<BookDTO> UpdateBookAsync(int id, BookDTO bookDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                {
                    _logger.LogWarning("Book with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Book with ID {id} not found.");
                }
                existingBook.BookId = id;
                existingBook.BookTitle = bookDTO.BookTitle;
                existingBook.BookDescription = bookDTO.BookDescription;
                existingBook.BookQuantity = bookDTO.BookQuantity;

                await _context.SaveChangesAsync();
                return new BookDTO
                {
                    BookId = existingBook.BookId,
                    BookTitle = existingBook.BookTitle,
                    BookDescription = existingBook.BookDescription,
                    BookQuantity = existingBook.BookQuantity,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Book.");
                throw new InvalidOperationException("An error occurred while updating the Book.", ex);
            }
        }
    }
}
