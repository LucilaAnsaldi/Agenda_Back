using Agenda_Back.Models.DTO;

namespace Agenda_Back.Services.Interfaces
{
    public interface IContactBookService
    {
        public Task<List<ContactBookDTO>> GetContactBooksByUserIdAsync(int userId);
        public Task<ContactBookDTO?> GetContactBookByIdAsync(int contactBookId);
        public Task<int> CreateContactBookAsync(ContactBookDTO contactBookDTO, int ownerUserId);
        public Task DeleteContactBookAsync(int contactBookId);
    }
}
