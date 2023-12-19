using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Agenda_Back.Models.DTO;
using Agenda_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Agenda_Back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getById/{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return NotFound($"Usuario con ID {userId} no encontrado");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario con ID {userId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{userId}")] // Endpoint para eliminar la cuenta del usuario
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var user = _userService.GetUserByIdAsync;

                if (user == null)
                {
                    return NotFound();
                }
                else if (userId != userSesionId)
                {
                    return Unauthorized();
                }
                else
                {
                    await _userService.DeleteUserAsync(userId);
                    return NoContent();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar usuario con ID {userId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{userId}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int userId, [FromBody] UserForModificationDTO userForUpdateDTO)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userId, userForUpdateDTO);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar usuario con ID {userId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAll/{userId}/sharedContactBooks")]
        public async Task<ActionResult<List<ContactBookDTO>>> GetSharedContactBooks(int userId)
        {
            try
            {
                var sharedContactBooks = await _userService.GetSharedContactBooksAsync(userId);
                return Ok(sharedContactBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener lista de SharedContactBooks para el usuario con ID {userId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("shareContactBook")]
        public async Task<IActionResult> ShareContactBook([FromQuery] string sharedUserEmail, [FromQuery] int contactBookId)
        {
            try
            {
                int ownerId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                await _userService.ShareContactBookAsync(ownerId, sharedUserEmail, contactBookId);

                return Ok("La agenda se compartió exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al compartir la agenda con ID {contactBookId} con el usuario con correo electrónico {sharedUserEmail}");
                return BadRequest(ex.Message);
            }
        }
    }
}
