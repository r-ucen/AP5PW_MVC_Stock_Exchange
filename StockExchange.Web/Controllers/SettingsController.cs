using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities.Interfaces;
using StockExchange.Infrastructure.Identity;
using StockExchange.Infrastructure.Identity.Enums;
using System;

namespace StockExchange.Web.Controllers
{
    public class SettingsController : Controller
    {

        IAccountService _accountService;

        UserManager<User> _userManager;
        SignInManager<User> _signInManager;


        public SettingsController(IAccountService accountService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Select()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                   var vm = _accountService.GetSelfUserPropertiesForEdit().Result;
                    return View(vm);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> UpdateProfile(EditSelfUserViewModel vm)
        {
            if (User.Identity.IsAuthenticated)
            {
                var success = await _accountService.UpdateSelfUserProperties(vm);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult DeleteAccount()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteAccount")]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            if (User.IsInRole(nameof(Roles.Admin)))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                return View();
            }

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
