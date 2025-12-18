using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockExchange.Application.Abstraction;
using StockExchange.Application.Implementation;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Identity.Enums;
using System.Threading.Tasks;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class MarketController : Controller
    {
        IMarketService _marketService;

        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }

        public async Task<IActionResult> Select()
        {
            var markets = await _marketService.GetAllMarketsAsync();
            return View(markets);
        }

        public IActionResult Create()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id).ToList();
            ViewBag.TimeZones = new SelectList(timeZones);
            return View(new Market());
        }

        [HttpPost]
        public IActionResult Create(Market market)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _marketService.Create(market);
                    return RedirectToAction(nameof(MarketController.Select));
                }
                catch (TimeZoneNotFoundException)
                {
                    ModelState.AddModelError(nameof(market.TimeZoneId), "Selected time zone is not available on this server.");
                    return View(market);
                }
                catch (InvalidTimeZoneException)
                {
                    ModelState.AddModelError(nameof(market.TimeZoneId), "Selected time zone is invalid.");
                    return View(market);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(market);
                }
            }
            return View(market);
        }

        public IActionResult Delete(int id)
        {
            var market = _marketService.GetMarketById(id);
            if (market != null)
            {
                return View(market);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _marketService.Delete(id);
            return RedirectToAction(nameof(MarketController.Select));
        }

        // Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var market = _marketService.GetMarketById(id);
            if (market == null)
            {
                return NotFound();
            }

            var timeZones = TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id).ToList();
            ViewBag.TimeZones = new SelectList(timeZones);

            return View(market);
        }

        [HttpPost, ActionName("Update")]
        public IActionResult UpdateConfirmed(Market market)
        {
            if (ModelState.IsValid)
            {
                _marketService.Update(market);
                return RedirectToAction(nameof(MarketController.Select));
            }

            return View(market);
        }
    }
}
