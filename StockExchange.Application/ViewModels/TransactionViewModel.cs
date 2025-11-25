using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.ViewModels
{
    public class TransactionViewModel
    {
        public string TickerSymbol { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
