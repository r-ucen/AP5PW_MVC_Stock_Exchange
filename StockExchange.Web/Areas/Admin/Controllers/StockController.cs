using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Identity.Enums;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
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
        public IActionResult DeleteConfirmed(int id)
        {
            _stockAppService.Delete(id);
            return RedirectToAction(nameof(StockController.Select));
        }

        // Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var stock = _stockAppService.Select(id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        [HttpPost]
        public IActionResult Update(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _stockAppService.Update(stock);
                return RedirectToAction(nameof(StockController.Select));
            }

            return View(stock);
        }
    }
}
