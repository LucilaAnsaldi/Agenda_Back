using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Models.DTO
{
    public class UserForModificationDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}
