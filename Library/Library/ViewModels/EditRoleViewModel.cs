using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class EditRoleViewModel
    {

        public EditRoleViewModel()
        {
            Members = Enumerable.Empty<AppUser>();
        }

        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
    }
}