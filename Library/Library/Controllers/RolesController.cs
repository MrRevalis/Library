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
    [Authorize(Roles = "Admin")]
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

        public async Task<ActionResult> DeleteRole(string ID)
        {
            AppRole appRole = await RoleManager.FindByIdAsync(ID);
            if(appRole == null)
            {
                ViewBag.Error = $"Error occurred while deleting role with ID = {ID}";
                return View("Error");
            }
            else
            {
                IdentityResult identityResult = await RoleManager.DeleteAsync(appRole);
                if (identityResult.Succeeded)
                {
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully deleted a role.</strong>", ClassName = "alertMessage successful" };
                    return RedirectToAction("List");
                }
                foreach (var x in identityResult.Errors)
                {
                    ModelState.AddModelError("", x);
                }
                TempData["Message"] = new Message() { Text = "Error! <strong>There was a problem while deleting a role.</strong>", ClassName = "alertMessage error" };
                return View();
            }
        }

        public async Task<ActionResult> ManageRole(string roleID)
        {
            AppRole appRole = await RoleManager.FindByIdAsync(roleID);

            if(appRole == null)
            {
                ViewBag.Error = $"Error occurred while looking for role with ID = {roleID}";
                return View("Error");
            }

            UserInRoleViewModel userList = new UserInRoleViewModel();
            userList.RoleID = appRole.Id;
            userList.RoleName = appRole.Name;

            foreach(AppUser user in UserManager.Users)
            {
                userList.Users.Add(user.UserName);
                if(await UserManager.IsInRoleAsync(user.Id, appRole.Name))
                {
                    userList.IsInRole.Add(true);
                }
                else
                {
                    userList.IsInRole.Add(false);
                }
            }
            return View(userList);
        }

        [HttpPost]
        public async Task<ActionResult> ManageRole(UserInRoleViewModel userList)
        {
            AppRole appRole = await RoleManager.FindByIdAsync(userList.RoleID);

            if (appRole == null)
            {
                ViewBag.Error = $"Error occurred while looking for role with ID = {userList.RoleID ?? "NULL ID"}";
                return View("Error");
            }

            for (int i = 0; i < userList.IsInRole.Count; i++)
            {
                AppUser user = await UserManager.FindByNameAsync(userList.Users[i]);
                IdentityResult result;
                if (userList.IsInRole[i] && !(await UserManager.IsInRoleAsync(user.Id, userList.RoleName)))
                {
                    result = await UserManager.AddToRoleAsync(user.Id, userList.RoleName);

                }
                else if(!userList.IsInRole[i] && await UserManager.IsInRoleAsync(user.Id, userList.RoleName))
                {
                    result = await UserManager.RemoveFromRoleAsync(user.Id, userList.RoleName);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if(i < userList.IsInRole.Count)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { id = userList.RoleID });
                    }
                }
            }

            return RedirectToAction("EditRole", new { id = userList.RoleID });
        }
    }
}