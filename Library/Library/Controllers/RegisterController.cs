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
                TempData["Message"] = new Message() { Text = "Success! <strong>You have registered successfully.</strong>", ClassName = "alertMessage successful" };
                return RedirectToAction("List", "Book");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult EmailExists(string email)
        {
            return repository.EmailExists(email) ? Json(true): Json($"Email {email} is already in use.");
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult UserExists(string username)
        {
            if (repository.UsernameExists(username))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string errorMessage = $"{username} is not available.";
                for (int i = 0; i < 10; i++)
                {
                    string alternativeUsername = username + i.ToString();
                    if (repository.UsernameExists(alternativeUsername))
                    {
                        errorMessage += $" Try '{alternativeUsername}'.";
                        break;
                    }
                }
                return Json(errorMessage, JsonRequestBehavior.AllowGet);
            }

        }
    }
}