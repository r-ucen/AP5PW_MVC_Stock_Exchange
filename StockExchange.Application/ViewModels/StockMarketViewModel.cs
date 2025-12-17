using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.ViewModels
{
    public class StockMarketViewModel
    {
        public int Id { get; set; }
        public string TickerSymbol { get; set; }
        public string FullName { get; set; }
        public decimal CurrentPrice { get; set; }
        public string? ImageSrc { get; set; }
        public bool isMarketOpen { get; set; }
    }
}
