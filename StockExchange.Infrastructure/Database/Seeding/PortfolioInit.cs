using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database.Seeding
{
    public class PortfolioInit
    {
        public IList<Portfolio> getPortfolio()
        {
            return new List<Portfolio>
            {
                new Portfolio {
                    Id = 1,
                    UserId = 1, // admin
                    Deposits = 10000.00m
                },
                new Portfolio {
                    Id = 1,
                    UserId = 2, // manager
                    Deposits = 15000.00m
                }
            };
        }
    }
}
