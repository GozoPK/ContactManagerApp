using ContactManagerApi.Entities;

namespace ContactManagerApi.Interfaces
{
    public interface IEmailRepository
    {
        Task<Email> GetEmail(string email);
        Task<bool> EmailExists(string email);
        bool HasChanges();
        Task<bool> SaveAllAsync();
    }
}
