using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Domain.Entities
{
    [Table(nameof(PortfolioStock))]
    public class PortfolioStock : Entity<int>
    {
        [Required]
        public int PortfolioId { get; set; }

        [Required]
        public int StockId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AvgPurchasePrice { get; set; }

        // Navigation properties
        public Portfolio? Portfolio { get; set; }
        public Stock? Stock { get; set; }
    }
}
