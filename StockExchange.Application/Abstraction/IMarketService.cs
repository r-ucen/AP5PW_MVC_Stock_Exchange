using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Abstraction
{
    public interface IMarketService
    {
        Task<IList<Market>> GetAllMarketsAsync();
        void Create(Market market);
        public void Delete(int id);
        public Market? GetMarketById(int id);
    }
}
