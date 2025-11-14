using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.ViewModels
{
    public class PortfolioHoldingViewModel
    {
        public int StockId { get; set; }
        public string TickerSymbol { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal AvgPurchasePrice { get; set; }
        public decimal TotalValue => Quantity * CurrentPrice;
        public decimal GainLoss => (Quantity * CurrentPrice) - (Quantity * AvgPurchasePrice);
        public decimal GainLossPercentage => AvgPurchasePrice == 0 ? 0m : GainLoss / (Quantity * AvgPurchasePrice) * 100;
    }
}