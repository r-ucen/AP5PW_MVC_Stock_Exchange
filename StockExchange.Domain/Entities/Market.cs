using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Domain.Entities
{
    [Table(nameof(Market))]
    public class Market : Entity<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TimeZoneId { get; set; } = string.Empty;

        [Required]
        public TimeSpan OpenTime { get; set; }

        [Required]
        public TimeSpan CloseTime { get; set; }

        public bool IsCurrentlyOpen { get; set; }

        // Navigation property
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
