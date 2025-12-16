using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using System.Security.Claims;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class UsersController : Controller
    {
        IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Select()
        {
            var users = _accountService.GetAllUsers().Result;
            return View(users);
        }


        public IActionResult Delete(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            var result = _accountService.DeleteUser(id).Result;
            if (result)
                return RedirectToAction("Select");
            else
                return BadRequest();
        }

        // Disable User
        public IActionResult Disable(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            return View(user);
        }

        [HttpPost, ActionName("Disable")]
        public IActionResult DisableConfirmed(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            var result = _accountService.DisableUser(id).Result;
            if (result)
                return RedirectToAction("Select");
            else
                return BadRequest();
        }

        // Enable User
        public IActionResult Enable(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            return View(user);
        }

        [HttpPost, ActionName("Enable")]
        public IActionResult EnableConfirmed(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            var result = _accountService.EnableUser(id).Result;
            if (result)
                return RedirectToAction("Select");
            else
                return BadRequest();
        }

        public IActionResult EditRoles(int id)
        {
            if (!User.IsInRole(nameof(Roles.Admin)))
            {
                return RedirectToAction("Select");
            }

            var user = _accountService.GetUserById(id).Result;

            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            var viewModel = _accountService.GetUserRolesForEdit(id).Result;
            if (viewModel == null)
            {
                return NotFound();
            }

            viewModel.AvailableRoles = viewModel.AvailableRoles
                .Where(r => r.RoleName == nameof(Roles.Manager))
                .ToList();

            return View(viewModel);
        }

        [HttpPost, ActionName("EditRoles")]
        public IActionResult EditRolesConfirmed(int id, EditUserRolesViewModel model)
        {
            if (!User.IsInRole(nameof(Roles.Admin)))
            {
                return RedirectToAction("Select");
            }

            var user = _accountService.GetUserById(id).Result;
            if (user == null) { 
                return NotFound();
            }
            
            if (!CanModifyUser(user))
            {
                return RedirectToAction("Select");
            }

            var selectedRoles = model.AvailableRoles
                .Where(r => r.IsSelected)
                .Select(r => r.RoleName)
                .ToList();

            if (!selectedRoles.Contains(nameof(Roles.Customer)))
            {
                selectedRoles.Add(nameof(Roles.Customer));
            }

            var result = _accountService.UpdateUserRoles(id, selectedRoles).Result;
            if (result)
                return RedirectToAction("Select");
            else
                return BadRequest();
        }

        private bool CanModifyUser(UserViewModel user)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInAsAdmin = User.IsInRole(nameof(Roles.Admin));
            var loggedInAsManager = User.IsInRole(nameof(Roles.Manager));

            var userIsCurrentUser = currentUserId == user.Id.ToString();
            var userIsAdmin = user.Roles.Contains(nameof(Roles.Admin));
            var userIsManager = user.Roles.Contains(nameof(Roles.Manager));

            if (userIsCurrentUser)
            {
                return false;
            }

            if (loggedInAsAdmin)
            {
                return !userIsAdmin;
            }

            if (loggedInAsManager)
            {
                return !userIsAdmin && !userIsManager;
            }

            return true;
        }
    }
}
