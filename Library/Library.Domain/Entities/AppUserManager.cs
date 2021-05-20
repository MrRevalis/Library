using Library.Domain.Concrete;
using Library.Domain.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store) { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            EFDbContext db = context.Get<EFDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

            manager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            const string name = "admin";
            const string password = "zaq1@WSXc";
            const string email = "admin@gmail.com";
            const string role = "Admin";

            AppUser user = manager.FindByName(name);
            if(user == null)
            {
                user = new AppUser { UserName = name, Email = email };
                manager.Create(user, password);
            }
            var userRole = manager.GetRoles(user.Id);
            if (!userRole.Contains(role))
            {
                manager.AddToRole(user.Id, role);
            }

            return manager;
        }
    }
}
