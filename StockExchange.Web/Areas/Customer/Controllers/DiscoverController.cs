using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Identity.Enums;

namespace StockExchange.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class DiscoverController : Controller
    {
        IStockAppService _stockAppService;

        public DiscoverController(IStockAppService stockAppService)
        {
            _stockAppService = stockAppService;
        }

        public async Task<IActionResult> Select()
        {

            IList<StockMarketViewModel> stocks = await _stockAppService.GetStockMarketViewModels();

            return View(stocks);
        }
    }
}
