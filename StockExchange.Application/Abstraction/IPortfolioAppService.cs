using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Abstraction
{
    public interface IPortfolioAppService
    {
        IList<Portfolio> Select();
        Task<IList<PortfolioHoldingViewModel>> GetPortfolioHoldingsAsync(int userId);
        Task<PortfolioSummaryViewModel> GetSummaryAsync(int userId);
        Task<TradeViewModel> GetBuySellTradeDataAsync(int userId, int stockId);
        Task BuyStockAsync(int userId, TradeViewModel viewModel);
        Task SellStockAsync(int userId, TradeViewModel viewModel);
        Task Deposit(decimal amount, int userId);
        Task Withdraw(decimal amount, int userId);
    }
}
