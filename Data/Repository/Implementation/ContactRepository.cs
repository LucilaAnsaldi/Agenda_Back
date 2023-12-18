using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_Back.Data.Repository.Implementation
{
    public class ContactRepository : IContactRepository
    {
        private readonly AplicationDbContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(AplicationDbContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Contact>> GetListContactsAsync(int contactBookId)
        {
            try
            {
                return await _context.Contacts
                    .Where(c => c.ContactBookId == contactBookId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la lista de contactos para la agenda con ID {contactBookId}");
                throw;
            }
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            try
            {
                return await _context.Contacts.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el contacto con ID {id}");
                throw;
            }
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el contacto");
                throw;
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);
                if (contact != null)
                {
                    _context.Contacts.Remove(contact);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el contacto con ID {id}");
                throw;
            }
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            try
            {
                var contactItem = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contact.Id);

                if (contactItem != null)
                {
                    contactItem.Id = contact.Id;
                    contactItem.Name = contact.Name;
                    contactItem.LastName = contact.LastName;
                    contactItem.Description = contact.Description;
                    contactItem.Email = contact.Email;
                    contactItem.PhoneNumber = contact.PhoneNumber;
                    contactItem.Location = contact.Location;
                    contactItem.ContactBookId = contact.ContactBookId;

                    await _context.SaveChangesAsync();

                    // Retorna el contacto actualizado
                    return contactItem;
                }

                return null; // O lanza una excepción indicando que no se encontró el contacto
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el contacto con ID {contact.Id}");
                throw;
            }
        }

    }
}
