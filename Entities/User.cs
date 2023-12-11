using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Entities

{   // Tabla de entidad Usuarios que puede tener muchas agendas
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public IEnumerable<SharedContactBook> SharedContactBooks { get; set; }
    }
}
