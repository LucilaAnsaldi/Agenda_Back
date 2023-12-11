using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        public List<Contact> GetListContacts(int contactBookId);
        public Contact GetContact(int id);
        public void DeleteContact(Contact contact);
        public Contact AddContact(Contact contact);
        public void UpdateContact(Contact contact);
    }
}
