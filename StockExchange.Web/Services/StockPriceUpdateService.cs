using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Database;
using StockExchange.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchange.Web.Services
{
    public class StockPriceUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StockPriceUpdateService> _logger;
        private static readonly Random _random = new();

        public StockPriceUpdateService(IServiceProvider serviceProvider, ILogger<StockPriceUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWorkAsync(stoppingToken);

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWorkAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StockExchangeDbContext>();
            var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<StockHub>>();
            var portfolioAppService = scope.ServiceProvider.GetRequiredService<IPortfolioAppService>();

            var stocks = await dbContext.Stocks.ToListAsync(stoppingToken);
            if (stocks.Count == 0)
                return;

            var sendToUsers = new HashSet<int>();

            foreach (var stock in stocks)
            {
                var priceChangeRate = (decimal)((_random.NextDouble() - 0.5) * 0.001); // -0.05% to 0.05%
                var newStockPrice = stock.CurrentPrice + (stock.CurrentPrice * priceChangeRate);
                stock.CurrentPrice = Math.Round(newStockPrice, 2);
            }

            await dbContext.SaveChangesAsync(stoppingToken);

            foreach (var stock in stocks)
            {
                await hubContext.Clients.All.SendAsync(
                    "ReceiveStockPriceUpdate",
                    stock.TickerSymbol,
                    stock.CurrentPrice,
                    cancellationToken: stoppingToken);
            }

            var holders = await (from ps in dbContext.PortfolioStocks.AsNoTracking()
                                 join p in dbContext.Portfolios.AsNoTracking() on ps.PortfolioId equals p.Id
                                 select p.UserId)
                                .Distinct()
                                .ToListAsync(stoppingToken);

            foreach (var holderId in holders)
            {
                sendToUsers.Add(holderId);
            }

            foreach (var userId in sendToUsers)
            {
                var summary = await portfolioAppService.GetSummaryAsync(userId);
                if (summary != null)
                {
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
                            },
                            cancellationToken: stoppingToken);
                }
            }

            foreach (var userId in sendToUsers)
            {
                var holdings = await portfolioAppService.GetPortfolioHoldingsAsync(userId);
                await hubContext.Clients.User(userId.ToString())
                    .SendAsync("ReceivePortfolioHoldingsUpdate",
                        holdings, cancellationToken: stoppingToken);
            }
        }
    }
}