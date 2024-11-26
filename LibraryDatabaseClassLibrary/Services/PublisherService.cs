using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<PublisherService> _logger;

        public PublisherService(LibraryDatabseContext context, ILogger<PublisherService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<PublisherDTO> CreatePublisherAsync(PublisherDTO publisherDTO)
        {
            if (publisherDTO == null || string.IsNullOrWhiteSpace(publisherDTO.PublisherName))
            {
                _logger.LogWarning("Invalid PublisherTitle data provided.");
                throw new ArgumentException("Publisher data is invalid.");
            }

            if (await ExistsPublisherAsync(publisherDTO.PublisherName))
            {
                _logger.LogWarning("An Publisher with the same name already exists: {PublisherName}", publisherDTO.PublisherName);
                throw new InvalidOperationException("An Publisher with the same name already exists.");
            }

            var publisher = new Publisher
            {
                PublisherId = publisherDTO.PublisherId,
                PublisherName = publisherDTO.PublisherName,
            };

            try
            {
                await _context.Publishers.AddAsync(publisher);
                await _context.SaveChangesAsync();
                return new PublisherDTO
                {
                    PublisherId = publisher.PublisherId,
                    PublisherName = publisher.PublisherName,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the Publisher.");
                throw new InvalidOperationException("An error occurred while adding the Publisher.", ex);
            }
        }

        public async Task DeletePublisherAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var publisher = await _context.Publishers.FindAsync(id);
                if (publisher == null)
                {
                    _logger.LogWarning("Publisher with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Publisher with ID {id} not found.");
                }

                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the Publisher.");
                throw new Exception("An error occurred while removing the Publisher.", ex);
            }
        }

        public async Task<bool> ExistsPublisherAsync(string publisher)
        {
            return await _context.Publishers.AnyAsync(p => p.PublisherName == publisher);
        }
        public async Task<ICollection<PublisherDTO>> GetAllPublishersAsync()
        {
            try
            {
                var publishers = await _context.Publishers.ToListAsync();
                return publishers.Select(p => new PublisherDTO
                {
                    PublisherId = p.PublisherId,
                    PublisherName = p.PublisherName,
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Publishers.");
                throw new InvalidOperationException("An error occurred while retrieving the Publishers.", ex);
            }
        }

        public async Task<PublisherDTO> GetPublisherByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var publisher = await _context.Publishers.FindAsync(id);

                if (publisher == null)
                {
                    _logger.LogWarning("Publisher with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Publisher with ID {id} not found.");
                }
                return new PublisherDTO
                {
                    PublisherId = publisher.PublisherId,
                    PublisherName = publisher.PublisherName,
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Publisher.");
                throw new InvalidOperationException("An error occurred while retrieving the Publisher.", ex);
            }
        }

        public async Task<PublisherDTO> UpdatePublisherAsync(int id, PublisherDTO publisherDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingPublisher = await _context.Publishers.FindAsync(id);
                if (existingPublisher == null)
                {
                    _logger.LogWarning("Publisher with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"Publisher with ID {id} not found.");
                }
                existingPublisher.PublisherId = publisherDTO.PublisherId;
                existingPublisher.PublisherName = publisherDTO.PublisherName;

                await _context.SaveChangesAsync();
                return new PublisherDTO
                {
                    PublisherId = existingPublisher.PublisherId,
                    PublisherName = existingPublisher.PublisherName,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Publisher.");
                throw new InvalidOperationException("An error occurred while updating the Publisher.", ex);
            }
        }
    }
}
