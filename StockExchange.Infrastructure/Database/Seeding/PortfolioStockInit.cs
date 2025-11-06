using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database.Seeding
{
    public class PortfolioStockInit
    {
        public IList<PortfolioStock> GetPortfolioStocks()
        {
            return new List<PortfolioStock>
            {
                // admin owns 10 shares of Apple and 8 shares of Microsoft
                new PortfolioStock { Id = 1, PortfolioId = 1, StockId = 1, Quantity = 10, AvgPurchasePrice = 170.00m },
                new PortfolioStock { Id = 2, PortfolioId = 1, StockId = 2, Quantity = 8, AvgPurchasePrice = 395.00m },
                // manager owns 5 shares of Google
                new PortfolioStock { Id = 3, PortfolioId = 2, StockId = 3, Quantity = 5, AvgPurchasePrice = 275.50m }
            };
        }
    }
}
