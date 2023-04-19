using ContactManagerApi.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManagerApi.DTOs
{
    public class ContactDto
    {
        public int ContactId { get; set; }

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

        [MustHaveOneElement(ErrorMessage = "Must provide at least one e-mail address")]
        public ICollection<EmailDto> Emails { get; set; } = new List<EmailDto>();

    }
}
