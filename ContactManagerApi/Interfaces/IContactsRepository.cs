using ContactManagerApi.Entities;

namespace ContactManagerApi.Interfaces
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
        Task<bool> PhoneNumberExists(string phoneNumber);
        Task AddContact(Contact contact);
        Task DeleteContact(int id);
        bool HasChanges();
        Task<bool> SaveAllAsync();
    }
}
