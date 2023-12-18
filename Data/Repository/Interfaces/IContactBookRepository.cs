using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface IContactBookRepository
    {
        public Task<List<ContactBook>> GetContactBooksByUserIdAsync(int userId);
        public Task<ContactBook?> GetContactBookByIdAsync(int contactBookId);
        public Task<ContactBook> CreateContactBookAsync(ContactBook contactBook, int ownerUserId);
        public Task DeleteContactBookAsync(ContactBook contactBook);
    }
}
