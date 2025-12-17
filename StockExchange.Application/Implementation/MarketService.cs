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

        public async Task<IList<Market>> GetAllMarketsAsync()
        {
            return await _stockExchangeDbContext.Markets.ToListAsync();
        }
    }
}
