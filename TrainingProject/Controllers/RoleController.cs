using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainingProject.Models;
using TrainingProject.ViewModels;

namespace TrainingProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private ApplicationUserManager _userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager _roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        public ActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(IdentityRole role)
        {
            if (role != null)
            {
                await _roleManager.CreateAsync(role);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string roleId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
               await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public ActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<ActionResult> Edit(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user.Id);
                List<IdentityRole> allRoles = _roleManager.Roles.ToList();

                List<CheckBoxListItem> checkBoxListItems = new List<CheckBoxListItem>();
                foreach (var role in allRoles)
                {
                    checkBoxListItems.Add(new CheckBoxListItem()
                    {
                        Id = role.Name,
                        isChecked = _userManager.IsInRole(user.Id, role.Name)
                    });
                }

                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    Roles = checkBoxListItems,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string userId, List<CheckBoxListItem> roles)
        {
            List<string> userSelesctedRoles = new List<string>();

            foreach (CheckBoxListItem checkBoxListItem in roles)
            {
                if (checkBoxListItem.isChecked)
                {
                    userSelesctedRoles.Add(checkBoxListItem.Id);
                }
            }

            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (userSelesctedRoles.Count != 0)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user.Id);
                List<IdentityRole> allRoles = _roleManager.Roles.ToList();

                if (roles != null)
                {
                    string[] addedRoles = userSelesctedRoles.Except(userRoles).ToArray();

                    string[] removedRoles = userRoles.Except(userSelesctedRoles).ToArray();

                    await _userManager.AddToRolesAsync(user.Id, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user.Id, removedRoles);
                }

                return RedirectToAction("UserList");
            }

            return RedirectToAction("Index");
        }
    }
}