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
    public class OrderAppService : IOrderAppService
    {
        StockExchangeDbContext _stockExchangeDbContext;

        public OrderAppService(StockExchangeDbContext stockExchangeDbContext)
        {
            _stockExchangeDbContext = stockExchangeDbContext;
        }

        public IList<Order> Select()
        {
            return _stockExchangeDbContext.Orders.ToList();
        }
    }
}
