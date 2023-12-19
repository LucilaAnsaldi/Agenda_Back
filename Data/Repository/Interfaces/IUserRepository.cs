using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> ValidateUserAsync(AuthenticationRequestBody authRequestBody);
        public Task<User> CreateUserAsync(User user);
        public Task DeleteUserAsync(User user);
        public Task<List<User>> GetListUsersAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task<UserDTO?> UpdateUserDataAsync(User user);
        public Task<List<ContactBook>> GetSharedContactBooksAsync(int userId);
        public Task ShareContactBookAsync(int ownerId, string sharedUserEmail, int contactBookId);

    }
}
