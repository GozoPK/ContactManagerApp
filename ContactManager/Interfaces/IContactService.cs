using ContactManager.Models;

namespace ContactManager.Interfaces
{
    public interface IContactService
    {
        public Task<IEnumerable<Contact>> GetContacts(string token);
        public Task<Contact> GetContact(int id, string token);
        public Task<Contact> CreateContact(Contact model, string token);
        public Task UpdateContact(int id, UpdateContactModel model, string token);
        public Task DeleteContact(int id, string token);
    }
}
