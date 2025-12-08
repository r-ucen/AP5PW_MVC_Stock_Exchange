using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using StockExchange.Web.Controllers;
using System.Security.Claims;

namespace StockExchange.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        IAccountService _accountService;
        IPortfolioAppService _portfolioAppService;
        public AccountController(IAccountService security, IPortfolioAppService portfolioAppService)
        {
            _accountService = security;
            _portfolioAppService = portfolioAppService;
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
                string[] errors = await _accountService.Register(registerVM, Roles.Customer);

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
                        var claimsPrincipal = User as ClaimsPrincipal;
                        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                        
                        if (int.TryParse(userId, out var uId))
                        {
                            await _portfolioAppService.CreatePortfolioAsync(uId);
                        }

                        return RedirectToAction("Select", "Portfolio", new { area = "Customer" });
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
                    return RedirectToAction("Select", "Portfolio", new { area = "Customer" });
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
