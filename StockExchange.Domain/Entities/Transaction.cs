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
    public class Transaction : Entity<int>
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int StockId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string TransactionType { get; set; } = string.Empty; // "Buy" or "Sell"

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        // Navigation properties
        [NotMapped]
        public IUser<int>? User { get; set; }
        public Stock? Stock { get; set; }
        public Order? Order { get; set; }
        
    }
}
