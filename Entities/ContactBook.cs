using System.Diagnostics.Contracts;

namespace Agenda_Back.Entities
{   // Tabla Agenda que tiene una lista de contactos propios
    // y una lista de usuarios que pueden compartir esta agenda
    public class ContactBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public IEnumerable<Contact> Contacts { get; set; } //relacion 1 a muchos contactos

        public IEnumerable<SharedContactBook> SharedContactBooks { get; set; } //relacion de 1 a muchos usuarios
        
    }
}
