using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchange.Domain.Validations;

namespace StockExchange.Domain.Entities
{
    [Table(nameof(Stock))]
    public class Stock : Entity<int>
    {
        [Required]
        [StringLength(10)]
        [AllLettersCapitalized]
        public string TickerSymbol { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrentPrice { get; set; }

        [Required]
        public DateTime CurrentPriceDateTime { get; set; }

        [StringLength(1024)]
        public string? ImageSrcUrl { get; set; }

        // Navigation properties
        public ICollection<PortfolioStock> PortfolioStocks { get; set; } = new List<PortfolioStock>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
