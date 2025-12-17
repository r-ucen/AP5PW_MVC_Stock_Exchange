using Microsoft.EntityFrameworkCore;
using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Implementation
{
    public class MarketService : IMarketService
    {
        StockExchangeDbContext _stockExchangeDbContext;

        public MarketService(StockExchangeDbContext stockExchangeDbContext)
        {
            _stockExchangeDbContext = stockExchangeDbContext;
        }

        private bool CalculateIsOpen(Market market)
        {
            if (string.IsNullOrWhiteSpace(market.TimeZoneId))
                throw new InvalidOperationException("Market TimeZoneId is not set.");

            TimeZoneInfo.FindSystemTimeZoneById(market.TimeZoneId);

            var zone = TimeZoneInfo.FindSystemTimeZoneById(market.TimeZoneId);
            var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);

            if (time.DayOfWeek == DayOfWeek.Saturday ||
                time.DayOfWeek == DayOfWeek.Sunday)
                return false;

            var marketOpen = market.OpenTime;
            var marketClose = market.CloseTime;

            return time.TimeOfDay >= marketOpen &&
                   time.TimeOfDay <= marketClose;
        }

        public void Create(Market market)
        {
            market.IsCurrentlyOpen = CalculateIsOpen(market);
            _stockExchangeDbContext.Markets.Add(market);
            _stockExchangeDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var market = _stockExchangeDbContext.Markets.FirstOrDefault(m => m.Id == id);
            if (market != null)
            {
                _stockExchangeDbContext.Markets.Remove(market);
                _stockExchangeDbContext.SaveChanges();
            }
        }

        public async Task<IList<Market>> GetAllMarketsAsync()
        {
            return await _stockExchangeDbContext.Markets.ToListAsync();
        }

        public Market? GetMarketById(int id)
        {
            return _stockExchangeDbContext.Markets.FirstOrDefault(m => m.Id == id);
        }
    }
}
