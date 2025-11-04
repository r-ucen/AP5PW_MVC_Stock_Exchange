using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using StockExchange.Web.Controllers;

namespace StockExchange.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        IAccountService _accountService;
        public AccountController(IAccountService security)
        {
            _accountService = security;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                string[] errors = await _accountService.Register(registerVM, Roles.User);

                if (errors == null)
                {
                    LoginViewModel loginViewModel = new LoginViewModel()
                    {
                        Username = registerVM.Username,
                        Password = registerVM.Password
                    };

                    bool isLoggedIn = await _accountService.Login(loginViewModel);

                    if (isLoggedIn)
                    {
                        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });
                    }
                    else
                    {
                        return RedirectToAction(nameof(Login));
                    }
                }
            }

            return View(registerVM);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                bool isLoggedIn = await _accountService.Login(loginVM);
                if (isLoggedIn)
                {
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });
                }

                loginVM.LoginFailed = true;
            }
            return View(loginVM);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction(nameof(Login));
        }
    }
}
