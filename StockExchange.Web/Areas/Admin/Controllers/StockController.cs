using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {
        IStockAppService _stockAppService;

        public StockController(IStockAppService stockAppService)
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
