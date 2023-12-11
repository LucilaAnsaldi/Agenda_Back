using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Interfaces
{
    public interface ISharedContactBookRepository
    {
        public List<SharedContactBook> GetSharedContactBooks(int userId);
        public void addSharedContactBook(int userId, int contactBookId);
        public void DeleteSharedContactBook(int contactBookId);
    }
}
