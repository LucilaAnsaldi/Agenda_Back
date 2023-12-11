using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface IContactBookRepository
    {
        public List<ContactBook> GetListContactBooks();
        public ContactBook GetContactBookById(int contactBookId);
        public int CreateContactBook(ContactBook contactBook);
        public void DeleteContactBook(ContactBook contactBook);
        public void UpdateContactBook(ContactBook contactBook);
    }
}
