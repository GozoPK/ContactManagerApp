using ContactManager.Models;

namespace ContactManager.Interfaces
{
    public interface IAccountService
    {
        public Task<User> Login(LoginModel model);
    }
}
