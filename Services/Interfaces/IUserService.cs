using Agenda_Back.Models.DTO;

namespace Agenda_Back.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO?> ValidateUserAsync(AuthenticationRequestBody authRequestBody);
        public Task<UserDTO> CreateUserAsync(UserForCreationDTO userForCreationDTO);
        public Task DeleteUserAsync(int userId);
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        public Task<UserDTO?> GetUserByIdAsync(int userId);
        public Task<UserDTO> UpdateUserAsync(int userId, UserForModificationDTO userForUpdateDTO);
        public Task<List<ContactBookDTO>> GetSharedContactBooksAsync(int userId);
        public Task ShareContactBookAsync(int ownerId, int sharedUserId, int contactBookId);
    }
}
