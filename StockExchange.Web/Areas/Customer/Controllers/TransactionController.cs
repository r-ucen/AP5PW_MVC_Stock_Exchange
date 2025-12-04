using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Identity.Enums;
using System.Security.Claims;

namespace StockExchange.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class TransactionController : Controller
    {
        ITransactionAppService _transactionAppService;
        public TransactionController(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }

        public async Task<IActionResult> Select()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioHoldingViewModel());
            }

            var data = await _transactionAppService.SelectByUserIdAsync(uId);

            return View(data);
        }
    }
}
