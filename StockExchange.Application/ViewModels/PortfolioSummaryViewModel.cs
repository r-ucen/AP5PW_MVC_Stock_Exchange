using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.ViewModels
{
    public class PortfolioSummaryViewModel
    {
        public string Title { get; set; } = "Portfolio Value";
        public decimal PortfolioValue { get; set; }
        public decimal UnrealizedGains { get; set; }
        public decimal UnrealizedGainPercentage { get; set; }
        public decimal AvailableCash { get; set; }
        public decimal Deposits { get; set; }
    }
}
