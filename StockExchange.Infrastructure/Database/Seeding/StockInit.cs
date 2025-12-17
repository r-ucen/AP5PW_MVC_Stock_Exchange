using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database.Seeding
{
    internal class StockInit
    {
        public IList<Stock> getStocks()
        {
            IList<Stock> stocks = new List<Stock>()
            {
                new Stock
                {
                    Id = 1,
                    TickerSymbol = "AAPL",
                    FullName = "Apple Inc.",
                    CurrentPrice = 150.00m,
                    ImageSrc = "/img/stocks/AAPL.png",
                    MarketId = 1
                },
                new Stock
                {
                    Id = 2,
                    TickerSymbol = "MSFT",
                    FullName = "Microsoft Corporation",
                    CurrentPrice = 400.00m,
                    ImageSrc = "/img/stocks/MSFT.png",
                    MarketId = 1
                },
                new Stock
                {
                    Id = 3,
                    TickerSymbol = "GOOGL",
                    FullName = "Alphabet Inc.",
                    CurrentPrice = 280.00m,
                    ImageSrc = "/img/stocks/GOOGL.png",
                    MarketId = 1
                },
                new Stock
                {
                    Id = 4,
                    TickerSymbol = "AMZN",
                    FullName = "Amazon.com, Inc.",
                    CurrentPrice = 250.00m,
                    ImageSrc = "/img/stocks/AMZN.png",
                    MarketId = 1
                },
                new Stock
                {
                    Id = 5,
                    TickerSymbol = "TSLA",
                    FullName = "Tesla, Inc.",
                    CurrentPrice = 450.00m,
                    ImageSrc = "/img/stocks/TSLA.png",
                    MarketId = 1
                }
            };
            return stocks;
        }
    };  
}
