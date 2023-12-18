using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Agenda_Back.Models.DTO;
using Agenda_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda_Back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ContactBook")]
    public class ContactBookController : ControllerBase
    {
        private readonly IContactBookService _contactBookService;
        private readonly ILogger<ContactBookController> _logger;

        public ContactBookController(IContactBookService contactBookService, ILogger<ContactBookController> logger)
        {
            _contactBookService = contactBookService;
            _logger = logger;
        }

        [HttpGet("getByUserId/{userId}")]
        public async Task<ActionResult<List<ContactBookDTO>>> GetContactBooksByUserId(int userId)
        {
            try
            {
                var contactBooks = await _contactBookService.GetContactBooksByUserIdAsync(userId);
                return Ok(contactBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener las agendas del usuario con ID {userId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getById/{contactBookId}")]
        public async Task<ActionResult<ContactBookDTO>> GetContactBookById(int contactBookId)
        {
            try
            {
                var contactBook = await _contactBookService.GetContactBookByIdAsync(contactBookId);

                if (contactBook == null)
                {
                    return NotFound($"Agenda con ID {contactBookId} no encontrada");
                }

                return Ok(contactBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la agenda con ID {contactBookId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateContactBook([FromBody] ContactBookDTO contactBookDTO)
        {
            try
            {
                int ownerId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var contactBookId = await _contactBookService.CreateContactBookAsync(contactBookDTO, ownerId);
                return Ok(contactBookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la agenda");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{contactBookId}")]
        public async Task<ActionResult> DeleteContactBook(int contactBookId)
        {
            try
            {
                await _contactBookService.DeleteContactBookAsync(contactBookId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la agenda con ID {contactBookId}");
                return BadRequest(ex.Message);
            }
        }
    }
}
