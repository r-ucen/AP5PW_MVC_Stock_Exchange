using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using System;

namespace StockExchange.Web.Controllers
{
    public class SettingsController : Controller
    {

        IAccountService _accountService;

        public SettingsController(IAccountService accountService)
        {
            _accountService = accountService;
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
    }
}
