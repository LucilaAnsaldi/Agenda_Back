using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Models.DTO
{
    public class AuthenticationRequestBody
    {
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
