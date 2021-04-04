using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class UserInRoleViewModel
    {
        public UserInRoleViewModel()
        {
            Users = new List<string>();
            IsInRole = new List<bool>();
        }

        public string RoleName { get; set; }
        public string RoleID { get; set; }
        public List<string> Users { get; set; }
        public List<bool> IsInRole { get; set; }
    }
}