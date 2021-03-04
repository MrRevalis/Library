using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Extensions;

namespace Library.Controllers
{
    public class RegisterController : Controller
    {
        private IAccountRepository repository;

        public RegisterController(IAccountRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                Account newAccount = new Account()
                {
                    Username = account.Username,
                    Email = account.Email,
                    Password = account.Password.hashSHA256()
                };
                repository.CreateAccount(newAccount);
                return RedirectToAction("List", "Book");
            }

            return View();
        }
    }
}