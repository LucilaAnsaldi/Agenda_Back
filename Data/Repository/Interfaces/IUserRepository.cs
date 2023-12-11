using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User ValidateUser(AuthenticationRequestBody authRequestBody);
        public List<User> GetListUsers();
        public User GetUser(int id);
        public void DeleteUser(User user);
        public User AddUser(User user);
        public void UpdateUserData(User user);
    }
}
