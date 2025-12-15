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
        private static readonly string[] ProtectedRoles = [nameof(Roles.Admin), nameof(Roles.Manager)];

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

        private bool CanModifyUser(UserViewModel user)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(currentUserId, out var currentId) && currentId == user.Id)
                return false;

            if (user.Roles.Any(r => ProtectedRoles.Contains(r)))
                return false;

            return true;
        }
    }
}
