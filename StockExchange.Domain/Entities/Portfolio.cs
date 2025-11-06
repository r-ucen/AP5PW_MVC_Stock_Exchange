using StockExchange.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Domain.Entities
{
    [Table(nameof(Portfolio))]
    public class Portfolio : Entity<string>
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Deposits { get; set; }

        // Navigation property
        public IUser<int>? User { get; set; }
        public ICollection<PortfolioStock> PortfolioStocks { get; set; } = new List<PortfolioStock>();
    }
}
