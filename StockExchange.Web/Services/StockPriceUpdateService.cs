
using Microsoft.AspNetCore.SignalR;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Database;
using StockExchange.Web.Hubs;

namespace StockExchange.Web.Services
{
    public class StockPriceUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly Random _random = new();

        public StockPriceUpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<StockExchangeDbContext>();
                    var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<StockHub>>();
                    var portfolioAppService = scope.ServiceProvider.GetRequiredService<IPortfolioAppService>();

                    var stocks = dbContext.Stocks.ToList();

                    // send portfolio update only to these users
                    var sendToUsers = new HashSet<int>();

                    foreach (var stock in stocks)
                    {
                        var stockToUpdate = dbContext.Stocks.FirstOrDefault(s => s.Id == stock.Id);
                        if (stockToUpdate != null)
                        {
                            var priceChangeRate = (decimal)((_random.NextDouble() - 0.5) * 0.01); // -0.5 % to 0.5 % 
                            var newStockPrice = stockToUpdate.CurrentPrice + (stockToUpdate.CurrentPrice * priceChangeRate);
                            stockToUpdate.CurrentPrice = Math.Round(newStockPrice, 2);

                            dbContext.SaveChanges();

                            await hubContext.Clients.All.SendAsync("ReceiveStockPriceUpdate",
                                    stockToUpdate.TickerSymbol,
                                    stockToUpdate.CurrentPrice,
                                    stoppingToken
                                );
                        }

                        /*
                        
                        SELECT distinct(UserId) FROM portfoliostock
                        INNER JOIN portfolio
                        ON portfoliostock.PortfolioId = portfolio.Id

                        */

                        var holders = (from ps in dbContext.PortfolioStocks
                                       join p in dbContext.Portfolios
                                       on ps.PortfolioId equals p.Id
                                       select p.UserId).Distinct().ToList();

                        foreach (var holderId in holders)
                        {
                            sendToUsers.Add(holderId);
                        }
                    }

                    foreach (var userId in sendToUsers)
                    {
                        var summary = await portfolioAppService.GetSummaryAsync(userId);
                        await hubContext.Clients.User(userId.ToString())
                            .SendAsync("ReceivePortfolioSummaryUpdate",
                            new PortfolioSummaryViewModel
                            {
                                Title = summary.Title,
                                PortfolioValue = summary.PortfolioValue,
                                UnrealizedGains = summary.UnrealizedGains,
                                UnrealizedGainPercentage = summary.UnrealizedGainPercentage,
                                AvailableCash = summary.AvailableCash,
                                Deposits = summary.Deposits
                            }, stoppingToken);
                    }

                    foreach (var userId in sendToUsers)
                    {
                        var holdings = await portfolioAppService.GetPortfolioHoldingsAsync(userId);
                        await hubContext.Clients.User(userId.ToString())
                            .SendAsync("ReceivePortfolioHoldingsUpdate",
                            holdings, stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
            }
        }
    }
}
