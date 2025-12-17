using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database.Seeding
{
    public class MarketInit
    {
        public IList<Market> GetMarkets()
        {
            return new List<Market>
            {
                new Market
                {
                    Id = 1,
                    Name = "NYSE",
                    TimeZoneId = "Eastern Standard Time",
                    OpenTime = new TimeSpan(9, 30, 0),
                    CloseTime = new TimeSpan(16, 0, 0),
                    IsCurrentlyOpen = false
                }
            };
        }
    }
}
