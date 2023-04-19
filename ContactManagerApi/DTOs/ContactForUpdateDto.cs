using System.ComponentModel.DataAnnotations;

namespace ContactManagerApi.DTOs
{
    public class ContactForUpdateDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Prefecture { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string MobileNumber { get; set; }
    }
}
