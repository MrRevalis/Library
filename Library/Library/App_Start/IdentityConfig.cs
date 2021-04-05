using Library.Domain.Concrete;
using Library.Domain.Entities;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Extensions.DependencyInjection;

namespace Library
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<EFDbContext>(EFDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),
                LogoutPath = new PathString("/Login/Logout")
            });
        }
    }
}