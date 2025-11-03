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

        // Select
        public IActionResult Select()
        {
            IList<Stock> stocks = _stockAppService.Select();
            return View(stocks);
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Stock stock)
        {
            stock.CurrentPriceDateTime = DateTime.UtcNow;
            _stockAppService.Create(stock);
            return RedirectToAction(nameof(StockController.Select));
        }

        // Delete
        public IActionResult Delete(int id)
        {
            bool deleted = _stockAppService.Delete(id);
            if (deleted)
            {
                return RedirectToAction(nameof(StockController.Select));
            } else
            {
                return NotFound();
            }
        }
    }
}
