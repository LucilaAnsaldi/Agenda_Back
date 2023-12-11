using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Entities
{   // Tabla de entidad contactos que pertenecen a una agenda
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }

        [ForeignKey("ContactBookId")]
        public int ContactBookId { get; set; } //relacion muchos contactos a 1 agenda
        public ContactBook ContactBook { get; set; }
    }
}

