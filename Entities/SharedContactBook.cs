// SharedContactBook.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_Back.Entities
{
    // Tabla que representa la agenda que fue compartida, 
    // y los usuarios que pueden ver esa agenda
    public class SharedContactBook
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public int ContactBookId { get; set; }

        // Usuarios que pueden ver esta agenda (SharedUsers), es decir, los usuarios a los que le compartieron esta agenda
        [ForeignKey("UserId")]
        public User User { get; set; }

        // Agenda que fue compartida, es decir que pueden ver otros usuarios
        [ForeignKey("ContactBookId")]
        public ContactBook ContactBook { get; set; }
    }
}
