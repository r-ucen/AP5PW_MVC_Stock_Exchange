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
    }
}
