using System.ComponentModel.DataAnnotations;

namespace ContactManagerApi.Entities
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
        public Contact Contact { get; set; }
        public int ContactId { get; set; }
    }
}
