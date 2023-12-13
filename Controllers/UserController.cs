using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_Back.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult CreateUser(UserDTOCreation userDTOCreation)
        {
            try
            {
                var user = _mapper.Map<User>(userDTOCreation);

                var usersActivos = _userRepository.GetListUsers();

                foreach (var userActivo in usersActivos)
                {
                    if (user.Email == userActivo.Email)
                    {
                        return BadRequest("El email ingresado ya es utilizado en una cuenta activa");
                    }
                }

                var userItem = _userRepository.AddUser(user);

                var userItemDto = _mapper.Map<UserDTO>(userItem);

                return Created("Created", userItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
