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
    public class TradeController : Controller
    {
        IPortfolioAppService _portfolioAppService;
        IStockAppService _stockAppService;


        public TradeController(IPortfolioAppService portfolioAppService, IStockAppService stockAppService)
        {
            _portfolioAppService = portfolioAppService;
            _stockAppService = stockAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Buy(int stockId)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioHoldingViewModel());
            }

            var data = await _portfolioAppService.GetBuySellTradeDataAsync(uId, stockId);

            return View(data);
        }

        [HttpPost, ActionName("Buy")]
        public async Task<IActionResult> BuyConfirmed(TradeViewModel viewModel)
        {

            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioHoldingViewModel());
            }

            if (!ModelState.IsValid)
            {
                var data = await _portfolioAppService.GetBuySellTradeDataAsync(uId, viewModel.StockId);
                data.Quantity = viewModel.Quantity;
                return View(data);
            }

            try
            {
                await _portfolioAppService.BuyStockAsync(uId, viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var data = await _portfolioAppService.GetBuySellTradeDataAsync(uId, viewModel.StockId);
                data.Quantity = viewModel.Quantity;
                return View(data);
            }
            return RedirectToAction("Select", "Portfolio");
        }

        [HttpGet]
        public async Task<IActionResult> Sell(int stockId)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioHoldingViewModel());
            }
            var data = await _portfolioAppService.GetBuySellTradeDataAsync(uId, stockId);
            return View(data);
        }

        [HttpPost, ActionName("Sell")]
        public async Task<IActionResult> SellConfirmed(TradeViewModel viewModel)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioHoldingViewModel());
            }
            if (!ModelState.IsValid)
            {
                var data = await _portfolioAppService.GetBuySellTradeDataAsync(uId, viewModel.StockId);
                data.Quantity = viewModel.Quantity;
                return View(data);
            }
            try
            {
                await _portfolioAppService.SellStockAsync(uId, viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var data = await _portfolioAppService.GetBuySellTradeDataAsync(uId, viewModel.StockId);
                data.Quantity = viewModel.Quantity;
                return View(data);
            }
            return RedirectToAction("Select", "Portfolio");
        }
    }
}
