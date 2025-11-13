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
    [Table(nameof(Order))]
    public class Order : Entity<int>
    {

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        public int StockId { get; set; }

        [Required]
        [MaxLength(4)]
        public string OrderType { get; set; } = string.Empty; // "Buy" or "Sell"

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public IUser<int>? User { get; set; }
        public Stock? Stock { get; set; }
        public Transaction? Transaction { get; set; }

    }
}
