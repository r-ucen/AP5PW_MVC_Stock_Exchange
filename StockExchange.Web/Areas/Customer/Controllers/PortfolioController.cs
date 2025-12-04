using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using System.Security.Claims;

namespace StockExchange.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class PortfolioController : Controller
    {
        IPortfolioAppService _portfolioAppService;

        public PortfolioController(IPortfolioAppService portfolioAppService)
        {
            _portfolioAppService = portfolioAppService;
        }

        public async Task<IActionResult> Select()
        {

            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioHoldingViewModel());
            }

            var viewModel = await _portfolioAppService.GetPortfolioHoldingsAsync(uId);

            if (viewModel == null)
            {
                return View(new PortfolioHoldingViewModel());
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}
