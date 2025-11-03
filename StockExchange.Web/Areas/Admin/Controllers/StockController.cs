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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var stock = _stockAppService.Select().FirstOrDefault(s => s.Id == id);
            if (stock != null)
            {
                return View(stock);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _stockAppService.Delete(id);
            return RedirectToAction(nameof(StockController.Select));
        }
    }
}
