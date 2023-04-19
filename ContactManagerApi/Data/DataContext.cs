using ContactManagerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(p => p.Username)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasMany(p => p.Emails)
                .WithOne(p => p.Contact)
                .HasForeignKey(p => p.ContactId)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(p => p.DateOfBirth)
                .HasColumnType("date");

            modelBuilder.Entity<Contact>()
                .HasIndex(p => p.MobileNumber)
                .IsUnique();

            modelBuilder.Entity<Email>()
                .HasIndex(p => p.EmailAddress)
                .IsUnique();

        }
    }
}
