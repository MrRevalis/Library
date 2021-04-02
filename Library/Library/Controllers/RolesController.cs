using Library.Domain.Entities;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class RolesController : Controller
    {

        private AppRoleManager RoleManager;

        public RolesController(AppRoleManager manager)
        {
            RoleManager = manager;
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole identityRole = new AppRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await RoleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully added new role.</strong>", ClassName = "alertMessage successful" };
                    return RedirectToAction("List", "Book");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}