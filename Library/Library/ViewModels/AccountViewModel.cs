using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class AccountViewModel
    {

        public AccountViewModel()
        {
            Roles = new List<string>();
        }

        public string ID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}