using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using Agenda_Back.Services.Interfaces;
using AutoMapper;

namespace Agenda_Back.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO?> ValidateUserAsync(AuthenticationRequestBody authRequestBody)
        {
            try
            {
                var user = await _userRepository.ValidateUserAsync(authRequestBody);
                return user != null ? _mapper.Map<UserDTO>(user) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar usuario");
                throw;
            }
        }

        public async Task<UserDTO> CreateUserAsync(UserForCreationDTO userForCreationDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userForCreationDTO);
                var createdUser = await _userRepository.CreateUserAsync(user);
                return _mapper.Map<UserDTO>(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar usuario");
                throw;
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    _logger.LogWarning($"Usuario con ID {userId} no encontrado");
                }
                else
                {
                    await _userRepository.DeleteUserAsync(user);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar usuario con ID {userId}");
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetListUsersAsync();
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                throw;
            }
        }

        public async Task<UserDTO?> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    _logger.LogWarning($"Usuario con ID {userId} no encontrado");
                    return null;
                }

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario con ID {userId}");
                throw;
            }
        }

        public async Task<UserDTO> UpdateUserAsync(int userId, UserForModificationDTO userForUpdateDTO)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    _logger.LogWarning($"Usuario con ID {userId} no encontrado");
                    throw new Exception($"Usuario con ID {userId} no encontrado");
                }

                _mapper.Map(userForUpdateDTO, user);
                await _userRepository.UpdateUserDataAsync(user);

                // Recuperar el usuario actualizado y mapearlo a DTO
                var updatedUser = await _userRepository.GetUserByIdAsync(userId);
                return _mapper.Map<UserDTO>(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar usuario con ID {userId}");
                throw;
            }
        }

        public async Task<List<ContactBookDTO>> GetMyContactBooksAsync(int userId)
        {
            try
            {
                var contactBooks = await _userRepository.GetMyContactBooksAsync(userId);

                var contactBookDTOs = _mapper.Map<List<ContactBookDTO>>(contactBooks);

                return contactBookDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener lista de ContactBooks para el usuario con ID {userId}");
                throw;
            }
        }


        public async Task<List<ContactBookDTO>> GetSharedContactBooksAsync(int userId)
        {
            try
            {
                var sharedContactBooks = await _userRepository.GetSharedContactBooksAsync(userId);

                var contactBookDTOs = _mapper.Map<List<ContactBookDTO>>(sharedContactBooks);

                return contactBookDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener lista de SharedContactBooks para el usuario con ID {userId}");
                throw;
            }
        }


        public async Task ShareContactBookAsync(int ownerId, int sharedUserId, int contactBookId)
        {
            try
            {
                await _userRepository.ShareContactBookAsync(ownerId, sharedUserId, contactBookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al compartir la agenda con ID {contactBookId} con el usuario con ID {sharedUserId}");
                throw;
            }
        }
    }
}
