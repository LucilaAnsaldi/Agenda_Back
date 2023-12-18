namespace Agenda_Back.Models.DTO
{
    public class ContactBookForModificationDTO
    {
        public int Id { get; set; }

        public string ContactBookName { get; set; }

        public string? Description { get; set; }

        public int OwnerUserId { get; set; }

    }
}
