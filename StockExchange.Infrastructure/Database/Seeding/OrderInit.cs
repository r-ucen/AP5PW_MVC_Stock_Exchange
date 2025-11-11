using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database.Seeding
{
    public class OrderInit
    {
        public IList<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    UserId = 1, // admin
                    StockId = 1, // Apple
                    OrderType = "Buy",
                    Quantity = 10,
                    CreatedDate = new DateTime(2025, 11, 2, 22, 58, 40)
                },
                new Order
                {
                    Id = 2,
                    UserId = 2, // manager
                    StockId = 3, // Google
                    OrderType = "Buy",
                    Quantity = 5,
                    CreatedDate = new DateTime(2025, 11, 2, 22, 58, 40)
                },
                new Order
                {
                    Id = 3,
                    UserId = 1, // admin
                    StockId = 2, // Microfoft
                    OrderType = "Buy",
                    Quantity = 8,
                    CreatedDate = new DateTime(2025, 11, 2, 22, 58, 40)
                }
            };
        }
    }
}
