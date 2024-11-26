using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LibraryDatabaseClassLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryDatabseContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(LibraryDatabseContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null || string.IsNullOrWhiteSpace(userDTO.Email))
            {
                _logger.LogWarning("Invalid User Email data provided.");
                throw new ArgumentException("User data is invalid.");
            }

            if (await ExistsUserAsync(userDTO.Email))
            {
                _logger.LogWarning("An User with the same mail already exists: {Email}", userDTO.Email);
                throw new InvalidOperationException("An User with the same mail already exists.");
            }

            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
            };

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return new UserDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the User.");
                throw new InvalidOperationException("An error occurred while adding the User.", ex);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while removing the User.");
                throw new Exception("An error occurred while removing the User.", ex);
            }
        }

        public async Task<bool> ExistsUserAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<ICollection<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return users.Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                }).ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Users.");
                throw new InvalidOperationException("An error occurred while retrieving the Users.", ex);
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }
                return new UserDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the User.");
                throw new InvalidOperationException("An error occurred while retrieving the User.", ex);
            }
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO userDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID value", nameof(id));
            }

            try
            {
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    _logger.LogWarning("User with ID {Id} not found.", id);
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }
                existingUser.UserId = userDTO.UserId;
                existingUser.FirstName = userDTO.FirstName;
                existingUser.LastName = userDTO.LastName;
                existingUser.Email = userDTO.Email;
                existingUser.Phone = userDTO.Phone;

                await _context.SaveChangesAsync();
                return new UserDTO
                {
                    UserId = existingUser.UserId,
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = existingUser.Email,
                    Phone = existingUser.Phone,
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the User.");
                throw new InvalidOperationException("An error occurred while updating the User.", ex);
            }
        }
    }
}
