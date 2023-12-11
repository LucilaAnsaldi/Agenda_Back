using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using AutoMapper;

namespace Agenda_Back.Data.Repository.Implementation
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactBookRepository(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ContactBook> GetListContactBooks()
        {
            return _context.ContactBooks.ToList();

        }

        public ContactBook GetContactBookById(int contactBookId)
        {
            return _context.ContactBooks.SingleOrDefault(a => a.Id == contactBookId);

        }

        public int CreateContactBook(ContactBook contactBook)
        {
            _context.ContactBooks.Add(contactBook);

            _context.SaveChanges();

            var id = contactBook.Id;

            return id;
        }

        public void DeleteContactBook(ContactBook contactBook)
        {
            _context.ContactBooks.Remove(contactBook);
            _context.SaveChanges();
        }
        public void UpdateContactBook(ContactBook contactBook)
        {
            var contactBookItem = _context.ContactBooks.FirstOrDefault(a => a.Id == contactBook.Id);

            if (contactBookItem != null)
            {
                contactBookItem.Name = contactBook.Name;

                _context.SaveChanges();
            }
        }

    }
}
