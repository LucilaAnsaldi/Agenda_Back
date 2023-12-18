using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Agenda_Back.Models.DTO;
using Microsoft.Extensions.Logging;
using Agenda_Back.Services.Interfaces;
using Agenda_Back.Entities;

namespace Agenda_Back_Tup1.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration config, IUserService userService, IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Autenticar([FromBody] AuthenticationRequestBody authenticationRequestBody)
        {
            try
            {
                var user = await _userService.ValidateUserAsync(authenticationRequestBody);

                if (user == null)
                {
                    return Unauthorized(new { error = "Credenciales inválidas" });
                }

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
                var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>
                {
                    new Claim("sub", user.Id.ToString()),
                    new Claim("given_name", user.Name),
                    new Claim("family_name", user.Email)
                };

                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    credentials
                );

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return Ok(new { token = tokenToReturn });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> PostUser([FromBody] UserForCreationDTO userDtoCreacion)
        {
            try
            {
                var user = _mapper.Map<User>(userDtoCreacion);
                var usersActivos = await _userService.GetAllUsersAsync();

                foreach (var userActivo in usersActivos)
                {
                    if (user.Email == userActivo.Email)
                    {
                        return BadRequest(new { error = "El email ingresado ya es utilizado en una cuenta activa" });
                    }
                }

                var userItemDto = await _userService.CreateUserAsync(userDtoCreacion);
                var userItem = _mapper.Map<User>(userItemDto);

                return Created("Created", userItem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("userId")]
        public IActionResult GetUserId()
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                return Ok(userSesionId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
