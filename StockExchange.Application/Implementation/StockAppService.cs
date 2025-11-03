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
    public class StockAppService : IStockAppService
    {
        StockExchangeDbContext _stockExchangeDbContext;

        public StockAppService(StockExchangeDbContext stockExchangeDbContext)
        {
            _stockExchangeDbContext = stockExchangeDbContext;
        }

        public IList<Stock> Select()
        {
            return _stockExchangeDbContext.Stocks.ToList();
        }

        public void Create(Stock stock)
        {
            _stockExchangeDbContext.Stocks.Add(stock);
            _stockExchangeDbContext.SaveChanges();
        }
    }
}
