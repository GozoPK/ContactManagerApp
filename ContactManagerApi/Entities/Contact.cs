using ContactManagerApi.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ContactManagerApi.Entities
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Prefecture { get; set; }

        [StringLength(5)]
        public string PostalCode { get; set; }
        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }
        public ICollection<Email> Emails { get; set; } = new List<Email>();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Contact other = (Contact)obj;

            return FirstName == other.FirstName
                && LastName == other.LastName
                && City == other.City
                && Prefecture == other.Prefecture
                && PostalCode == other.PostalCode
                && DateOfBirth == other.DateOfBirth
                && MobileNumber == other.MobileNumber;
        }

        public override int GetHashCode()
        {
            return ContactId.GetHashCode();
        }
    }
}
