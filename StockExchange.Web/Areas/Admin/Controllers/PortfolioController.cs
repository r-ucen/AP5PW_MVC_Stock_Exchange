using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Identity.Enums;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class PortfolioController : Controller
    {
        IPortfolioAppService _portfolioAppService;

        public PortfolioController(IPortfolioAppService portfolioAppService)
        {
            _portfolioAppService = portfolioAppService;
        }

        public IActionResult Select()
        {
            IList<Portfolio> portfolios = _portfolioAppService.Select();
            return View(portfolios);
        }
    }
}
