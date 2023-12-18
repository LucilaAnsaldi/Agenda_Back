using Agenda_Back.Models.DTO;

namespace Agenda_Back.Services.Interfaces
{
    public interface IContactService
    {
        public Task<List<ContactDTO>> GetListContactsAsync(int contactBookId);
        public Task<ContactDTO> GetContactByIdAsync(int id);
        public Task<ContactDTO> CreateContactAsync(ContactForCreationDTO contactForCreationDTO);
        public Task DeleteContactAsync(int id);
        public Task<ContactDTO> UpdateContactAsync(int id, ContactForModificationDTO contactForModificationDTO);

    }
}
