using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using System.Security.Claims;

namespace StockExchange.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class DepositWithdrawController : Controller
    {
        IPortfolioAppService _portfolioAppService;

        public DepositWithdrawController(IPortfolioAppService portfolioAppService)
        {
            _portfolioAppService = portfolioAppService;
        }

        public IActionResult Select()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost, ActionName("Deposit")]
        public async Task<IActionResult> DepositConfirm(decimal amount)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId, out var uId))
            {
                return View();
            }
            try
            {
                await _portfolioAppService.Deposit(amount, uId);
                return RedirectToAction("Select");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost, ActionName("Withdraw")]
        public async Task<IActionResult> WithDrawConfirm(decimal amount)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId, out var uId))
            {
                return View();
            }
            try
            {
                await _portfolioAppService.Withdraw(amount, uId);
                return RedirectToAction("Select");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
    }
}
