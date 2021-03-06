﻿using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Library.Controllers
{
    public class RegisterController : Controller
    {
        /*private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }*/

        private AppUserManager UserManager;
        private IAuthenticationManager AuthManager;
        public RegisterController(AppUserManager userManager, IAuthenticationManager authManager)
        {
            UserManager = userManager;
            AuthManager = authManager;
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = account.Username, Email = account.Email };
                IdentityResult result = await UserManager.CreateAsync(user, account.Password);

                if (result.Succeeded)
                {
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have registered successfully.</strong>", ClassName = "alertMessage successful" };
                    return RedirectToAction("List", "Book");
                }
                else
                {
                    AddErrorsFromResult(result);
                }

            }
            return View(account);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> EmailExists(string email)
        {
            AppUser user = await UserManager.FindByEmailAsync(email);
            return user == null ? Json(true) : Json($"Email {email} is already in use.");
            //return repository.EmailExists(email) ? Json(true): Json($"Email {email} is already in use.");
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> UserExists(string username)
        {
            AppUser user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string errorMessage = $"'{username}' is not available.";
                return Json(errorMessage, JsonRequestBehavior.AllowGet);
            }
        }
    }
}