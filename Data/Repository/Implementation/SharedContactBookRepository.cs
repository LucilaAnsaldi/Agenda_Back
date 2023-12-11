using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using AutoMapper;

namespace Agenda_Back.Data.Repository.Implementation
{
    public class SharedContactBookRepository : ISharedContactBookRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        public SharedContactBookRepository(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<SharedContactBook> GetSharedContactBooks(int userId)
        {
            return _context.SharedContactBooks.Where(au => au.UserId == userId).ToList();

        }
        public void addSharedContactBook(int userId, int contactBookId)
        {
            SharedContactBook sharedContactBook = new SharedContactBook();
            {
                sharedContactBook.ContactBookId = contactBookId;
                sharedContactBook.UserId = userId;

            };

            _context.SharedContactBooks.Add(sharedContactBook);

            _context.SaveChanges();


        }


        public void DeleteSharedContactBook(int contactBookId)
        {
            var contactBook = _context.SharedContactBooks.Where(au => au.ContactBookId == contactBookId).ToList();

            foreach (var c in contactBook)
            {
                _context.SharedContactBooks.Remove(c);
                _context.SaveChanges();
            }



        }
    }
}
