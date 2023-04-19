using ContactManagerApi.Entities;

namespace ContactManagerApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username);
    }
}
