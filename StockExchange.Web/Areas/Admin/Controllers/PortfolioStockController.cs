using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Identity.Enums;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class PortfolioStockController : Controller
    {
        IPortfolioStockAppService _portStockfolioAppService;

        public PortfolioStockController(IPortfolioStockAppService portStockfolioAppService)
        {
            _portStockfolioAppService = portStockfolioAppService;
        }

        public IActionResult Select()
        {
            IList<PortfolioStock> portfolioStocks = _portStockfolioAppService.Select();
            return View(portfolioStocks);
        }
    }
}
