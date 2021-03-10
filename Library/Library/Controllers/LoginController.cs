using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using Library.Extensions;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        /*private IAccountRepository repository;
        public LoginController(IAccountRepository repositoryParam)
        {
            repository = repositoryParam;
        }*/
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        private IAuthenticationManager AuthManager;
        public LoginController(IAuthenticationManager authenticationManager)
        {
            AuthManager = authenticationManager;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginData)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(loginData.Login, loginData.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid login or password.");
                }
                else
                {
                    ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, identity);
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully logged in.</strong>", ClassName = "alertMessage successful" };
                    return RedirectToAction("List", "Book");
                }
                //Account validUser = repository.ValidAccount(loginData.Login, loginData.Password.hashSHA256());
                /*if(validUser != null)
                {
                    FormsAuthentication.SetAuthCookie(loginData.Login, false);
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully logged in.</strong>", ClassName = "alertMessage successful" };
                    return RedirectToAction("List", "Book");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Incorrect login or password!");
                    return View();
                }*/
            }
            return View();
        }

        public ActionResult Logout()
        {
            /*FormsAuthentication.SignOut();
            Session.Remove("username");*/
            AuthManager.SignOut();
            TempData["Message"] = new Message() { Text = "<strong>You have been logged out.</strong>", ClassName = "alertMessage info" };
            return RedirectToAction("List", "Book");
        }
    }
}