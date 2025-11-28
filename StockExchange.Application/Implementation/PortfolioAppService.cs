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

            var portfolio = await _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

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

            var portfolio = await _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

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

        public async Task<TradeViewModel> GetBuySellTradeDataAsync(int userId, int stockId)
        {

            var stock = await _stockExchangeDbContext.Stocks
                .Where(s => s.Id == stockId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            var portfolio = await _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (stock == null || portfolio == null)
            {
                return new TradeViewModel();
            }

            var quantityOwned = await _stockExchangeDbContext.PortfolioStocks
                .AsNoTracking()
                .Where(ps => ps.PortfolioId == portfolio.Id)
                .Where(ps => ps.StockId == stockId)
                .Select(ps => ps.Quantity)
                .FirstOrDefaultAsync();

            var buys = await _stockExchangeDbContext.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.TransactionType == "BUY")
                .SumAsync(t => t.Price * t.Quantity);

            var sells = await _stockExchangeDbContext.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.TransactionType == "SELL")
                .SumAsync(t => t.Price * t.Quantity);

            var availableCash = portfolio.Deposits - buys + sells;

            return new TradeViewModel
            {
                StockId = stock.Id,
                TickerSymbol = stock.TickerSymbol,
                FullName = stock.FullName,
                CurrentPrice = stock.CurrentPrice,
                AvailableCash = availableCash,
                QuantityOwned = quantityOwned,
                Quantity = 1
            };
        }

        public async Task BuyStockAsync(int userId, TradeViewModel viewModel)
        {
            /*
            DayOfWeek day = DateTime.Now.DayOfWeek;
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                throw new InvalidOperationException("Market is closed.");
            }
            */

            await using var dbTransaction = await _stockExchangeDbContext.Database.BeginTransactionAsync();

            try
            {
                var tradeData = await GetBuySellTradeDataAsync(userId, viewModel.StockId);
                var totalCost = tradeData.CurrentPrice * viewModel.Quantity;

                if (tradeData.AvailableCash < totalCost)
                {
                    throw new Exception("Insufficient funds to complete the purchase.");
                }

                var order = new Order
                {
                    UserId = userId,
                    StockId = viewModel.StockId,
                    OrderType = "BUY",
                    Quantity = viewModel.Quantity,
                    CreatedDate = DateTime.UtcNow
                };

                await _stockExchangeDbContext.Orders.AddAsync(order);
                await _stockExchangeDbContext.SaveChangesAsync();

                var userTransaction = new Transaction
                {
                    UserId = userId,
                    StockId = viewModel.StockId,
                    OrderId = order.Id,
                    TransactionType = "BUY",
                    Quantity = viewModel.Quantity,
                    Price = tradeData.CurrentPrice,
                    TransactionDate = DateTime.UtcNow
                };

                await _stockExchangeDbContext.Transactions.AddAsync(userTransaction);

                // find out whether the user is buying more of a stock or buying it for the first time
                var portfolio = await _stockExchangeDbContext.Portfolios
                    .Where(p => p.UserId == userId)
                    .SingleOrDefaultAsync();

                if (portfolio == null) throw new InvalidOperationException("Portfolio not found.");

                var portfolioStock = await _stockExchangeDbContext.PortfolioStocks
                    .Where(
                        ps => ps.PortfolioId == portfolio.Id
                        && ps.StockId == viewModel.StockId
                    )
                    .SingleOrDefaultAsync();

                if (portfolioStock != null)
                {
                    var totalQuantity = portfolioStock.Quantity + viewModel.Quantity;

                    var oldTotalValue = portfolioStock.Quantity * portfolioStock.AvgPurchasePrice;
                    var boughtValue = viewModel.Quantity * tradeData.CurrentPrice;

                    portfolioStock.AvgPurchasePrice = (oldTotalValue + boughtValue) / totalQuantity;
                    portfolioStock.Quantity = totalQuantity;
                }
                else
                {
                    portfolioStock = new PortfolioStock
                    {
                        PortfolioId = portfolio.Id,
                        StockId = viewModel.StockId,
                        Quantity = viewModel.Quantity,
                        AvgPurchasePrice = tradeData.CurrentPrice
                    };

                    await _stockExchangeDbContext.PortfolioStocks.AddAsync(portfolioStock);
                }


                await _stockExchangeDbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();

            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task SellStockAsync(int userId, TradeViewModel viewModel)
        {
            /*
            DayOfWeek day = DateTime.Now.DayOfWeek;
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                throw new InvalidOperationException("Market is closed.");
            }
            */

            await using var dbTransaction = await _stockExchangeDbContext.Database.BeginTransactionAsync();

            try
            {
                var tradeData = await GetBuySellTradeDataAsync(userId, viewModel.StockId);

                var order = new Order
                {
                    UserId = userId,
                    StockId = viewModel.StockId,
                    OrderType = "SELL",
                    Quantity = viewModel.Quantity,
                    CreatedDate = DateTime.UtcNow
                };

                await _stockExchangeDbContext.Orders.AddAsync(order);
                await _stockExchangeDbContext.SaveChangesAsync();

                var userTransaction = new Transaction
                {
                    UserId = userId,
                    StockId = viewModel.StockId,
                    OrderId = order.Id,
                    TransactionType = "SELL",
                    Quantity = viewModel.Quantity,
                    Price = tradeData.CurrentPrice,
                    TransactionDate = DateTime.UtcNow
                };

                await _stockExchangeDbContext.Transactions.AddAsync(userTransaction);

                var portfolio = await _stockExchangeDbContext.Portfolios
                    .Where(p => p.UserId == userId)
                    .SingleOrDefaultAsync();

                if (portfolio == null) throw new InvalidOperationException("Portfolio not found.");

                var portfolioStock = await _stockExchangeDbContext.PortfolioStocks
                    .Where(
                        ps => ps.PortfolioId == portfolio.Id
                        && ps.StockId == viewModel.StockId
                    )
                    .SingleOrDefaultAsync();

                if (portfolioStock != null)
                {
                    if (portfolioStock.Quantity < viewModel.Quantity)
                    {
                        throw new Exception("You don't own that many shares.");
                    }
                    else if (portfolioStock.Quantity == viewModel.Quantity)
                    {
                        _stockExchangeDbContext.PortfolioStocks.Remove(portfolioStock);
                    }
                    else
                    {
                        portfolioStock.Quantity -= viewModel.Quantity;
                    }
                }
                else
                {
                    await dbTransaction.RollbackAsync();
                    throw new Exception("Invalid stock.");
                }


                await _stockExchangeDbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();

            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task Deposit(decimal amount, int userId)
        {
            try
            {
                var portfolio = await _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .SingleOrDefaultAsync();

                if (portfolio == null)
                {
                    throw new Exception("Portfolio not found.");
                }

                if (amount < 1)
                {
                    throw new Exception("Deposit amount must be greater than 1.");
                }

                portfolio.Deposits += amount;
                _stockExchangeDbContext.Portfolios.Update(portfolio);
                await _stockExchangeDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Withdraw(decimal amount, int userId)
        {
            try
            {
                var portfolio = await _stockExchangeDbContext.Portfolios
                .Where(p => p.UserId == userId)
                .SingleOrDefaultAsync();

                var buys = await _stockExchangeDbContext.Transactions
                    .AsNoTracking()
                    .Where(t => t.UserId == userId && t.TransactionType == "BUY")
                    .SumAsync(t => t.Price * t.Quantity);

                var sells = await _stockExchangeDbContext.Transactions
                    .AsNoTracking()
                    .Where(t => t.UserId == userId && t.TransactionType == "SELL")
                    .SumAsync(t => t.Price * t.Quantity);

                var availableCash = portfolio.Deposits - buys + sells;

                if (portfolio == null)
                {
                    throw new Exception("Portfolio not found.");
                }

                if (amount <= 0)
                {
                    throw new Exception("Withdrawal amount must be greater than 0.");
                }

                if (amount > availableCash)
                {
                    throw new Exception("Not enought funds to withdraw");
                }

                portfolio.Deposits -= amount;
                _stockExchangeDbContext.Portfolios.Update(portfolio);
                await _stockExchangeDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
