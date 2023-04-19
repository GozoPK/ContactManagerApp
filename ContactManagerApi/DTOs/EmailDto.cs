using System.ComponentModel.DataAnnotations;

namespace ContactManagerApi.DTOs
{
    public class EmailDto
    {
        [EmailAddress(ErrorMessage = "This is not a valid e-mail address")]
        [Required]
        public string EmailAddress { get; set; }
    }
}
