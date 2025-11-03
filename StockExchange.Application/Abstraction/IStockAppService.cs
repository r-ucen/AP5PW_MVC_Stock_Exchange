using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Abstraction
{
    public interface IStockAppService
    {
        IList<Stock> Select();
        Stock? Select(int id);
        void Create(Stock stock);
        bool Delete(int id);
        bool Update(Stock stock);
    }
}
