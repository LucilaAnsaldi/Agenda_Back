using Agenda_Back.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Back.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) //constructor para inyeccion de dbcontext
        {
        }

        // Creamos las tablas de la base de datos

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactBook> ContactBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SharedContactBook> SharedContactBooks { get; set; }


        // lógica de las tablas en la base de datos:
        // un usuario tiene muchos agendas
        // una agenda tiene muchos contactos y muchos usuarios


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<SharedContactBook>().HasKey(s => new { s.ContactBookId, s.UserId });

            
            builder.Entity<SharedContactBook>()
                .HasOne(s => s.ContactBook)
                .WithMany(c => c.SharedContactBooks)
                .HasForeignKey(s => s.ContactBookId);

            builder.Entity<SharedContactBook>()
                .HasOne(s => s.User)
                .WithMany(u => u.SharedContactBooks)
                .HasForeignKey(s => s.UserId);

            
            builder.Entity<Contact>()
                .HasOne(c => c.ContactBook)
                .WithMany(cb => cb.Contacts)
                .HasForeignKey(c => c.ContactBookId);


            // Falta agregar cómo se eliminan las tablas relacionadas (en cascada, no action, etc)
        }
    }
}
