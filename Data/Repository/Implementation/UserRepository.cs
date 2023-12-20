using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_Back.Data.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;

        public UserRepository(AplicationDbContext context, IMapper mapper, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<User?> ValidateUserAsync(AuthenticationRequestBody authRequestBody)
        {
            try
            {
                return await Task.Run(() =>
                    _context.Users.FirstOrDefaultAsync(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar usuario");
                throw; 
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar usuario");
                throw;
            }
        }

        public async Task DeleteUserAsync(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario");
                throw;
            }
        }

        public async Task<List<User>> GetListUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista de usuarios");
                throw;
            }
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario con ID {id}");
                throw;
            }
        }

        public async Task<UserDTO?> UpdateUserDataAsync(User user)
        {
            try
            {
                var userItem = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                if (userItem != null)
                {
                    userItem.Id = user.Id;
                    userItem.Name = user.Name;
                    userItem.Email = user.Email;
                    userItem.PhoneNumber = user.PhoneNumber;
                    userItem.Password = user.Password;

                    await _context.SaveChangesAsync();

                    return _mapper.Map<UserDTO>(userItem);
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar datos de usuario con ID {user.Id}");
                throw;
            }
        }

        public async Task<List<ContactBook>> GetSharedContactBooksAsync(int userId)
        {
            try
            {
                var sharedContactBooks = await _context.SharedContactBooks
                    .Where(sc => sc.UserId == userId)
                    .Include(sc => sc.ContactBook)
                        .ThenInclude(cb => cb.OwnerUser)  // Incluir la información del propietario
                    .Select(sc => sc.ContactBook)
                    .ToListAsync();

                return sharedContactBooks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener lista de SharedContactBooks para el usuario con ID {userId}");
                throw;
            }
        }


        public async Task ShareContactBookAsync(int ownerId, string sharedUserEmail, int contactBookId)
        {
            try
            {
                var owner = await _context.Users
                    .Include(u => u.MyContactBooks)
                    .FirstOrDefaultAsync(u => u.Id == ownerId);

                var sharedUser = await _context.Users
                    .Include(u => u.MySharedContactBooks)
                    .FirstOrDefaultAsync(u => u.Email == sharedUserEmail);

                if (owner == null || sharedUser == null)
                {
                    throw new Exception("Usuario o usuario compartido no encontrado");
                }

                var contactBook = owner.MyContactBooks.FirstOrDefault(cb => cb.Id == contactBookId);

                if (contactBook == null)
                {
                    throw new Exception("Agenda no encontrada");
                }

                // Asegurémonos de que no estemos compartiendo la misma agenda más de una vez
                if (sharedUser.MySharedContactBooks.Any(sc => sc.ContactBookId == contactBookId))
                {
                    throw new Exception($"La agenda con ID {contactBookId} ya está compartida a ese usuario");
                }

                var sharedContactBook = new SharedContactBook
                {
                    UserId = sharedUser.Id,
                    ContactBookId = contactBookId
                    // Otros campos que puedan ser necesarios...
                };

                _context.SharedContactBooks.Add(sharedContactBook);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al compartir la agenda con ID {contactBookId} con el usuario");
                throw;
            }
        }
    
    }
}



