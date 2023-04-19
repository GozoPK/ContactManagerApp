using ContactManagerApi.Data;
using ContactManagerApi.Entities;
using ContactManagerApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApi.Services
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly DataContext _context;

        public ContactsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _context.Contacts
                .Where(contact => contact.ContactId == id)
                .Include(contact => contact.Emails)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> PhoneNumberExists(string phoneNumber)
        {
            return await _context.Contacts.AnyAsync(contact => contact.MobileNumber == phoneNumber);
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _context.Contacts
                .Include(contact => contact.Emails)
                .ToListAsync();
        }

        public async Task AddContact(Contact contact)
        {
            await _context.AddAsync(contact);
        }

        public async Task DeleteContact(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(contact => contact.ContactId == id);

            if (contact == null) return;

            _context.Contacts.Remove(contact);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
