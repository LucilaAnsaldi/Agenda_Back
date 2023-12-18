namespace Agenda_Back.Models.DTO
{
    public class ContactBookForCreationDTO
    {
        public string ContactBookName { get; set; }

        public string? Description { get; set; }

        public int OwnerUserId { get; set; }
    }
}
