using ContactManagerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ContactManagerApi.Data
{
    public class Seed
    {
        public static async Task SeedDataBase(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = new List<User>()
            {
                new User()
                {
                    Username = "admin",
                    Role = "Admin"
                },
                new User()
                {
                    Username = "petros",
                    Role = "User"
                }
            };

            foreach (var user in userData)
            {
                using var hmac = new HMACSHA512();

                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));

                await context.Users.AddAsync(user);
            }

            var contacts = new List<Contact>()
            {
                new Contact()
                {
                    FirstName = "Γιώργος",
                    LastName = "Γεωργίου",
                    City = "Αθήνα",
                    Prefecture = "Αττική",
                    PostalCode = "11111",
                    DateOfBirth = DateTime.Parse("01/01/1990"),
                    MobileNumber = "6971234567"
                },
                new Contact()
                {
                    FirstName = "Ιωάννης",
                    LastName = "Ιωάννου",
                    City = "Τρίπολη",
                    Prefecture = "Αρκαδία",
                    PostalCode = "22222",
                    DateOfBirth = DateTime.Parse("01/01/1985"),
                    MobileNumber = "6941234567"
                },
                new Contact()
                {
                    FirstName = "Παναγιώτα",
                    LastName = "Παναγιώτου",
                    City = "Αθήνα",
                    Prefecture = "Αττική",
                    PostalCode = "11112",
                    DateOfBirth = DateTime.Parse("01/01/1994"),
                    MobileNumber = "6931234567"
                }
            };

            contacts[0].Emails = new List<Email>()
            {
                new Email() { EmailAddress = "georgiou@gmail.com"},
                new Email() { EmailAddress = "georgiou@yahoo.com"},
                new Email() { EmailAddress = "georgiou@hotmail.com"}
            };

            contacts[1].Emails = new List<Email>()
            {
                new Email() { EmailAddress = "ioannou@gmail.com"},
                new Email() { EmailAddress = "ioannou@hotmail.com"}
            };

            contacts[2].Emails = new List<Email>()
            {
                new Email() { EmailAddress = "panagiotou@gmail.com"}
            };

            await context.Users.AddRangeAsync(userData);
            await context.Contacts.AddRangeAsync(contacts);

            await context.SaveChangesAsync();
        }
    }
}
