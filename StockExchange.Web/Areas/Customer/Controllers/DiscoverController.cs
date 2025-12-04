using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
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

        public IActionResult Select()
        {

            IList<Stock> stocks = _stockAppService.Select();

            return View(stocks);
        }
    }
}
