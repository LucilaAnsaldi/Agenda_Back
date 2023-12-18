using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_Back.Data.Repository.Implementation
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactBookRepository> _logger;

        public ContactBookRepository(AplicationDbContext context, IMapper mapper, ILogger<ContactBookRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ContactBook>> GetContactBooksByUserIdAsync(int userId)
        {
            try
            {
                return await _context.ContactBooks
                    .Where(a => a.OwnerUserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener las agendas del usuario con ID {userId}");
                throw;
            }
        }

        public async Task<ContactBook?> GetContactBookByIdAsync(int contactBookId)
        {
            try
            {
                return await _context.ContactBooks
                    .SingleOrDefaultAsync(a => a.Id == contactBookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la agenda con ID {contactBookId}");
                throw;
            }
        }

        public async Task<int> CreateContactBookAsync(ContactBook contactBook, int ownerUserId)
        {
            try
            {
                contactBook.OwnerUserId = ownerUserId;
                _context.ContactBooks.Add(contactBook);
                await _context.SaveChangesAsync();
                return contactBook.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la agenda");
                throw;
            }
        }

        public async Task DeleteContactBookAsync(ContactBook contactBook)
        {
            try
            {
                _context.ContactBooks.Remove(contactBook);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la agenda con ID {contactBook.Id}");
                throw;
            }
        }
    }
}
