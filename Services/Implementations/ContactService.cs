using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using Agenda_Back.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda_Back.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactRepository contactRepository, IMapper mapper, ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ContactDTO>> GetListContactsAsync(int contactBookId)
        {
            try
            {
                var contacts = await _contactRepository.GetListContactsAsync(contactBookId);
                return _mapper.Map<List<ContactDTO>>(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la lista de contactos para la agenda con ID {contactBookId}");
                throw;
            }
        }

        public async Task<ContactDTO> GetContactByIdAsync(int id)
        {
            try
            {
                var contact = await _contactRepository.GetContactByIdAsync(id);
                return _mapper.Map<ContactDTO>(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el contacto con ID {id}");
                throw;
            }
        }

        public async Task<ContactDTO> CreateContactAsync(ContactForCreationDTO contactForCreationDTO)
        {
            try
            {
                var contact = _mapper.Map<Contact>(contactForCreationDTO);
                var createdContact = await _contactRepository.CreateContactAsync(contact);
                return _mapper.Map<ContactDTO>(createdContact);
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
                await _contactRepository.DeleteContactAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el contacto con ID {id}");
                throw;
            }
        }

        public async Task<ContactDTO> UpdateContactAsync(int id, ContactForModificationDTO contactForModificationDTO)
        {
            try
            {
                var contact = _mapper.Map<Contact>(contactForModificationDTO);
                contact.Id = id;

                // Llama al método del repositorio y recupera el contacto actualizado
                var updatedContact = await _contactRepository.UpdateContactAsync(contact);

                // Mapea el contacto actualizado al DTO y lo retorna
                return _mapper.Map<ContactDTO>(updatedContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el contacto con ID {id}");
                throw;
            }
        }
    }
}
