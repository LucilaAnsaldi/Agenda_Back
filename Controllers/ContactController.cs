using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using Agenda_Back.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda_Back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Contact")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, IMapper mapper, ILogger<ContactController> logger)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("getAllContactsFromContactBook/{contactBookId}")]
        public async Task<ActionResult<List<ContactDTO>>> GetContacts(int contactBookId)
        {
            try
            {
                var contacts = await _contactService.GetListContactsAsync(contactBookId);
                var contactDTOs = _mapper.Map<List<ContactDTO>>(contacts);

                return Ok(contactDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la lista de contactos para la agenda con ID {contactBookId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getContactById/{contactId}")]
        public async Task<ActionResult<ContactDTO>> GetContact(int contactId)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(contactId);

                if (contact == null)
                {
                    return NotFound($"Contacto con ID {contactId} no encontrado");
                }

                var contactDTO = _mapper.Map<ContactDTO>(contact);

                return Ok(contactDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el contacto con ID {contactId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createContact")]
        public async Task<ActionResult<ContactDTO>> CreateContact([FromBody] ContactForCreationDTO contactForCreationDTO)
        {
            try
            {
                var createdContactDTO = await _contactService.CreateContactAsync(contactForCreationDTO);
                return Created("Created", createdContactDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el contacto");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateContact/{contactId}")]
        public async Task<ActionResult<ContactDTO>> UpdateContact(int contactId, [FromBody] ContactForModificationDTO contactForModificationDTO)
        {
            try
            {
                var updatedContact = await _contactService.UpdateContactAsync(contactId, contactForModificationDTO);

                if (updatedContact == null)
                {
                    return NotFound($"Contacto con ID {contactId} no encontrado");
                }

                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el contacto con ID {contactId}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("deleteContact/{contactId}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            try
            {
                var existingContact = await _contactService.GetContactByIdAsync(contactId);

                if (existingContact == null)
                    return NotFound($"Contacto con ID {contactId} no encontrado");

                await _contactService.DeleteContactAsync(contactId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el contacto con ID {contactId}");
                return BadRequest(ex.Message);
            }
        }
    }
}
