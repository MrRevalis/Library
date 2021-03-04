using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class AccountModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Please enter a username.")]
        [MaxLength(32)]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(320)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [MinLength(4)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}