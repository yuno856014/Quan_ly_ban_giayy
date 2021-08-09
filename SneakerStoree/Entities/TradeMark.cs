using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Entities
{
    public class TradeMark
    {
        [Key]
        public int TradeMarkId { get; set; }
        [Required]
        [MaxLength(250)]
        public string TradeMarkName { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public ICollection<Sneaker> Sneakers { get; set; }
    }
}
