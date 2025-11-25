using Microsoft.EntityFrameworkCore;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database;
using StockExchange.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Implementation
{
    public class TransactionAppService : ITransactionAppService
    {
        StockExchangeDbContext _stockExchangeDbContext;

        public TransactionAppService(StockExchangeDbContext stockExchangeDbContext)
        {
            _stockExchangeDbContext = stockExchangeDbContext;
        }

        public IList<Transaction> Select()
        {
            return _stockExchangeDbContext.Transactions.ToList();
        }

        async public Task<IList<TransactionViewModel>> SelectByUserIdAsync(int userId)
        {
            /*
            SELECT
                stock.TickerSymbol,
                stock.FullName,
                TransactionType,
                Quantity,
                Price,
                TransactionDate 
            FROM
                transaction
            INNER JOIN
                stock
            ON
                transaction.StockId = stock.Id WHERE transaction.UserId = userId;
            */


            var result = await (from t in _stockExchangeDbContext.Transactions.AsNoTracking()
                                  join s in _stockExchangeDbContext.Stocks.AsNoTracking() on t.StockId equals s.Id
                                  where t.UserId == userId
                                  orderby t.TransactionDate descending
                                select new TransactionViewModel
                                  {
                                      TickerSymbol = s.TickerSymbol,
                                      FullName = s.FullName,
                                      TransactionType = t.TransactionType,
                                      Quantity = t.Quantity,
                                      Price = t.Price,
                                      TransactionDate = t.TransactionDate
                                  }).ToListAsync();

            return result;
        }
    }
}
