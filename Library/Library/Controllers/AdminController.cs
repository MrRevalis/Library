﻿using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private AppUserManager UserManager;
        private IAuthenticationManager AuthManager;
        private IShelfRepository ShelfRepository;
        public AdminController(AppUserManager userManager, IAuthenticationManager authenticationManager, IShelfRepository repository)
        {
            UserManager = userManager;
            AuthManager = authenticationManager;
            ShelfRepository = repository;
        }

        public ActionResult Index()
        {
            IEnumerable<AppUser> users = UserManager.Users;
            return View(users);
        }

        public async Task<ActionResult> EditUser(string ID)
        {
            AppUser user = await UserManager.FindByIdAsync(ID);
            if(user == null)
            {
                ViewBag.Error = $"Error occurred while looking for a user with ID = {ID}";
                return View("Error");
            }

            AccountViewModel accountView = new AccountViewModel()
            {
                ID = ID,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await UserManager.GetRolesAsync(ID)
            };

            return View(accountView);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string ID)
        {
            AppUser user = await UserManager.FindByIdAsync(ID);
            if (user == null)
            {
                ViewBag.Error = $"Error occurred while looking for a user with ID = {ID}";
                return View("Error");
            }

            TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully deleted user.</strong>", ClassName = "alertMessage successful" };

            //First delete book from shelf
            ShelfRepository.RemoveBooksFromUser(ID);

            if (user.UserName == User.Identity.Name)
            {
                AuthManager.SignOut();
                await UserManager.DeleteAsync(user);
                return RedirectToAction("List", "Book");
            }

            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByIdAsync(account.ID);
                if (user == null)
                {
                    ViewBag.Error = $"Error occurred while looking for a user with ID = {account.ID}";
                    return View("Error");
                }
                else
                {
                    user.Email = account.Email;
                    user.UserName = account.UserName;

                    IdentityResult results = await UserManager.UpdateAsync(user);

                    if (results.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach(var error in results.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    return View(account);
                }
            }
            else
                return View(account);
        }

        public async Task<ActionResult> Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}