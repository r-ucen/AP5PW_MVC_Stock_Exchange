using Microsoft.EntityFrameworkCore;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Implementation
{
    public class PortfolioAppService : IPortfolioAppService
    {
        StockExchangeDbContext _stockExchangeDbContext;
        public PortfolioAppService(StockExchangeDbContext stockExchangeDbContext)
        {
            _stockExchangeDbContext = stockExchangeDbContext;
        }

        public IList<Portfolio> Select()
        {
            return _stockExchangeDbContext.Portfolios.ToList();
        }

        public async Task<IList<PortfolioHoldingViewModel>> GetPortfolioHoldingsAsync(int userId)
        {
            // SELECT * FROM portfolio WHERE UserId = userId LIMIT 1;

            var portfolio = _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .SingleOrDefault();

            if (portfolio == null)
            {
                return [];
            }

            /*
            
            SELECT
	            stock.Id AS StockId,
                stock.TickerSymbol,
                stock.FullName,
                portfoliostock.Quantity,
                stock.CurrentPrice,
                portfoliostock.AvgPurchasePrice
            FROM
                portfoliostock
            INNER JOIN stock
                on portfoliostock.StockId = stock.Id
            WHERE portfoliostock.PortfolioId = portfolio.Id;

            */

            var holdings = await (from ps in _stockExchangeDbContext.PortfolioStocks.AsNoTracking()
                                  join s in _stockExchangeDbContext.Stocks.AsNoTracking() on ps.StockId equals s.Id
                                  where ps.PortfolioId == portfolio.Id
                                  select new PortfolioHoldingViewModel
                                  {
                                      StockId = s.Id,
                                      TickerSymbol = s.TickerSymbol,
                                      FullName = s.FullName,
                                      Quantity = ps.Quantity,
                                      CurrentPrice = s.CurrentPrice,
                                      AvgPurchasePrice = ps.AvgPurchasePrice
                                  }).ToListAsync();

            return holdings;
        }

        public async Task<PortfolioSummaryViewModel> GetSummaryAsync(int userId)
        {
            // SELECT * FROM portfolio WHERE UserId = userId LIMIT 1;

            var portfolio = _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .SingleOrDefault();

            if (portfolio == null)
            {
                return new PortfolioSummaryViewModel();
            }

            /*
            
            CREATE VIEW holdings AS
            SELECT
                portfoliostock.Quantity,
                portfoliostock.AvgPurchasePrice,
                stock.CurrentPrice
            FROM
                portfoliostock
            INNER JOIN stock
                on portfoliostock.StockId = stock.Id
            WHERE portfoliostock.PortfolioId = portfolio.Id;

            */

            var holdings = await (from ps in _stockExchangeDbContext.PortfolioStocks.AsNoTracking()
                                  join s in _stockExchangeDbContext.Stocks.AsNoTracking() on ps.StockId equals s.Id
                                  where ps.PortfolioId == portfolio.Id
                                  select new
                                  {
                                      ps.Quantity,
                                      ps.AvgPurchasePrice,
                                      s.CurrentPrice
                                  }).ToListAsync();
            /*
            
            CREATE VIEW costbasis AS
            SELECT SUM(Quantity * AvgPurchasePrice) AS CostBasis FROM holdings;
            
            */
            var costBasis = holdings.Sum(h => h.Quantity * h.AvgPurchasePrice);

            /*
            
            CREATE VIEW currentvalue AS
            SELECT SUM(Quantity * AvgPurchasePrice) AS CurrentValue FROM holdings;

            */
            var currentValue = holdings.Sum(h => h.Quantity * h.CurrentPrice);

            /*
            
            CREATE VIEW unrealized AS
            SELECT
                SUM(Quantity * CurrentPrice) - SUM(Quantity * AvgPurchasePrice) AS Unrealized
            FROM holdings;
            
            */
            var unrealized = currentValue - costBasis;
            var unrealizedPct = costBasis != 0 ? (unrealized / costBasis) : 0m;

            /*
             
            CREATE VIEW buys AS
            SELECT SUM(Price * Quantity) FROM transaction WHERE UserId = userId AND TransactionType = "BUY";

            */
            var buys = await _stockExchangeDbContext.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.TransactionType == "BUY")
                .SumAsync(t => t.Price * t.Quantity);

            /*
            
            CREATE VIEW sells AS
            SELECT SUM(Price * Quantity) FROM transaction WHERE UserId = userId AND TransactionType = "SELL";

            */

            var sells = await _stockExchangeDbContext.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.TransactionType == "SELL")
                .SumAsync(t => t.Price * t.Quantity);

            var availableCash = portfolio.Deposits - buys + sells;

            return new PortfolioSummaryViewModel
            {
                PortfolioValue = currentValue,
                UnrealizedGains = unrealized,
                UnrealizedGainPercentage = unrealizedPct * 100,
                AvailableCash = availableCash,
                Deposits = portfolio.Deposits
            };
        }
    }
}
