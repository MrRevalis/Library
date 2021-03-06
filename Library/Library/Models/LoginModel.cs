using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [MaxLength(32)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}