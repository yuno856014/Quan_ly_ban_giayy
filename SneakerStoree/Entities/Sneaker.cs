using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Entities
{
    public class Sneaker
    {
        [Key]
        public int SneakerId { get; set; }
        [Required(ErrorMessage = "The Shoes name is required")]
        [MaxLength(500)]
        public string SneakerName { get; set; }
        [Required(ErrorMessage = "The publish year is required")]
        [Range(minimum: 1900, maximum: 2021)]
        public int PublishYear { get; set; }
        [Required]
        [MaxLength(300)]
        public string Photo { get; set; }
        [Required]
        [Range(maximum:42,minimum:35)]
        public int Size { get; set; }
        [Required]
        [MaxLength(3000)]
        public string Information { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int TradeMarkId { get; set; }
        public TradeMark TradeMarks { get; set; }

    }
}
