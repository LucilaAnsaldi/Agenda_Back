using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        public Task<List<Contact>> GetListContactsAsync(int contactBookId);
        public Task<Contact> GetContactByIdAsync(int id);
        public Task<Contact> CreateContactAsync(Contact contact);
        public Task DeleteContactAsync(int id);
        public Task<Contact> UpdateContactAsync(Contact contact);
    }
}
