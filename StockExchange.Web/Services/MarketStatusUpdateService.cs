using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database;

namespace StockExchange.Web.Services;

public class MarketStatusUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MarketStatusUpdateService> _logger;

    public MarketStatusUpdateService(
        IServiceProvider serviceProvider,
        ILogger<MarketStatusUpdateService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await DoWorkAsync(stoppingToken);

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWorkAsync(stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("MarketStatusUpdateService is stopping due to cancellation.");
        }
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StockExchangeDbContext>();

            var markets = await dbContext.Markets
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var hasChanges = false;

            foreach (var market in markets)
            {
                var newStatus = CalculateMarketStatus(market);

                if (market.IsCurrentlyOpen != newStatus)
                {
                    var tracked = await dbContext.Markets.FindAsync(new object[] { market.Id }, cancellationToken);
                    if (tracked != null)
                    {
                        tracked.IsCurrentlyOpen = newStatus;
                        hasChanges = true;
                    }
                }
            }

            if (hasChanges)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Market statuses updated.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MarketStatusUpdateService.DoWorkAsync");
        }
    }

    private static bool CalculateMarketStatus(Market market)
    {
        try
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(market.TimeZoneId);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

            if (localTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                return false;

            return localTime.TimeOfDay >= market.OpenTime &&
                   localTime.TimeOfDay <= market.CloseTime;
        }
        catch
        {
            return false;
        }
    }
}