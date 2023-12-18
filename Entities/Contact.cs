using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Entities
{   // Tabla de entidad contactos que pertenecen a una agenda
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        [MaxLength(30)]   
        public string? Description { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone] 
        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public int ContactBookId { get; set; } 


        [ForeignKey("ContactBookId")]
        public ContactBook ContactBook { get; set; } //relacion muchos contactos a 1 agenda
    }
}

