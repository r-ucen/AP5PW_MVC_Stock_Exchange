using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using System.Security.Claims;

namespace StockExchange.Web.ViewComponents
{
    public class PortfolioSummaryViewComponent : ViewComponent
    {
        private readonly IPortfolioAppService _portfolioAppService;
        
        public PortfolioSummaryViewComponent(IPortfolioAppService portfolioAppService)
        {
            _portfolioAppService = portfolioAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (!int.TryParse(userId, out var uId))
            {
                return View(new PortfolioSummaryViewModel());
            }

            var viewModel = await _portfolioAppService.GetSummaryAsync(uId);

            if (viewModel == null)
            {
                return View(new PortfolioSummaryViewModel());
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}
