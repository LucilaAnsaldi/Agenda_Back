using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Agenda_Back.Entities
{   // Tabla Agenda que tiene una lista de contactos propios,
    // un propietario de esta Agenda,
    // y una lista de usuarios que pueden ver esta agenda compartida
    public class ContactBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ContactBookName { get; set; }

        [MaxLength(30)]
        public string? Description { get; set; }

        public int OwnerUserId { get; set; }


        [ForeignKey("OwnerUserId")]
        public User OwnerUser { get; set; } //relación de muchas agendas a 1 usuario propietario


        public ICollection<Contact> Contacts { get; set; } //relacion 1 a muchos contactos

        public ICollection<SharedContactBook> SharedUsers { get; set; } //relacion de 1 a muchos usuarios que solo pueden verla
        
    }
}
