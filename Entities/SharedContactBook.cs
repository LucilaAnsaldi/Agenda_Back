// SharedContactBook.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_Back.Entities
{
    // Tabla que representa el libro de contactos compartido entre usuarios
    public class SharedContactBook
    {
        public int Id { get; set; }

        // Relación con el usuario que compartió el libro de contactos
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Relación con el libro de contactos compartido
        [ForeignKey("ContactBookId")]
        public int ContactBookId { get; set; }
        public ContactBook ContactBook { get; set; }
    }
}
