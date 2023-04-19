using ContactManagerApi.Data;
using ContactManagerApi.Entities;
using ContactManagerApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        }
    }
}
