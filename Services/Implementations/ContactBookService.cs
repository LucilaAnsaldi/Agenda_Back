using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using Agenda_Back.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Agenda_Back.Services.Implementation
{
    public class ContactBookService : IContactBookService
    {
        private readonly IContactBookRepository _contactBookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactBookService> _logger;

        public ContactBookService(IContactBookRepository contactBookRepository, IMapper mapper, ILogger<ContactBookService> logger)
        {
            _contactBookRepository = contactBookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ContactBookDTO>> GetContactBooksByUserIdAsync(int userId)
        {
            try
            {
                var contactBooks = await _contactBookRepository.GetContactBooksByUserIdAsync(userId);
                return _mapper.Map<List<ContactBookDTO>>(contactBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener las agendas del usuario con ID {userId}");
                throw;
            }
        }

        public async Task<ContactBookDTO?> GetContactBookByIdAsync(int contactBookId)
        {
            try
            {
                var contactBook = await _contactBookRepository.GetContactBookByIdAsync(contactBookId);
                return _mapper.Map<ContactBookDTO>(contactBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la agenda con ID {contactBookId}");
                throw;
            }
        }

        public async Task<ContactBookDTO> CreateContactBookAsync(ContactBookForCreationDTO contactBookDTO, int ownerUserId)
        {
            try
            {
                var contactBook = _mapper.Map<ContactBook>(contactBookDTO);
                var createdContactBook = await _contactBookRepository.CreateContactBookAsync(contactBook, ownerUserId);
                return _mapper.Map<ContactBookDTO>(createdContactBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la agenda desde el servicio");
                throw;
            }
        }

        public async Task DeleteContactBookAsync(int contactBookId)
        {
            try
            {
                var contactBook = await _contactBookRepository.GetContactBookByIdAsync(contactBookId);
                if (contactBook != null)
                {
                    await _contactBookRepository.DeleteContactBookAsync(contactBook);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la agenda con ID {contactBookId}");
                throw;
            }
        }
    }
}
