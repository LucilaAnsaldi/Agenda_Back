using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;

namespace Agenda_Back.Data.Repository.Implementation
{
    public class ContactRepository : IContactRepository
    {
        private readonly AplicationDbContext _context;

        public ContactRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public List<Contact> GetListContacts(int contactBookId)
        {
            return _context.Contacts.Where(c => c.ContactBookId == contactBookId).ToList();

        }

        public Contact GetContact(int id)
        {
            return _context.Contacts.Find(id);

        }
        public Contact AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact;
        }

        public void DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }

        public void UpdateContact(Contact contact)
        {
            var contactItem = _context.Contacts.FirstOrDefault(c => c.Id == contact.Id);

            if (contactItem != null)
            {
                contactItem.Name = contact.Name;
                contactItem.LastName = contact.LastName;
                contactItem.PhoneNumber = contact.PhoneNumber;
                contactItem.Location = contact.Location;
                contactItem.Email = contact.Email;

                _context.SaveChanges();
            }

        }
    }
}
