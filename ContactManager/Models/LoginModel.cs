using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
    }
}
