using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;

namespace StockExchange.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
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
