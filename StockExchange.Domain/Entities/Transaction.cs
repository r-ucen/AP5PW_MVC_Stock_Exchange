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
    [Table(nameof(Transaction))]
    public class Transaction : Entity<int>
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        public int StockId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [StringLength(4)]
        public string TransactionType { get; set; } = string.Empty; // "Buy" or "Sell"

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        // Navigation properties
        public IUser<int>? User { get; set; }
        public Stock? Stock { get; set; }
        public Order? Order { get; set; }
        
    }
}
