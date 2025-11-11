using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database.Seeding
{
    public class TransactionInit
    {
        public IList<Transaction> GetTransactions()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Id = 1,
                    UserId = 1, // admin
                    StockId = 1, // Apple
                    OrderId = 1,
                    TransactionType = "Buy",
                    Quantity = 10,
                    Price = 170.00m,
                    TransactionDate = new DateTime(2025, 11, 2, 22, 58, 40)
                },
                new Transaction
                {
                    Id = 2,
                    UserId = 2, // manager
                    StockId = 3, // Google
                    OrderId = 2,
                    TransactionType = "Buy",
                    Quantity = 5,
                    Price = 275.50m,
                    TransactionDate = new DateTime(2025, 11, 2, 22, 58, 40)
                },
                new Transaction
                {
                    Id = 3,
                    UserId = 1, // admin
                    StockId = 2, // Microfoft
                    OrderId = 3,
                    TransactionType = "Buy",
                    Quantity = 8,
                    Price = 395.00m,
                    TransactionDate = new DateTime(2025, 11, 2, 22, 58, 40)
                }
            };
        }
    }
}
