using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using Library.Extensions;
using System.Web.Mvc;
using System.Web.Security;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        private IAccountRepository repository;
        public LoginController(IAccountRepository repositoryParam)
        {
            repository = repositoryParam;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginData)
        {
            if (ModelState.IsValid)
            {
                Account validUser = repository.ValidAccount(loginData.Login, loginData.Password.hashSHA256());
                if(validUser != null)
                {
                    FormsAuthentication.SetAuthCookie(loginData.Login, false);
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully logged in.</strong>", ClassName = "alertMessage successful" };
                    return RedirectToAction("List", "Book");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Incorrect login or password!");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("username");
            TempData["Message"] = new Message() { Text = "<strong>You have been logged out.</strong>", ClassName = "alertMessage info" };
            return RedirectToAction("List", "Book");
        }
    }
}