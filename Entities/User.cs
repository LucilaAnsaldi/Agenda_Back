using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Entities

{   // Tabla de entidad Usuario, que puede tener una o más agendas propias
    // y una o más agendas de otros usuarios que solo puede ver
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public ICollection<ContactBook> MyContactBooks { get; set; }  // Son las agendas que pertenecen a este usuario - relación de 1 a muchos
        public ICollection<SharedContactBook> MySharedContactBooks { get; set; }  // Son las agendas compartidas que este usuario puede ver
    }
}
