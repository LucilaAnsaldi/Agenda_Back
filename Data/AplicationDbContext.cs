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
                .WithMany(c => c.SharedUsers)
                .HasForeignKey(s => s.ContactBookId);

            builder.Entity<SharedContactBook>()
                .HasOne(s => s.User)
                .WithMany(u => u.MySharedContactBooks)
                .HasForeignKey(s => s.UserId);
            
            builder.Entity<Contact>()
                .HasOne(c => c.ContactBook)
                .WithMany(cs => cs.Contacts)
                .HasForeignKey(c => c.ContactBookId);

            builder.Entity<ContactBook>()
                .HasOne(c => c.OwnerUser)
                .WithMany(cb => cb.MyContactBooks)
                .HasForeignKey(c => c.OwnerUserId);

            builder.Entity<ContactBook>()
                .HasMany(cb => cb.SharedUsers)
                .WithOne(s => s.ContactBook)
                .HasForeignKey(s => s.ContactBookId)
                .OnDelete(DeleteBehavior.Restrict); // Cambiado de Cascade a Restrict

            // Falta agregar cómo se eliminan las tablas relacionadas (en cascada, no action, etc)
        }
    }
}
