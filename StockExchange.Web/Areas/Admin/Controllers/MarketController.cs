using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
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
    }
}
