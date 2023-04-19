using ContactManagerApi.Data;
using ContactManagerApi.Entities;
using ContactManagerApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApi.Services
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DataContext _context;

        public EmailRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Emails.AnyAsync(e => e.EmailAddress == email);
        }

        public async Task<Email> GetEmail(string email)
        {
            return await _context.Emails.FirstOrDefaultAsync(e => e.EmailAddress == email);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
