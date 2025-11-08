
using Microsoft.AspNetCore.SignalR;
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

                    var stocks = dbContext.Stocks.ToList();

                    foreach (var stock in stocks)
                    {
                        var stockToUpdate = dbContext.Stocks.FirstOrDefault(s => s.Id == stock.Id);
                        if (stockToUpdate != null)
                        {
                            var priceChangeRate = (decimal)((_random.NextDouble() - 0.5) * 0.01); // -0.5 % to 0.5 % 
                            var newStockPrice = stockToUpdate.CurrentPrice + (stockToUpdate.CurrentPrice * priceChangeRate);
                            stockToUpdate.CurrentPrice = Math.Round(newStockPrice, 2);
                            stockToUpdate.CurrentPriceDateTime = DateTime.UtcNow;

                            dbContext.SaveChanges();

                            await hubContext.Clients.All.SendAsync("ReceiveStockPriceUpdate",
                                    stockToUpdate.TickerSymbol,
                                    stockToUpdate.CurrentPrice,
                                    stoppingToken
                                );
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
            }
        }
    }
}
