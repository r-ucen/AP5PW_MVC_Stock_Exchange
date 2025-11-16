using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchange.Domain.Validations;

namespace StockExchange.Application.ViewModels
{
    public class TradeViewModel
    {
        public int StockId { get; set; }
        public string TickerSymbol { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal AvailableCash { get; set; }
        public int QuantityOwned { get; set; }

        [Required]
        [HigherOrEqualToOne]
        public int Quantity { get; set; }
    }
}
