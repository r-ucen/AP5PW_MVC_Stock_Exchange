using StockExchange.Application.Abstraction;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        public Stock? Select(int id)
        {
            return _stockExchangeDbContext.Stocks.FirstOrDefault(
                    s => s.Id == id
                );
        }

        public void Create(Stock stock)
        {
            _stockExchangeDbContext.Stocks.Add(stock);
            _stockExchangeDbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Stock? stock = _stockExchangeDbContext.Stocks.FirstOrDefault(
                    s => s.Id == id
                );

            if (stock != null)
            {
                _stockExchangeDbContext.Stocks.Remove(stock);
                _stockExchangeDbContext.SaveChanges();
                deleted = true;
            }

            return deleted;
        }

        public bool Update(Stock stock)
        {
            var existingStock = _stockExchangeDbContext.Stocks.FirstOrDefault(
                    s => s.Id == stock.Id
                );

            if (existingStock == null)
            {
                return false;
            }

            _stockExchangeDbContext.Entry(existingStock).CurrentValues.SetValues(stock);
            existingStock.Id = stock.Id;
            existingStock.CurrentPriceDateTime = DateTime.UtcNow;
            _stockExchangeDbContext.SaveChanges();
            return true;
        }
    }
}
