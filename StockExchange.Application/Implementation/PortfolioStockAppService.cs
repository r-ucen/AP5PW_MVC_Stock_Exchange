using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Implementation
{
    public class PortfolioStockAppService : IPortfolioStockAppService
    {
        StockExchangeDbContext _stockExchangeDbContext;

        public PortfolioStockAppService(StockExchangeDbContext stockExchangeDbContext)
        {
            _stockExchangeDbContext = stockExchangeDbContext;
        }

        public IList<PortfolioStock> Select()
        {
            return _stockExchangeDbContext.PortfolioStocks.ToList();
        }
    }
}
