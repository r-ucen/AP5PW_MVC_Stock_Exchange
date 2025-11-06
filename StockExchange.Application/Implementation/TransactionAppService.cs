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
    }
}
