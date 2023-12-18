using Agenda_Back.Models.DTO;

namespace Agenda_Back.Services.Interfaces
{
    public interface IContactBookService
    {
        public Task<List<ContactBookDTO>> GetContactBooksByUserIdAsync(int userId);
        public Task<ContactBookDTO?> GetContactBookByIdAsync(int contactBookId);
        public Task<ContactBookDTO> CreateContactBookAsync(ContactBookForCreationDTO contactBookDTO, int ownerUserId);
        public Task DeleteContactBookAsync(int contactBookId);
    }
}
