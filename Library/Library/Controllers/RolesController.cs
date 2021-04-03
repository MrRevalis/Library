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
        private AppUserManager UserManager;

        public RolesController(AppRoleManager manager, AppUserManager userManager)
        {
            RoleManager = manager;
            UserManager = userManager;
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
                    return RedirectToAction("List", "Roles");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var roles = RoleManager.Roles;
            return View(roles);
        }

        public async Task<ActionResult> EditRole(string id)
        {
            AppRole appRole = await RoleManager.FindByIdAsync(id);

            if(appRole == null)
            {
                ViewBag.Error = $"Error occurred while looking for role with ID = {id}";
                return View("Error");
            }
            else
            {
                string[] membersID = appRole.Users.Select(x => x.UserId).ToArray();
                IEnumerable<AppUser> members = UserManager.Users.Where(x => membersID.Any(y => y == x.Id));
                EditRoleViewModel editRoleViewModel = new EditRoleViewModel
                {
                    Role = appRole,
                    Members = members
                };
                return View(editRoleViewModel);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(EditRoleViewModel model)
        {
            AppRole appRole = await RoleManager.FindByIdAsync(model.Role.Id);

            if (appRole == null)
            {
                ViewBag.Error = $"Error occurred while looking for role with ID = {model.Role.Id}";
                return View("Error");
            }
            else
            {
                appRole.Name = model.Role.Name;
                IdentityResult result = await RoleManager.UpdateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }

                foreach(var x in result.Errors)
                {
                    ModelState.AddModelError("", x);
                }

                return View(model);
            }
        }
    }
}