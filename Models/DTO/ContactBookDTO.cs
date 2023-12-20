using Agenda_Back.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Models.DTO
{
    public class ContactBookDTO
    {
        public int Id { get; set; }

        public string ContactBookName { get; set; }

        public string? Description { get; set; }

        public int OwnerUserId { get; set; }

        public string OwnerUserName { get; set; }

    }
}
